using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    public class SupplierController : Controller
    {
        ProductManagemenrDBEntities db = new ProductManagemenrDBEntities();
        public ActionResult Index()
        {
            List<Supplier> suppliers = db.Suppliers.ToList();
            return View(suppliers);
        }
        public ActionResult CreateSupplier()
        {
            return View();
        }
        public ActionResult AjaxCreateSupplier(Supplier obj) 
        { 
            if(ModelState.IsValid)
            {
                System.Threading.Thread.Sleep(5000);
                db.Suppliers.Add(obj);
                db.SaveChanges();
            }
            IEnumerable<Supplier> Model = db.Suppliers.ToList();
            return PartialView("_SupplierDetails", Model);
        }
    }
}