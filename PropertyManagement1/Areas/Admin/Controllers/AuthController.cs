using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PropertyManagement1.Models;

namespace PropertyManagement1.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        PPCDB2Entities1 db = new PPCDB2Entities1();
        // GET: Admin/Auth
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Account acc)
        {
            if (ModelState.IsValid)
            {
                var account = db.Accounts.Where(a => a.Username.Equals(acc.Username) && a.Password.Equals(acc.Password)).FirstOrDefault();
                if(account != null)
                {
                    return Redirect("/Admin/PropertyAdmin");
                }
            }
            return View( acc );
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session["ID"] = null;
            Session["Username"] = null;
            Session["Role"] = null;
            return RedirectToAction("Index","Login");
        }


    }
}