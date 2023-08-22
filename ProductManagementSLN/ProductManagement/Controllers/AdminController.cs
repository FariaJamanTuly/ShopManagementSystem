using ProductManagement.Models;
using ProductManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    [Authorize(Roles="Admin, SuperAdmin")]
    public class AdminController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult CreateRole()
        {
            using (var db = new ProductManagemenrDBEntities())
            {
                List<tblUser> users = db.tblUsers.ToList();
                ViewBag.Users = users;
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult CreateRole(tblRole obj)
        {
            using (var db = new ProductManagemenrDBEntities())
            {
                db.tblRoles.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                
        }
        public ActionResult Index()
        {
            using (var db = new ProductManagemenrDBEntities())
            {
                var userRoleList = (from user in db.tblUsers
                join role in db.tblRoles on user.Id equals role.UserId
                select new
                {
                    UserId=user.Id,
                    RoleId=role.Id,
                    UserName=user.UseName,
                    RoleName=role.RoleName
                }).ToList();
                List<UserRoleViewModel> list = new List<UserRoleViewModel>();
                foreach (var item in userRoleList)
                {
                    UserRoleViewModel obj=new UserRoleViewModel();
                    obj.UseName = item.UserName;
                    obj.RoleName = item.RoleName;
                    obj.RoleId=item.RoleId;
                    obj.UserId= item.UserId;
                    list.Add(obj);
                    
                }
                return View(list);
            }
            
        }
        public ActionResult Edit(int id)
        {
            using (var db = new ProductManagemenrDBEntities())
            {
                tblRole role=db.tblRoles.Find(id);
                List<tblUser> list=db.tblUsers.ToList();
                ViewBag.Users = new SelectList(list, "Id", "Username");
                ViewBag.User = list;
                return View(role);
            }
        }
    }
}