using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.Entity;

using ITechArt.Blog.Models;
using ITechArt.Blog.Service;


namespace ITechArt.Blog.Controllers
{
    public class HomeController : Controller
    {
        private BlogContext db = new BlogContext();

        public ActionResult Index(int page = 1)
        {
            //Пагинация. На странице отображается по три поста. Ссылки на страницы выполнены в общем стиле блога.
            int pageSize = 3;
            IEnumerable<Post> postPerPages = db.Post.OrderByDescending(d => d.PostedOn).Skip((page - 1) * pageSize).Take(pageSize);
            PostPageInfoModel pageInfo = new PostPageInfoModel { PageNumber = page, PageSize = pageSize, TotalItems = db.Post.Count() };
            PostViewModel pvm = new PostViewModel { PageInfo = pageInfo, Posts = postPerPages };
            return View(pvm);
        }

        public ActionResult RecentPostPart()
        {
            //Получение трех последних постов.
            int countLastPosts = 3;
            List<Post> postsList = db.Post.OrderByDescending(p => p.PostedOn).Take(countLastPosts).ToList();
            PostViewModel pvm = new PostViewModel();
            pvm.Posts = postsList;
            return PartialView(pvm);
        }

        // TODO Сделать приличный вывод.
        public ActionResult PopularTagPart()
        {
            List<Tag> tagsList = db.Tag.ToList();
            TagViewModel tagViewModel = new TagViewModel();
            tagViewModel.TagsDict = CountTags.TagsNamesAndFontsSize(tagsList);
            return PartialView(tagViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}