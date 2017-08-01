using System.Linq;
using System.Web.Security;
using System.Web.Mvc;

using ITechArt.Blog.Models;


namespace ITechArt.Blog.Controllers
{
    public class AccountController : Controller
    {
        private const int adminRole = 1;
        private const int userRole = 2;
        //В проекте используется валидация форм на основе cookies.
        [HttpGet]
        public ActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (BlogContext db = new BlogContext())
                {
                    user = db.User.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password);
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The user with such a username and password does not exist");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (BlogContext db = new BlogContext())
                {
                    user = db.User.FirstOrDefault(u => u.Email == model.Name);
                }
                if (user == null)
                {
                    using (BlogContext db = new BlogContext())
                    {
                        db.User.Add(new User { Email = model.Name, Password = model.Password, Role = userRole });
                        db.SaveChanges();

                        user = db.User.Where(u => u.Email == model.Name && u.Password == model.Password).FirstOrDefault();
                    }
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "This user already exists");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}