using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PropertyManagement1.Models;

namespace PropertyManagement1.Areas.Admin.Controllers
{
    public class PropertyAdminController : Controller
    {
        PPCDB2Entities1 db = new PPCDB2Entities1();
        // GET: Admin/PropertyAdmin
        public ActionResult Index()
        {
            var Property = db.Property.ToList();
            return View(Property);


        }
        public ActionResult Create()
        {
            PopularData();
            return View();
        }
        public void PopularData(object propertyTypeSelected = null, object districtSelected = null, object propertyStastusSelected = null)
        {
            ViewBag.Property_Type_ID = new SelectList(db.Property_Type.ToList(), "ID", "Property_Type_Name", propertyTypeSelected);
            ViewBag.District_ID = new SelectList(db.District.ToList(), "ID", "District_Name", districtSelected);
            ViewBag.Property_Status_ID = new SelectList(db.Property_Status.ToList(), "ID", "Property_Status_Name", propertyTypeSelected);
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID, Property_Code, Property_Name, Property_Type_ID, Description, District_ID, Address, Area, Bed_Room, Bath_Room, Price, Installment_Rate, Avatar, Album, Property_Status_ID")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Property.Add(property);
                db.SaveChanges();
                PopularMessage(true);
            }
            else
            {
                PopularMessage(false);
            }

            return Redirect("Index");
        }
        public void PopularMessage(bool success)
        {
            if (success)
                Session["success"] = "Successfull";
            else
                Session["success"] = "Fail";
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var property = db.Property.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
            PopularData(property.Property_Type_ID, property.Property_Status_ID);
            return View(property);
        }
        [HttpPost]
        public ActionResult Edit(int id,Property pp)
        {
            var Property = db.Property.ToList();
            try
            {
                var property = db.Property.Select(p => p).Where(p => p.ID ==id).FirstOrDefault();
                PopularData(property.Property_Type_ID, property.Property_Status_ID);
                property.ID = pp.ID;
                property.Property_Name = pp.Property_Name;
                property.Description = pp.Description;
                property.Address = pp.Address;
                property.Area = pp.Area;
                property.Bed_Room = pp.Bed_Room;
                property.Bath_Room = pp.Bath_Room;
                property.Price = pp.Price;
                property.Installment_Rate = pp.Installment_Rate;
                db.SaveChanges();
                return RedirectToAction("Index");

            } catch { 
                return View(Property); 
            }
            
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var property = db.Property.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
            return View(property);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection collection)
        {
            try
            {
                var property = db.Property.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
                if (property != null)
                {
                    db.Property.Remove(property);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");

            } catch { return View(); } 
        }
        public ActionResult Details(int id)
        {
            var property = db.Property.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
            return View(property);
        }
    }
}