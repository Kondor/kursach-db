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
    public class Объекты_экспертизыController : Controller
    {
        private Бюро_экспертизы_и_оценкиEntities db = new Бюро_экспертизы_и_оценкиEntities();

        // GET: Объекты_экспертизы
        public ActionResult Index()
        {
            var объекты_экспертизы = db.Объекты_экспертизы.Include(о => о.Недвижимость).Include(о => о.Техника);
            return View(объекты_экспертизы.ToList());
        }

        // GET: Объекты_экспертизы/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Объекты_экспертизы объекты_экспертизы = db.Объекты_экспертизы.Find(id);
            if (объекты_экспертизы == null)
            {
                return HttpNotFound();
            }
            return View(объекты_экспертизы);
        }

        // GET: Объекты_экспертизы/Create
        public ActionResult Create()
        {
            ViewBag.Ид_объекта = new SelectList(db.Недвижимость, "Ид_объекта", "Адрес");
            ViewBag.Ид_объекта = new SelectList(db.Техника, "Ид_объекта", "Серийный_номер");
            return View();
        }

        // POST: Объекты_экспертизы/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ид_объекта,Наименование,Вид")] Объекты_экспертизы объекты_экспертизы)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Объекты_экспертизы.Add(объекты_экспертизы);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Error");
                }
            }

            ViewBag.Ид_объекта = new SelectList(db.Недвижимость, "Ид_объекта", "Адрес", объекты_экспертизы.Ид_объекта);
            ViewBag.Ид_объекта = new SelectList(db.Техника, "Ид_объекта", "Серийный_номер", объекты_экспертизы.Ид_объекта);
            return View(объекты_экспертизы);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Объекты_экспертизы/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Объекты_экспертизы объекты_экспертизы = db.Объекты_экспертизы.Find(id);
            if (объекты_экспертизы == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ид_объекта = new SelectList(db.Недвижимость, "Ид_объекта", "Адрес", объекты_экспертизы.Ид_объекта);
            ViewBag.Ид_объекта = new SelectList(db.Техника, "Ид_объекта", "Серийный_номер", объекты_экспертизы.Ид_объекта);
            return View(объекты_экспертизы);
        }

        // POST: Объекты_экспертизы/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ид_объекта,Наименование,Вид")] Объекты_экспертизы объекты_экспертизы)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(объекты_экспертизы).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {

                    return RedirectToAction("Error");
                }
            }
            ViewBag.Ид_объекта = new SelectList(db.Недвижимость, "Ид_объекта", "Адрес", объекты_экспертизы.Ид_объекта);
            ViewBag.Ид_объекта = new SelectList(db.Техника, "Ид_объекта", "Серийный_номер", объекты_экспертизы.Ид_объекта);
            return View(объекты_экспертизы);
        }

        // GET: Объекты_экспертизы/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Объекты_экспертизы объекты_экспертизы = db.Объекты_экспертизы.Find(id);
            if (объекты_экспертизы == null)
            {
                return HttpNotFound();
            }
            return View(объекты_экспертизы);
        }

        // POST: Объекты_экспертизы/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Объекты_экспертизы объекты_экспертизы = db.Объекты_экспертизы.Find(id);

            try
            {
                db.Объекты_экспертизы.Remove(объекты_экспертизы);
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
