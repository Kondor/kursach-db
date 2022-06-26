using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DB_BureauExpertiseAndEvaluation;
using System.Data.SqlClient;

namespace DB_BureauExpertiseAndEvaluation.Controllers
{
    public class НакладнаяController : Controller
    {
        private Бюро_экспертизы_и_оценкиEntities db = new Бюро_экспертизы_и_оценкиEntities();

        // GET: Накладная
        public ActionResult Index()
        {
            var накладная = db.Накладная.Include(н => н.Клиенты).Include(н => н.Объекты_экспертизы).Include(н => н.Сотрудники);
            return View(накладная.ToList());
        }

        [HttpPost]
        public ActionResult Index(bool value, int? series, int? number)
        {
            
            var nakl = db.Накладная.AsEnumerable();
            if (value == true && series == null && number == null)
            {
                if (value == true)
                    nakl = db.Накладная.Where(d => d.Статус.ToLower() == "открыт");

                return View(nakl.ToList());
            }
            else if (value == false && series != null && number != null)
            {
                object[] xparams = {
                new SqlParameter("@Series", series),
                new SqlParameter("@Nomer", number)
                };

                db.Database.ExecuteSqlCommand("EXEC procEditNaklad @Series, @Nomer", xparams);
                
                return View(nakl.ToList()) ;
            }
            else if (value == true)
            {
                nakl = db.Накладная.Where(d => d.Статус.ToLower() == "открыт");
                
                return View(nakl.ToList());
            }
            
            return View(nakl.ToList());
        }

        public ActionResult Error()
        {
            return View();
        }
       
        public ActionResult printFuncProgDohod(DateTime ?dateTimeFirst, DateTime ?dateTimeSecond)
        {
            IQueryable<funcProgDohod_Result> printFunc = db.funcProgDohod(dateTimeFirst, dateTimeSecond);

            return View(printFunc);
        }

        // GET: Накладная/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Накладная накладная = db.Накладная.Find(id);
            if (накладная == null)
            {
                return HttpNotFound();
            }
            return View(накладная);
        }

        // GET: Накладная/Create
        public ActionResult Create()
        {
            ViewBag.Ид_клиента = new SelectList(db.Клиенты, "Ид_клиента", "Ид_клиента");
            ViewBag.Ид_объекта = new SelectList(db.Объекты_экспертизы, "Ид_объекта", "Наименование");
            ViewBag.Ид_сотрудника = new SelectList(db.Сотрудники, "Ид_сотрудника", "Фамилия");
            return View();
        }

        // POST: Накладная/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Номер,Дата_приёма,Срок_выполнения_диагностики,Причина_обращения,Состояние_объекта,Статус,Примерная_стоимость_экспертизы,Ид_клиента,Ид_сотрудника,Ид_объекта")] Накладная накладная)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Накладная.Add(накладная);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    
                    return RedirectToAction("Error");
                    
                }
            }

            ViewBag.Ид_клиента = new SelectList(db.Клиенты, "Ид_клиента", "Ид_клиента", накладная.Ид_клиента);
            ViewBag.Ид_объекта = new SelectList(db.Объекты_экспертизы, "Ид_объекта", "Наименование", накладная.Ид_объекта);
            ViewBag.Ид_сотрудника = new SelectList(db.Сотрудники, "Ид_сотрудника", "Фамилия", накладная.Ид_сотрудника);
            return View(накладная);
        }

        // GET: Накладная/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Накладная накладная = db.Накладная.Find(id);
            if (накладная == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ид_клиента = new SelectList(db.Клиенты, "Ид_клиента", "Ид_клиента", накладная.Ид_клиента);
            ViewBag.Ид_объекта = new SelectList(db.Объекты_экспертизы, "Ид_объекта", "Наименование", накладная.Ид_объекта);
            ViewBag.Ид_сотрудника = new SelectList(db.Сотрудники, "Ид_сотрудника", "Фамилия", накладная.Ид_сотрудника);
            return View(накладная);
        }

        // POST: Накладная/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Номер,Дата_приёма,Срок_выполнения_диагностики,Причина_обращения,Состояние_объекта,Статус,Примерная_стоимость_экспертизы,Ид_клиента,Ид_сотрудника,Ид_объекта")] Накладная накладная)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(накладная).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {

                    return RedirectToAction("Error");
                }
            }
            ViewBag.Ид_клиента = new SelectList(db.Клиенты, "Ид_клиента", "Ид_клиента", накладная.Ид_клиента);
            ViewBag.Ид_объекта = new SelectList(db.Объекты_экспертизы, "Ид_объекта", "Наименование", накладная.Ид_объекта);
            ViewBag.Ид_сотрудника = new SelectList(db.Сотрудники, "Ид_сотрудника", "Фамилия", накладная.Ид_сотрудника);
            return View(накладная);
        }

        // GET: Накладная/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Накладная накладная = db.Накладная.Find(id);
            if (накладная == null)
            {
                return HttpNotFound();
            }
            return View(накладная);
        }

        // POST: Накладная/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Накладная накладная = db.Накладная.Find(id);
            try
            {
                db.Накладная.Remove(накладная);
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

        public ActionResult IndexUser()
        {
            var накладная = db.Накладная.Include(н => н.Клиенты).Include(н => н.Объекты_экспертизы).Include(н => н.Сотрудники);
            return View(накладная.ToList());
        }

        // GET: Накладная/Details/5
        public ActionResult DetailsUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Накладная накладная = db.Накладная.Find(id);
            if (накладная == null)
            {
                return HttpNotFound();
            }
            return View(накладная);
        }
    }
}
