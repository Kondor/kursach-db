using DB_BureauExpertiseAndEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DB_BureauExpertiseAndEvaluation.Controllers
{
    public class АутентификацияController : Controller
    {
        // GET: Аутентификация
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;
                string role = string.Empty;

                int id;
                using (UserContext db = new UserContext())
                {
                    try
                    {
                        user = db.Users.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password);
                        role = db.Users.Where(n => n.Email == model.Name).Select(s => s.Role).Single();
                        id = db.Users.Where(n => n.Email == model.Name).Select(s => s.Id).Single();
                    }
                    catch
                    {

                    }
                }
                if (user != null)
                {
                    if (role == "admin")
                    {
                        return RedirectToAction("IndexAdmin", "Начальная_страница");
                    }
                    else if(role == "user")
                    {
                        return RedirectToAction("IndexUser", "Начальная_страница");
                    }
                    else
                    {
                        ViewBag.Message = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "artem:", DateTime.Now.ToString());
                    }

                    //FormsAuthentication.SetAuthCookie(model.Name, true);
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegister model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Name);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (UserContext db = new UserContext())
                    {
                        db.Users.Add(new User { Email = model.Name, Password = model.Password });
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Email == model.Name && u.Password == model.Password).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}