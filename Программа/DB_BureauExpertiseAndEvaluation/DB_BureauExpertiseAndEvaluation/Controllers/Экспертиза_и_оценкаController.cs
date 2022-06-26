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
    public class Экспертиза_и_оценкаController : Controller
    {
        private Бюро_экспертизы_и_оценкиEntities db = new Бюро_экспертизы_и_оценкиEntities();

        // GET: Экспертиза_и_оценка
        public ActionResult Index()
        {
            var экспертиза_и_оценка = db.Экспертиза_и_оценка.Include(э => э.Объекты_экспертизы);
            return View(экспертиза_и_оценка.ToList());
        }

        // GET: Экспертиза_и_оценка/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Экспертиза_и_оценка экспертиза_и_оценка = db.Экспертиза_и_оценка.Find(id);
            if (экспертиза_и_оценка == null)
            {
                return HttpNotFound();
            }
            return View(экспертиза_и_оценка);
        }

        // GET: Экспертиза_и_оценка/Create
        public ActionResult Create()
        {
            ViewBag.Ид_объекта = new SelectList(db.Объекты_экспертизы, "Ид_объекта", "Наименование");
            return View();
        }


        public ActionResult Error()
        {
            return View();
        }

        // POST: Экспертиза_и_оценка/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ид_диагностики,Ид_объекта,Вид_диагностики,Количество_дней")] Экспертиза_и_оценка экспертиза_и_оценка)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Экспертиза_и_оценка.Add(экспертиза_и_оценка);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {

                    return RedirectToAction("Error");
                }
                
            }

            ViewBag.Ид_объекта = new SelectList(db.Объекты_экспертизы, "Ид_объекта", "Наименование", экспертиза_и_оценка.Ид_объекта);
            return View(экспертиза_и_оценка);
        }

        // GET: Экспертиза_и_оценка/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Экспертиза_и_оценка экспертиза_и_оценка = db.Экспертиза_и_оценка.Find(id);
            if (экспертиза_и_оценка == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ид_объекта = new SelectList(db.Объекты_экспертизы, "Ид_объекта", "Наименование", экспертиза_и_оценка.Ид_объекта);
            return View(экспертиза_и_оценка);
        }

        // POST: Экспертиза_и_оценка/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ид_диагностики,Ид_объекта,Вид_диагностики,Количество_дней")] Экспертиза_и_оценка экспертиза_и_оценка)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(экспертиза_и_оценка).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {

                    return RedirectToAction("Error");
                }
            }
            ViewBag.Ид_объекта = new SelectList(db.Объекты_экспертизы, "Ид_объекта", "Наименование", экспертиза_и_оценка.Ид_объекта);
            return View(экспертиза_и_оценка);
        }

        // GET: Экспертиза_и_оценка/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Экспертиза_и_оценка экспертиза_и_оценка = db.Экспертиза_и_оценка.Find(id);
            if (экспертиза_и_оценка == null)
            {
                return HttpNotFound();
            }
            return View(экспертиза_и_оценка);
        }

        // POST: Экспертиза_и_оценка/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Экспертиза_и_оценка экспертиза_и_оценка = db.Экспертиза_и_оценка.Find(id);

            try
            {
                db.Экспертиза_и_оценка.Remove(экспертиза_и_оценка);
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
