using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PropertyManagement1.Models;

namespace PropertyManagement1.Areas.Admin
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login objUser)
        {
            if (ModelState.IsValid)
            {
                
                using (PPCDB2Entities1 db = new PPCDB2Entities1())
                {
                    var obj = db.Logins.Where(a => a.username.Equals(objUser.username) && a.password.Equals(objUser.password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.id.ToString();
                        Session["UserName"] = obj.username.ToString();
                        return RedirectToAction("Index", "PropertyAdmin");
                        
                    } else
                    {
                        return RedirectToAction("Index", "Login");

                    }
                }
                
            }
            else {

                
            }
            return View(objUser);
        }

        //public ActionResult UserDashBoard()
        //{
        //    if (Session["UserID"] != null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}
        
    }
} 
  