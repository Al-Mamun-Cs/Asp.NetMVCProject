using AuthATMProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AuthATMProject.Controllers
{
    public class OrdersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            using (var db = new ApplicationDbContext())
            {
                List<AcTitle> playerAndGrade = db.acTitle.Include("AcHolders").ToList();
                return View(playerAndGrade);
            }

        }
        //[HttpGet]
        //public JsonResult GetAllGetegory()
        //{

        //    var dataList = db.Grades.Where(x => x.GradeId == 1).ToList();
        //    //var dataList = db.Categories.ToList();
        //    return Json(dataList, JsonRequestBehavior.AllowGet);
        //}
        [HttpGet]
        public ActionResult GetAllGrade()
        {

            return Json(db.acTitle.Select(x => new
            {
                GradeId = x.AcTitleId,
                Name = x.AccountType
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveOrder(string name, AcHolder[] acHolders)
        {
            using (var db = new ApplicationDbContext())
            {
                string result = "Error! AcHolder Is Not Complete!";
                if (name != null && acHolders != null)
                {
                    //var GradeId = Guid.NewGuid();
                    AcTitle model = new AcTitle();

                    model.AccountType = name;
                    db.acTitle.Add(model);

                    foreach (var item in acHolders)
                    {
                        //var PlayerId = Guid.NewGuid();
                        AcHolder O = new AcHolder();

                        O.AcHolderName = item.AcHolderName;
                        O.Email = item.Email;
                        O.Phone = item.Phone;
                        O.DoB = item.DoB;
                        O.Amount = item.Amount;
                        O.AcTitleId = item.AcTitleId;
                        db.AcHolders.Add(O);
                    }
                    db.SaveChanges();
                    result = "Success! Order Is Complete!";
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult EditOrder(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AcHolder acHolders = db.AcHolders.Find(id);
                if (acHolders == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CustomerId = new SelectList(db.acTitle, "AcTitleId", "AccountType", acHolders.AcTitleId);
                return View(acHolders);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrder([Bind(Include = "AcHolderId,AcHolderName,Email,Phone,DoB,Amount,AcTitleId")] AcHolder acHolders)
        {
            using (var db = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(acHolders).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.acHoldersId = new SelectList(db.acTitle, "AcTitleId", "AccountType", acHolders.AcTitleId);
                return View(acHolders);
            }

        }



        public ActionResult EditGrade(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                AcTitle cus = db.acTitle.Find(id);
                if (cus == null)
                {
                    return HttpNotFound();
                }

                return View(cus);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer([Bind(Include = "AcTitleId,AccountType")] AcTitle acTitle)
        {
            using (var db = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(acTitle).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(acTitle);
            }
        }

    }
}