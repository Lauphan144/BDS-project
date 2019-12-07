using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PropertyManagement1.Models;

namespace PropertyManagement1.Areas.Admin.Controllers
{
    public class InstallmentController : Controller
    {
        PPCDB2Entities1 db = new PPCDB2Entities1();
        // GET: Admin/Full
        public ActionResult Index()
        {
            var iC = db.Installment_Contract.ToList();
            return View(iC);
        }
        public ActionResult Print(int id)
        {
            var contract = db.Installment_Contract.FirstOrDefault(n => n.ID == id);
            if (contract != null)
            {
                InstallmentContractPrintModel fc1 = new InstallmentContractPrintModel();
                fc1.Installment_Contract_Code = contract.Installment_Contract_Code;
                fc1.Customer_Name = contract.Customer_Name;
                fc1.Customer_Address = contract.Customer_Address;
                fc1.Date_Of_Contract = contract.Date_Of_Contract;
                fc1.Mobile = contract.Mobile;
                fc1.Price = contract.Price;
                fc1.Desposit = contract.Deposit;

                fc1.Property_Code = contract.Property.Property_Code;
                fc1.Address = contract.Property.Address;

                return View(fc1);
            }
            else
                return View();
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
        public ActionResult Create([Bind(Include = "ID, Installment_Contract_Code, Customer_Name, Year_Of_Birth, SSN, Customer_Address, Mobile, Property_ID, Date_Of_Contract, Installment_Payment_Method, Payment_Period, Price, Deposit, Loan_Amount, Taken, Remain, Status")] Installment_Contract F)
        {
            try
            {

                db.Installment_Contract.Add(F);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch { return View(); }
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
            var iC = db.Installment_Contract.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
            return View(iC);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var iC = db.Installment_Contract.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
            return View(iC);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection collection)
        {
            try
            {
                var iC = db.Installment_Contract.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
                if (iC != null)
                {
                    db.Installment_Contract.Remove(iC);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");

            }
            catch { return View(); }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var iC = db.Installment_Contract.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
            PopularData(iC.Property_ID);
            return View(iC);
        }
        public ActionResult Edit(int id, Installment_Contract pp)
        {
            var iC = db.Installment_Contract.ToList();
            try
            {
                var IC = db.Installment_Contract.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
                PopularData(IC.Property_ID);
                
                IC.Installment_Contract_Code = pp.Installment_Contract_Code;
                IC.Customer_Name = pp.Customer_Name;
                IC.Year_Of_Birth = pp.Year_Of_Birth;
                IC.SSN = pp.SSN;
                IC.Customer_Address = pp.Customer_Address;
                IC.Mobile = pp.Mobile;
                IC.Date_Of_Contract = pp.Date_Of_Contract;
                IC.Installment_Payment_Method = pp.Installment_Payment_Method;
                IC.Payment_Period = pp.Payment_Period;
                IC.Price = pp.Price;
                IC.Deposit = pp.Deposit;
                IC.Loan_Amount = pp.Loan_Amount;
                IC.Taken = pp.Taken;
                IC.Remain = pp.Remain;
                IC.Status = pp.Status;

                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View(iC);
            }

        }
    }
}