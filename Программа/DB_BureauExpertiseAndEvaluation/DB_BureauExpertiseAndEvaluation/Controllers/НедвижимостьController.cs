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
    public class НедвижимостьController : Controller
    {
        private Бюро_экспертизы_и_оценкиEntities db = new Бюро_экспертизы_и_оценкиEntities();

        // GET: Недвижимость
        public ActionResult Index()
        {
            var недвижимость = db.Недвижимость.Include(н => н.Объекты_экспертизы);
            return View(недвижимость.ToList());
        }

        // GET: Недвижимость/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Недвижимость недвижимость = db.Недвижимость.Find(id);
            if (недвижимость == null)
            {
                return HttpNotFound();
            }
            return View(недвижимость);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Недвижимость/Create
        public ActionResult Create()
        {
            ViewBag.Ид_объекта = new SelectList(db.Объекты_экспертизы, "Ид_объекта", "Наименование");
            return View();
        }

        // POST: Недвижимость/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Адрес,Район,Ид_объекта,Количество_комнат,Площадь,Тип_постройки")] Недвижимость недвижимость)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Недвижимость.Add(недвижимость);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Error");
                }
            }

            ViewBag.Ид_объекта = new SelectList(db.Объекты_экспертизы, "Ид_объекта", "Наименование", недвижимость.Ид_объекта);
            return View(недвижимость);
        }

        // GET: Недвижимость/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Недвижимость недвижимость = db.Недвижимость.Find(id);
            if (недвижимость == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ид_объекта = new SelectList(db.Объекты_экспертизы, "Ид_объекта", "Наименование", недвижимость.Ид_объекта);
            return View(недвижимость);
        }

        // POST: Недвижимость/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Адрес,Район,Ид_объекта,Количество_комнат,Площадь,Тип_постройки")] Недвижимость недвижимость)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(недвижимость).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {

                    return RedirectToAction("Error");
                }
            }
            ViewBag.Ид_объекта = new SelectList(db.Объекты_экспертизы, "Ид_объекта", "Наименование", недвижимость.Ид_объекта);
            return View(недвижимость);
        }

        // GET: Недвижимость/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Недвижимость недвижимость = db.Недвижимость.Find(id);
            if (недвижимость == null)
            {
                return HttpNotFound();
            }
            return View(недвижимость);
        }

        // POST: Недвижимость/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Недвижимость недвижимость = db.Недвижимость.Find(id);

            try
            {
                db.Недвижимость.Remove(недвижимость);
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
