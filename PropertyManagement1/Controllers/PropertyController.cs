using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PropertyManagement1.Models;
using PagedList;
using PagedList.Mvc;

namespace PropertyManagement1.Controllers
{
   
    public class PropertyController : Controller
    {
        PPCDB2Entities1 db = new PPCDB2Entities1();
        // GET: Property
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(db.Property.ToList().OrderBy(n => n.Price).ToPagedList(pageNumber,pageSize));
        }
        public ViewResult Detail(int id)
        {
            Property property = db.Property.SingleOrDefault(n => n.ID == id);
            if (property == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(property);
                
        }                                
    }
}