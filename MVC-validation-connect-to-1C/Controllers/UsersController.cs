using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Company.Register.Lib;
using Company.Register.Lib.Model;
using DataBase_model.Model;
using MVC_validation_connect_to_1C.Models;
using Phone = Company.Register.Lib.Model.Phone;
using User = DataBase_model.Model.User;

namespace MVC_validation_connect_to_1C.Controllers
{
    public class UsersController : Controller
    {
        private AvisWeb1Entities db = new AvisWeb1Entities();

        public UsersController()
        {
            ViewData["ErrorMessage"] = 0;
        }
        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Login,Bik,Password,RePassword,Email,EmailElInvoice,IsLegalEntity,ContactNumbers,Bin,Kbe,CertSeries,CertNumber,CertDateIssue,Status,NameOrganization,User1CGuid,Surname,Name,Patronymic,AddressLegal,PhoneNumber")] LocaUser user)
        {
            if (ModelState.IsValid)
            {
                user.CreateDate = DateTime.Now;
                user.UpdateDate = DateTime.Now;
                try
                {
                    if (db.Users.Any(f => f.Login == user.Login))
                    {
                        ViewData["ErrorMessage"] = "Данный пользователь уже есть.";
                        return View(user);
                    }

                    user.AddressPhysical.House = user.AddressLegal.House;
                    user.AddressPhysical.Street = user.AddressLegal.Street;
                    UserAccount a = (UserAccount)user;
                    a.BankDetails[0] = new BankDetail() { Bik = "", CurrencyId = 1, AccountNumber = "" };
                    
                    user.PhoneNumber.PhoneCode = "7";
                    user.PhoneNumber.PhoneTypeId = 1;
                    user.PhoneNumber.СountryСode = "727";
                    a.ContactNumbers[0] = user.PhoneNumber;
                    ViewData["ErrorMessage"] = CompanyRegisterService.RegisterContractors(a);
                    return View(user);


                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    ViewData["ErrorMessage"] = e.ToString();
                }

                return RedirectToAction("Index");
            }


            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Login,Password,Email,EmailElInvoice,IsLegalEntity,Bin,Kbe,CertSeries,CertNumber,CertDateIssue,Status,CreateDate,UpdateDate,NameOrganization,User1CGuid,Surname,Name,Patronymic")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
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
