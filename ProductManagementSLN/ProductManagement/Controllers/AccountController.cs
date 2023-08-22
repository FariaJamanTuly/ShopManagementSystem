using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProductManagement.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
       // ProductManagemenrDBEntities db = new ProductManagemenrDBEntities();
        [HttpGet]
        
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult lgout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        
        public ActionResult Login(tblUser obj)
        {
            using (var db = new ProductManagemenrDBEntities())
            {
                bool isVaild = db.tblUsers.Any(u => u.UseName == obj.UseName && u.UserPassword==obj.UserPassword);
                if (isVaild)
                {
                    FormsAuthentication.SetAuthCookie(obj.UseName, false);
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("","Invalid User");
                    return View();
                }
            }
           
        }
        [HttpGet]
        
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
       
        public ActionResult Signup(tblUser obj)
        {
            using (var db= new ProductManagemenrDBEntities())
            {
                bool isVaild=!db.tblUsers.Any(u=>u.UseName==obj.UseName);
                if(isVaild)
                {
                    db.tblUsers.Add(obj);
                    db.SaveChanges();
                    int count=db.tblUsers.Count();
                    if(count==1) 
                    {
                        return RedirectToAction("CreateRole","Admin");
                    }
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("","User already available");
                    return View();
                }
            }
            //return View();
        }
    }
}