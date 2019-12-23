using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PropertyManagement1.Models;
using System.IO;
using System.Data.Entity;
using System.Transactions;


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
            ViewBag.Service_ID = new MultiSelectList(db.Service.ToList(), "ID", "Service_Name", null);
            return View();
        }

        public void PopularData(IQueryable<int> selectedService = null, object propertyTypeSelected = null, object districtSelected = null, object propertyStastusSelected = null, object listCitySelected = null)
        {
            ViewBag.CityList = new SelectList(db.City.ToList(), "ID", "City_Name", listCitySelected);
            
            ViewBag.Property_Type_ID = new SelectList(db.Property_Type.ToList(), "ID", "Property_Type_Name", propertyTypeSelected);
            ViewBag.District_ID = new SelectList(db.District.ToList(), "ID", "District_Name", districtSelected);
            ViewBag.Property_Status_ID = new SelectList(db.Property_Status.ToList(), "ID", "Property_Status_Name", propertyTypeSelected);
            ViewBag.Service_ID = new MultiSelectList(db.Service.ToList(), "ID", "Service_Name", selectedService);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID, Property_Code, Property_Name, Property_Type_ID, Description, District_ID, Address, Area, Bed_Room, Bath_Room, Price, Installment_Rate, Avatar, Album, Property_Status_ID")] Property property, List<HttpPostedFileBase> files, List<int> Service_ID)
        {


            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    string album = "";
                    var file = Request.Files["file"];

                    if (file != null)
                    {
                        foreach (var imageFile in files)
                        {
                            if (imageFile != null)
                            {
                                var fileName = DateTime.Now.Ticks + "-" + Path.GetFileName(imageFile.FileName);
                                var physicalPath = Path.Combine(Server.MapPath("~/Other/hinh/"), fileName);

                                // The files are not actually saved in this demo
                                imageFile.SaveAs(physicalPath);
                                album += album.Length > 0 ? ";" + fileName : fileName;
                            }
                        }
                    }
                    property.Album = album;
                    if (file != null)
                    {
                        var avatar = DateTime.Now.Ticks + "-" + Path.GetFileName(file.FileName);
                        var physicPath = Path.Combine(Server.MapPath("~/Other/hinh/"), avatar);
                        file.SaveAs(physicPath);
                        property.Avatar = avatar;
                    }
                    foreach (var item in Service_ID)
                    {

                        Property_Service properS = new Property_Service();
                        properS.Property_ID = property.ID;
                        properS.Service_ID = item;
                        db.Property_Service.Add(properS);
                    }
                    property.Installment_Rate = 0.7;


                    db.SaveChanges();
                    scope.Complete();
                    PopularMessage(true);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                PopularMessage(false);
            }
            ViewBag.Service_ID = new MultiSelectList(db.Service.ToList(), "ID", "Service_Name", null);

            PopularData();
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
            PopularData();
            return View(property);
        }
        [HttpPost]
        public ActionResult Edit(int id,Property pp, List<HttpPostedFileBase> files)
        {
            var Property = db.Property.ToList();
            try
            {

                string album = "";

                var file = Request.Files["file"];
                //up album
                if (files != null)
                {
                    foreach (var imageFile in files)
                    {
                        if (imageFile != null)
                        {
                            var fileName = DateTime.Now.Ticks + "-" + Path.GetFileName(imageFile.FileName);
                            var physicalPath = Path.Combine(Server.MapPath("~/Other/hinh"), fileName);

                            // The files are not actually saved in this demo
                            imageFile.SaveAs(physicalPath);
                            album += album.Length > 0 ? ";" + fileName : fileName;
                        }
                    }
                }
                pp.Album = album;
                //upload ảnh

                if (file != null)
                {
                    var avatar = DateTime.Now.Ticks + "-" + Path.GetFileName(file.FileName);
                    var physicPath = Path.Combine(Server.MapPath("~/Other/hinh"), avatar);
                    file.SaveAs(physicPath);
                    pp.Avatar = avatar;
                }





                var property = db.Property.Select(p => p).Where(p => p.ID ==id).FirstOrDefault();
                PopularData();
                property.Property_Name = pp.Property_Name;
                property.Property_Type_ID = pp.Property_Type_ID;
                property.Description = pp.Description;
                property.District_ID = pp.District_ID;
                property.Address = pp.Address;
                property.Area = pp.Area;
                property.Avatar = pp.Avatar;
                property.Album = pp.Album;
                property.Bath_Room = pp.Bath_Room;
                property.Bed_Room = pp.Bed_Room;
                property.Price = pp.Price;
                property.Installment_Rate = pp.Installment_Rate;
                property.Property_Status_ID = pp.Property_Status_ID;
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
        [HttpPost]
        public string deleteImage(string imageName, int id)
        {
            string fullPath = Request.MapPath("~/Other/hinh" + imageName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            var property = db.Property.FirstOrDefault(x => x.ID == id);
            var album = property.Album.Split(';');
            album = album.Where(w => w != imageName).ToArray();
            property.Album = string.Join(";", album);

            db.Entry(property).State = EntityState.Modified;
            db.SaveChanges();
            return property.Album;
        }
        public JsonResult GetDistrictByCityId(int id)
        {
            // Disable proxy creation
            db.Configuration.ProxyCreationEnabled = false;
            var listDistrict = db.District.Where(x => x.City_ID == id).ToList();
            return Json(listDistrict, JsonRequestBehavior.AllowGet);
        }
    }
}