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
    public class КлиентыController : Controller
    {
        private Бюро_экспертизы_и_оценкиEntities db = new Бюро_экспертизы_и_оценкиEntities();

        // GET: Клиенты
        public ActionResult Index()
        {
            var клиенты = db.Клиенты.Include(к => к.Паспорта);
            return View(клиенты.ToList());
        }


        // GET: Клиенты/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Клиенты клиенты = db.Клиенты.Find(id);
            if (клиенты == null)
            {
                return HttpNotFound();
            }
            return View(клиенты);
        }

        // GET: Клиенты/Create
        public ActionResult Create()
        {
            ViewBag.Номер_паспорта = new SelectList(db.Паспорта, "Номер_паспорта", "Номер_паспорта");
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        // POST: Клиенты/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ид_клиента,Телефон,Эл_почта,Номер_паспорта")] Клиенты клиенты)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Клиенты.Add(клиенты);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Error");
                }
            }

            ViewBag.Номер_паспорта = new SelectList(db.Паспорта, "Номер_паспорта", "Номер_паспорта", клиенты.Номер_паспорта);
            return View(клиенты);
        }

        // GET: Клиенты/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Клиенты клиенты = db.Клиенты.Find(id);
            if (клиенты == null)
            {
                return HttpNotFound();
            }
            ViewBag.Номер_паспорта = new SelectList(db.Паспорта, "Номер_паспорта", "Номер_паспорта", клиенты.Номер_паспорта);
            return View(клиенты);
        }

        // POST: Клиенты/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ид_клиента,Телефон,Эл_почта,Номер_паспорта")] Клиенты клиенты)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(клиенты).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Error");
                }
            }
            ViewBag.Номер_паспорта = new SelectList(db.Паспорта, "Номер_паспорта", "Номер_паспорта", клиенты.Номер_паспорта);
            return View(клиенты);
        }

        // GET: Клиенты/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Клиенты клиенты = db.Клиенты.Find(id);
            if (клиенты == null)
            {
                return HttpNotFound();
            }
            return View(клиенты);
        }

        // POST: Клиенты/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Клиенты клиенты = db.Клиенты.Find(id);
            try
            {
                db.Клиенты.Remove(клиенты);
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
