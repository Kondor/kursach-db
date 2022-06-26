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
    public class КвитанцияController : Controller
    {
        private Бюро_экспертизы_и_оценкиEntities db = new Бюро_экспертизы_и_оценкиEntities();

        // GET: Квитанция
        public ActionResult Index()
        {
            var квитанция = db.Квитанция.Include(к => к.Экспертиза_и_оценка);
            return View(квитанция.ToList());
        }

        [HttpPost]
        public ActionResult Index(decimal ?firstValue, decimal ?secondValue)
        {
            var order = db.Квитанция.AsEnumerable();

            if (firstValue != null && secondValue != null)
            {
                order = db.Квитанция.Where(n => n.Стоимость_экспертизы >= firstValue && n.Стоимость_экспертизы <= secondValue).Select(k => k);
            }
            return View(order.ToList());
        }

        // GET: Квитанция/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Квитанция квитанция = db.Квитанция.Find(id);
            if (квитанция == null)
            {
                return HttpNotFound();
            }
            return View(квитанция);
        }

        // GET: Квитанция/Create
        public ActionResult Create()
        {
            ViewBag.Ид_диагностики = new SelectList(db.Экспертиза_и_оценка, "Ид_диагностики", "Вид_диагностики");
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        // POST: Квитанция/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Номер,Стоимость_экспертизы,Оценка,Решение,Ид_диагностики")] Квитанция квитанция)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Квитанция.Add(квитанция);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Error");
                }
            }

            ViewBag.Ид_диагностики = new SelectList(db.Экспертиза_и_оценка, "Ид_диагностики", "Вид_диагностики", квитанция.Ид_диагностики);
            return View(квитанция);
        }

        // GET: Квитанция/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Квитанция квитанция = db.Квитанция.Find(id);
            if (квитанция == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ид_диагностики = new SelectList(db.Экспертиза_и_оценка, "Ид_диагностики", "Вид_диагностики", квитанция.Ид_диагностики);
            return View(квитанция);
        }

        // POST: Квитанция/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Номер,Стоимость_экспертизы,Оценка,Решение,Ид_диагностики")] Квитанция квитанция)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(квитанция).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Error");
                }
            }
            ViewBag.Ид_диагностики = new SelectList(db.Экспертиза_и_оценка, "Ид_диагностики", "Вид_диагностики", квитанция.Ид_диагностики);
            return View(квитанция);
        }

        // GET: Квитанция/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Квитанция квитанция = db.Квитанция.Find(id);
            if (квитанция == null)
            {
                return HttpNotFound();
            }
            return View(квитанция);
        }

        // POST: Квитанция/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Квитанция квитанция = db.Квитанция.Find(id);
            try
            {
                db.Квитанция.Remove(квитанция);
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
