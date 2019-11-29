using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PropertyManagement1.Models;

namespace PropertyManagement1.Controllers
{
    public class HomeController : Controller
    {
        PPCDB2Entities1 db = new PPCDB2Entities1();
        // GET: Home
        public ActionResult Index()
        {
            var lslPro = db.Property.Take(8).ToList(); 
            return View(lslPro);
        }
        public ActionResult Detais(int id)
        {
            Property property = db.Property.SingleOrDefault(n => n.ID == id);
            if (property == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(property);

        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Listing()
        {
            return View();
        }
    }
}