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
        public ActionResult Login(Account objUser)
        {
            if (ModelState.IsValid)
            {
                
                using (PPCDB2Entities1 db = new PPCDB2Entities1())
                {
                    var obj = db.Accounts.Where(a => a.Username.Equals(objUser.Username) && a.Password.Equals(objUser.Username)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.ID.ToString();
                        Session["UserName"] = obj.Username.ToString();
                        Session["Role"] = obj.Role.ToString();
                        return RedirectToAction("Index", "PropertyAdmin");
                        
                    } else
                    {
                        return RedirectToAction("Index", "Login");

                    }
                }
                
            }
           
            return View(objUser);
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session["ID"] = null;
            Session["Username"] = null;
            Session["Role"] = null;
            return RedirectToAction("Index", "Login");
        }

        private ActionResult RedirectToLocal(string returnURL = "")
        {
            try
            {
                // If the return url starts with a slash "/" we assume it belongs to our site
                // so we will redirect to this "action"
                if (!string.IsNullOrWhiteSpace(returnURL) && Url.IsLocalUrl(returnURL))
                    return Redirect(returnURL);

                // If we cannot verify if the url is local to our host we redirect to a default location
                return RedirectToAction("Index", "Login");
            }
            catch
            {
                throw;
            }
            
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
  