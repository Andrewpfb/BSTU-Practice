using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.Entity;

using ITechArt.Blog.Models;


namespace ITechArt.Blog.Controllers
{
    public class PostController : Controller
    {
        private BlogContext db = new BlogContext();
        private enum WorkWithTagModifier
        {
            Edit,
            Create,
            Delete
        }

        public ActionResult Details(int id)
        {
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            PostViewModel tmpPostViewModel = new PostViewModel();
            tmpPostViewModel.Id = post.Id;
            tmpPostViewModel.Title = post.Title;
            tmpPostViewModel.ImagePath = post.ImagePath;
            tmpPostViewModel.PostedOn = post.PostedOn;
            tmpPostViewModel.Description = post.Description;
            tmpPostViewModel.Author = db.User.Find(post.AuthorId).Email.Split('@')[0];
            return View(tmpPostViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Post post)
        {
            Post newPost = db.Post.Find(post.Id);
            // Для изменения данных из БД, а не данных из представления.
            string tagsFromDB = newPost.Tags;
            newPost.Title = post.Title;
            newPost.ShortDescription = post.ShortDescription;
            newPost.Description = post.Description;
            newPost.Tags = post.Tags;
            db.Entry(newPost).State = EntityState.Modified;
            db.SaveChanges();
            // Если есть различия между старыми и новыми тегами.
            if (tagsFromDB != post.Tags)
            {
                SelectDifferentTags(tagsFromDB, post.Tags);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Post post)
        {
            //Автор поста, изображение - постоянно, так как на макете не было поля для добавления изображения.
            post.PostedOn = DateTime.Now;
            post.AuthorId = db.User.Where(a => a.Email == User.Identity.Name).First().Id;
            post.ImagePath = "/Images/post1.png";
            db.Post.Add(post);
            db.SaveChanges();
            WorkWithTags(post.Tags, WorkWithTagModifier.Create);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Post post = db.Post.Find(id);
            if (post != null)
            {
                return PartialView("Delete", post);
            }
            return View("Index", "Home", null);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Delete")]
        public ActionResult DeleteRecord(int id)
        {
            Post post = db.Post.Find(id);
            if (post != null)
            {
                db.Post.Remove(post);
                db.SaveChanges();
                WorkWithTags(post.Tags, WorkWithTagModifier.Delete);
            }
            return RedirectToAction("Index", "Home");
        }


        #region Function for work with tags
        // Формирование строки тэгов, которых не было в посте.
        private void SelectDifferentTags(string oldTags, string newTags)
        {
            List<string> tmpOldTagsList = SplitTags(oldTags);
            List<string> tmpNewTagsList = SplitTags(newTags);
            bool tagInOld = true;
            StringBuilder differentTag = new StringBuilder();
            // Проходим циклом по списку новых тегов (которые добавлены) и проверяем каждый элемент на наличие в старом
            // списке тегов.
            for (int i = 0; i < tmpNewTagsList.Count(); i++)
            {
                if (tmpOldTagsList.Contains(tmpNewTagsList[i]))
                {
                    // Если старый список содержит тэг, то указываем это в tagInOld и 
                    // прекращаем проверку этого тэга.
                    tagInOld = true;
                    break;
                }
                tagInOld = false;
                // Если тега нет, то добавляем его в строку. Делаем это для уменьшения данных для работы функции
                // WorkWithTagsAsync.
                if (!tagInOld)
                {
                    differentTag.Append(tmpNewTagsList[i]);
                    differentTag.Append(" ");
                }
            }
            WorkWithTags(differentTag.ToString(), WorkWithTagModifier.Edit);
        }
        // Добавление\удаление тэга в зависимости от вызвавшего действия.
        private void WorkWithTags(string tags, WorkWithTagModifier action)
        {
            List<string> tagsList = SplitTags(tags);
            // Проходим по списку новых тэгов.
            foreach (string tag in tagsList)
            {
                switch (action)
                {
                    //Если в список тегов при редактировании добавился тэг.
                    case WorkWithTagModifier.Edit:
                        db.Tag.Add(new Tag { Name = tag });
                        break;
                    //Когда создаем пост, все тэги добавляем.
                    case WorkWithTagModifier.Create:
                        db.Tag.Add(new Tag { Name = tag });
                        break;
                    //Удаляем тэги при удалении поста.
                    case WorkWithTagModifier.Delete:
                        Tag tmp = db.Tag.First(t => t.Name == tag);
                        db.Tag.Remove(tmp);
                        break;
                }
                db.SaveChanges();
                break;
            }
        }
        //Разделение цельной строки тэгов на отдельные.
        private List<string> SplitTags(string tags)
        {
            List<string> tmp = (tags.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)).ToList();
            return tmp;
        }
        #endregion
    }
}