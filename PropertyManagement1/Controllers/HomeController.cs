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
    public class HomeController : Controller
    {
        PPCDB2Entities1 db = new PPCDB2Entities1();
        // GET: Home
        public ActionResult Index()
        {
            
            var lslPro = db.Property.Take(3).OrderBy(n => n.Price).ToList();
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
        public ActionResult Listing(int? page)
        {
            if (page == null) page = 1;
            var links = (from l in db.Property
                         select l).OrderBy(x => x.ID);
            
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(links.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult News(int? page)
        {
            if (page == null) page = 1;
            var links = (from l in db.Property
                         select l).OrderBy(x => x.ID);

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(links.ToPagedList(pageNumber, pageSize));


        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string search)
        {
            var p = db.Property.ToList().Where(x => x.Property_Name.Contains(search));
            

            var property = from b in db.Property select b;

            if (!String.IsNullOrEmpty(search))
            {
                property = db.Property.Where(b => b.Property_Name.Contains(search));
            }
            
            return View(property.ToList());
        }
    }
}