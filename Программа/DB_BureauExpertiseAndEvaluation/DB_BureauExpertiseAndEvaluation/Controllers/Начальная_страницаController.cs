using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB_BureauExpertiseAndEvaluation.Controllers
{
    public class Начальная_страницаController : Controller
    {
        // GET: Начальная_страница
        public ActionResult IndexAdmin()
        {
            return View();
        }

        public ActionResult IndexUser()
        {
            return View();
        }
    }
}