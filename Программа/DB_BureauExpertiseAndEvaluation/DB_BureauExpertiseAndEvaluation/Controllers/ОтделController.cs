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
    public class ОтделController : Controller
    {
        private Бюро_экспертизы_и_оценкиEntities db = new Бюро_экспертизы_и_оценкиEntities();

        // GET: Отдел
        public ActionResult Index()
        {
            return View(db.Отдел.ToList());
        }

        // GET: Отдел/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Отдел отдел = db.Отдел.Find(id);
            if (отдел == null)
            {
                return HttpNotFound();
            }
            return View(отдел);
        }

        // GET: Отдел/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Отдел/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ид_отдела,Наименование_отдела,Начальник_отдела,Телефон,Зам_начальника")] Отдел отдел)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Отдел.Add(отдел);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {

                    return RedirectToAction("Error");
                }
                
            }

            return View(отдел);
        }

        // GET: Отдел/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Отдел отдел = db.Отдел.Find(id);
            if (отдел == null)
            {
                return HttpNotFound();
            }
            return View(отдел);
        }

        // POST: Отдел/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ид_отдела,Наименование_отдела,Начальник_отдела,Телефон,Зам_начальника")] Отдел отдел)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(отдел).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {

                    return RedirectToAction("Error");
                }
            }
            return View(отдел);
        }

        public ActionResult Error()
        {
            return View();
        }
        // GET: Отдел/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Отдел отдел = db.Отдел.Find(id);
            if (отдел == null)
            {
                return HttpNotFound();
            }
            return View(отдел);
        }

        // POST: Отдел/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Отдел отдел = db.Отдел.Find(id);
            try
            {
                db.Отдел.Remove(отдел);
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
