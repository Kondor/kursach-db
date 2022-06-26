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
    public class ТехникаController : Controller
    {
        private Бюро_экспертизы_и_оценкиEntities db = new Бюро_экспертизы_и_оценкиEntities();

        // GET: Техника
        public ActionResult Index()
        {
            var техника = db.Техника.Include(т => т.Объекты_экспертизы);
            return View(техника.ToList());
        }

        // GET: Техника/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Техника техника = db.Техника.Find(id);
            if (техника == null)
            {
                return HttpNotFound();
            }
            return View(техника);
        }

        // GET: Техника/Create
        public ActionResult Create()
        {
            ViewBag.Ид_объекта = new SelectList(db.Объекты_экспертизы, "Ид_объекта", "Наименование");
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        // POST: Техника/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Год_производства,Серийный_номер,Цена_при_покупке,Ид_объекта,Компания_производитель,Модель")] Техника техника)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Техника.Add(техника);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Error");
                }
                
            }

            ViewBag.Ид_объекта = new SelectList(db.Объекты_экспертизы, "Ид_объекта", "Наименование", техника.Ид_объекта);
            return View(техника);
        }

        // GET: Техника/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Техника техника = db.Техника.Find(id);
            if (техника == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ид_объекта = new SelectList(db.Объекты_экспертизы, "Ид_объекта", "Наименование", техника.Ид_объекта);
            return View(техника);
        }

        // POST: Техника/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Год_производства,Серийный_номер,Цена_при_покупке,Ид_объекта,Компания_производитель,Модель")] Техника техника)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(техника).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Error");
                }
                
            }
            ViewBag.Ид_объекта = new SelectList(db.Объекты_экспертизы, "Ид_объекта", "Наименование", техника.Ид_объекта);
            return View(техника);
        }

        // GET: Техника/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Техника техника = db.Техника.Find(id);
            if (техника == null)
            {
                return HttpNotFound();
            }
            return View(техника);
        }

        // POST: Техника/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Техника техника = db.Техника.Find(id);
            try
            {
                db.Техника.Remove(техника);
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
