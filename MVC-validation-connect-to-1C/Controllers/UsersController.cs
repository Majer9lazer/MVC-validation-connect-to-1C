using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Company.Register.Lib;
using Company.Register.Lib.Model;
using DataBase_model.Model;
using MVC_validation_connect_to_1C.Models;
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
        public ActionResult Create([Bind(Include = "UserId,Login,Password,RePassword,Email,EmailElInvoice,IsLegalEntity,Bin,Kbe,CertSeries,CertNumber,CertDateIssue,Status,CreateDate,UpdateDate,NameOrganization,User1CGuid,Surname,Name,Patronymic,AddressLegal")] LocaUser user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (db.Users.Any(f => f.Login == user.Login))
                    {
                        ViewData["ErrorMessage"] = "Данный пользователь уже есть.";
                        return View(user);
                    }
                    else
                    {
                        if (user.IsLegalEntity == 2)
                        {
                            if (string.IsNullOrEmpty(user.NameOrganization))
                            {
                                ViewData["ErrorMessage"] = "Поле NameOrganization обязательно к заполнению.";
                                return View(user);
                            }
                            else
                            {
                                user.Surname = null;
                                user.NameOrganization = null;
                                user.Patronymic = null;
                                if (!string.IsNullOrEmpty(user.AddressLegal.House) ||
                                    !string.IsNullOrEmpty(user.AddressLegal.Street))
                                {
                                    user.AddressPhysical.House = user.AddressLegal.House;
                                    user.AddressPhysical.Street = user.AddressLegal.Street;
                                    UserAccount a = (UserAccount)user;
                                    a.BankDetails[0]= new BankDetail(){Bik = "", CurrencyId= 1, AccountNumber = ""};
                                    a.ContactNumbers[0] = new Company.Register.Lib.Model.Phone(){PhoneCode = "7", PhoneNumber = "777 77 77", PhoneTypeId = 1, СountryСode = "727"};
                                    
                                    ViewData["ErrorMessage"] = CompanyRegisterService.RegisterContractors(a);
                                    return View(user);
                                }
                                else
                                {
                                    ViewData["ErrorMessage"] = "Поле дом и улица должны быть заполненными";
                                    return View(user);
                                }
                            }
                        }
                    }


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
