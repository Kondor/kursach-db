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
using System.Diagnostics;
using DB_BureauExpertiseAndEvaluation.Models;

namespace DB_BureauExpertiseAndEvaluation.Controllers
{
    public class СотрудникиController : Controller
    {
        private Бюро_экспертизы_и_оценкиEntities db = new Бюро_экспертизы_и_оценкиEntities();

        // GET: Сотрудники
        public ActionResult Index()
        {
          
            var сотрудники = db.Сотрудники.Include(с => с.Отдел);
            return View(сотрудники.ToList());
        }

        /// ПОИСК И ФИЛЬТРАЦИЯ
        [HttpPost]
        public ActionResult Index(string text, string name, string surname)
        {

            var sotr = db.Сотрудники.AsEnumerable();

            if (text != "Все")
            {
               
                if (!String.IsNullOrEmpty(text))
                {
                    sotr = db.Сотрудники.Where(n => n.Отдел.Наименование_отдела == text).ToList();
                }

                return View(sotr.ToList());
            }
            else if (name != null && surname != null && text == "Все")
            {
                
                if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(surname))
                {
                    sotr = db.Сотрудники.Where(n => n.Имя == name).Where(s => s.Фамилия == surname).Select(k => k);
                }
                return View(sotr.ToList());
            }
            else
            {
                
                sotr = db.Сотрудники.Where(n => n.Отдел.Наименование_отдела == text).ToList().Where(n => n.Имя == name).Where(s => s.Фамилия == surname).Select(k => k);
                return View(sotr.ToList());
            }

            return View(sotr.ToList());   
        }

        /// ФУНКЦИЯ
        public ActionResult printFuncZarplata(int ?Id)
        {
            IQueryable<funcZarplata_ResultF> printFunc = db.funcZarplata(Id);

            return View(printFunc);
        }

        // GET: Сотрудники/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сотрудники сотрудники = db.Сотрудники.Find(id);
            if (сотрудники == null)
            {
                return HttpNotFound();
            }
            return View(сотрудники);
        }

        // GET: Сотрудники/Create
        public ActionResult Create()
        {
            ViewBag.Ид_отдела = new SelectList(db.Отдел, "Ид_отдела", "Наименование_отдела");
            return View();
        }

        // POST: Сотрудники/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ид_сотрудника,Фамилия,Имя,Отчество,Дата_рождения,Адрес_проживания,Стаж,Телефон,Номер_трудовой_книжки,ИНН,Зарплата,Серия_паспорта,Номер_паспорта,Ид_отдела")] Сотрудники сотрудники)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Сотрудники.Add(сотрудники);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Error");
                }
            }

            ViewBag.Ид_отдела = new SelectList(db.Отдел, "Ид_отдела", "Наименование_отдела", сотрудники.Ид_отдела);
            return View(сотрудники);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Сотрудники/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сотрудники сотрудники = db.Сотрудники.Find(id);
            if (сотрудники == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ид_отдела = new SelectList(db.Отдел, "Ид_отдела", "Наименование_отдела", сотрудники.Ид_отдела);
            return View(сотрудники);
        }

        // POST: Сотрудники/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ид_сотрудника,Фамилия,Имя,Отчество,Дата_рождения,Адрес_проживания,Стаж,Телефон,Номер_трудовой_книжки,ИНН,Зарплата,Серия_паспорта,Номер_паспорта,Ид_отдела")] Сотрудники сотрудники)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(сотрудники).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Error");
                }
            }
            ViewBag.Ид_отдела = new SelectList(db.Отдел, "Ид_отдела", "Наименование_отдела", сотрудники.Ид_отдела);
            return View(сотрудники);
        }

        // GET: Сотрудники/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сотрудники сотрудники = db.Сотрудники.Find(id);
            if (сотрудники == null)
            {
                return HttpNotFound();
            }
            return View(сотрудники);
        }

        // POST: Сотрудники/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Сотрудники сотрудники = db.Сотрудники.Find(id);

            try
            {
                db.Сотрудники.Remove(сотрудники);
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
