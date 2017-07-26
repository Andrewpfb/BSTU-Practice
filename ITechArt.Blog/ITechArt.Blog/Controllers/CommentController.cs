using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.Entity;

using ITechArt.Blog.Models;
using ITechArt.Blog.Service;


namespace ITechArt.Blog.Controllers
{
    public class CommentController : Controller
    {
        private BlogContext db = new BlogContext();
        public ActionResult CommentPart(int id = 0)
        {
            var commentsList = db.Comment.Where(c => c.PostId == id).ToList();
            if (commentsList.Count() <= 0)
            {
                return HttpNotFound();
            }
            return PartialView(CreateCommentList.CreateViewListComment(commentsList));
        }

        public ActionResult Delete(int id)
        {
            Comment comm = db.Comment.Find(id);
            if (comm!= null)
            {
                return PartialView("Delete",comm);
            }
            return View("Index", "Home", null);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Delete")]
        public ActionResult DeleteRecord(int id)
        {
            Comment comm = db.Comment.Find(id);
            if (comm != null)
            {
                db.Comment.Remove(comm);
                db.SaveChanges();
            }
            return RedirectToAction("Details", "Post",new { id = comm.PostId });
        }


        public ActionResult CommentCountPart(int id=0)
        {
            int commentCount = db.Comment.Where(p => p.PostId == id).Count();
            return PartialView(commentCount);
        }

        //TODO Добавить функционал по комментариям.
        //[HttpGet]
        //[Authorize]
        //public ActionResult AddComment()
        //{
        //    return PartialView();
        //}

        //[HttpPost]
        //[Authorize]
        //public ActionResult AddComment(Comment comment)
        //{
        //    comment.AuthorId= db.User.Where(a => a.Email == User.Identity.Name).First().Id;
        //    comment.CommentOn = DateTime.Now;
        //    comment.ParentId = 0;
        //    comment.Seed = 0;
        //    comment.UserImagePath = "/Images/U1.png";
        //    db.Comment.Add(comment);
        //    db.SaveChanges();
        //}
    }
}