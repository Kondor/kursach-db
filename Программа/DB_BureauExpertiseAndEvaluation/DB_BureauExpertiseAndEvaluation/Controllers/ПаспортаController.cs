using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DB_BureauExpertiseAndEvaluation;

namespace DB_BureauExpertiseAndEvaluation.Controllers
{
    public class ПаспортаController : Controller
    {
        private Бюро_экспертизы_и_оценкиEntities db = new Бюро_экспертизы_и_оценкиEntities();

        // GET: Паспорта
        public ActionResult Index()
        {
            return View(db.Паспорта.ToList());
        }

        /// Поиск клиентов в сущности Паспорта
        [HttpPost]
        public ActionResult Index(string name, string surname)
        {
            var passport = db.Паспорта.AsEnumerable();
            if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(surname))
            {
                passport = db.Паспорта.Where(n => n.Имя == name).Where(s => s.Фамилия == surname).Select(k => k);
            }
            else
            {
                passport = db.Паспорта;
            }
            return View(passport.ToList());
        }

        // GET: Паспорта/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Паспорта паспорта = db.Паспорта.Find(id);
            if (паспорта == null)
            {
                return HttpNotFound();
            }
            return View(паспорта);
        }

        // GET: Паспорта/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Паспорта/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Номер_паспорта,Серия_паспорта,Дата_выдачи,Дата_окончания,Адрес,Фамилия,Имя,Отчество,Дата_рождения,Пол")] Паспорта паспорта)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Паспорта.Add(паспорта);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {

                    return RedirectToAction("Error");
                }
            }

            return View(паспорта);
        }

        // GET: Паспорта/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Паспорта паспорта = db.Паспорта.Find(id);
            if (паспорта == null)
            {
                return HttpNotFound();
            }
            return View(паспорта);
        }

        // POST: Паспорта/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Номер_паспорта,Серия_паспорта,Дата_выдачи,Дата_окончания,Адрес,Фамилия,Имя,Отчество,Дата_рождения,Пол")] Паспорта паспорта)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(паспорта).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    return RedirectToAction("Error");
                }
            }
            return View(паспорта);
        }

        // GET: Паспорта/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Паспорта паспорта = db.Паспорта.Find(id);
            if (паспорта == null)
            {
                return HttpNotFound();
            }
            return View(паспорта);
        }

        public ActionResult Error()
        {
            return View();
        }

        // POST: Паспорта/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Паспорта паспорта = db.Паспорта.Find(id);
            try
            {
                db.Паспорта.Remove(паспорта);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
