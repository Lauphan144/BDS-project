using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PropertyManagement1.Models;   

namespace PropertyManagement1.Areas.Admin.Controllers
{
    public class FullController : Controller
    {
        PPCDB2Entities1 db = new PPCDB2Entities1();
        // GET: Admin/Full
        public ActionResult Index()
        {
            var full = db.Full_Contract.ToList();
            return View(full);
        }
        public ActionResult Create()
        {
            PopularData();
            return View();
        }
        public void PopularData(object propertyIDSelected = null)
        {
            ViewBag.Property_ID = new SelectList(db.Property.ToList(), "ID", "Property_Name", propertyIDSelected);
            
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,  Customer_Name, Year_Of_Birth, SSN, Customer_Address, Mobile, Property_ID, Date_Of_Contract, Price, Deposit, Remain, Status")] Full_Contract F)
        {
            try
            {
               
                db.Full_Contract.Add(F);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            } catch { return View(); }
        }
        public void PopularMessage(bool success)
        {
            if (success)
                Session["success"] = "Successfull";
            else
                Session["success"] = "Fail";
        }
        public ActionResult Details(int id)
        {
            var fullC = db.Full_Contract.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
            return View(fullC);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var fullC = db.Full_Contract.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
            return View(fullC);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection collection)
        {
            try
            {
                var fullC = db.Full_Contract.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
                if (fullC != null)
                {
                    db.Full_Contract.Remove(fullC);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");

            }
            catch { return View(); }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var fullC = db.Full_Contract.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
            PopularData(fullC.Property_ID);
            return View(fullC);
        }
        public ActionResult Edit(int id, Full_Contract pp)
        {
            var fullC = db.Full_Contract.ToList();
            try
            {
                var FullC = db.Full_Contract.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
                PopularData(FullC.Property_ID);
                FullC.ID = pp.ID;
                FullC.Full_Contract_Code = pp.Full_Contract_Code;
                FullC.Customer_Name = pp.Customer_Name;
                FullC.Year_Of_Birth = pp.Year_Of_Birth;
                FullC.SSN = pp.SSN;
                FullC.Customer_Address = pp.Customer_Address;
                FullC.Mobile = pp.Mobile;
                FullC.Date_Of_Contract = pp.Date_Of_Contract;
                FullC.Price = pp.Price;
                FullC.Deposit = pp.Deposit;
                FullC.Remain = pp.Remain;
                FullC.Status = pp.Status;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View(fullC);
            }

        }
    }
}