using ProductManagement.Models;
using ProductManagement.Models.ViewModels;
using ProductManagement.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{

    public class ProductController : Controller
    {
        ProductRepository repo=new ProductRepository();
        [Authorize(Roles ="View, Admin")]
        public ActionResult Index()
        {
            IEnumerable<ProductViewModel> list = repo.GetAllProduct();
            return View(list);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            List<Supplier> list=repo.GetSuppliers();
            ViewBag.Suppliers=list;
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            List<Supplier> list = repo.GetSuppliers();
            ViewBag.Suppliers = list;
            Product vObj = repo.GetProductById(id);
            ProductViewModel obj=new ProductViewModel();
            obj.ProductName = vObj.ProductName;
            obj.PurchaseDate = vObj.PurchaseDate;
            obj.Quantity = vObj.Quantity;
            obj.UnitPrice = vObj.UnitPrice;
            obj.SupplierId = vObj.SupplierId;
            obj.EmailAddress = vObj.EmailAddress;
            obj.ImageName = vObj.ImageName;
            obj.ImageUrl = vObj.ImageUrl;
            obj.ProductId = vObj.ProductId;
            return View(obj);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(ProductViewModel vObj)
        {
            bool result = false;
            Product obj;
            if (vObj.ProductId == 0)
            {
                if (ModelState.IsValid)
                {
                    obj = new Product();
                    obj.ProductName = vObj.ProductName;
                    obj.PurchaseDate = vObj.PurchaseDate;
                    obj.Quantity = vObj.Quantity;
                    obj.UnitPrice = vObj.UnitPrice;
                    obj.SupplierId = vObj.SupplierId;
                    obj.EmailAddress = vObj.EmailAddress;
                    string fileName = Path.GetFileNameWithoutExtension(vObj.ImageFile.FileName);
                    string extension = Path.GetExtension(vObj.ImageFile.FileName);
                    fileName = fileName + extension;
                    obj.ImageName = fileName;
                    obj.ImageUrl = "~/Images/" + obj.ImageName;
                    fileName = Path.Combine(Server.MapPath(obj.ImageUrl));
                    vObj.ImageFile.SaveAs(fileName);
                    repo.SaveProduct(obj);
                    result = true;
                }
            }
            else
            {
                obj=repo.GetProductById(vObj.ProductId);
                obj.ProductName = vObj.ProductName;
                obj.PurchaseDate = vObj.PurchaseDate;
                obj.Quantity = vObj.Quantity;
                obj.UnitPrice = vObj.UnitPrice;
                obj.SupplierId = vObj.SupplierId;
                obj.EmailAddress = vObj.EmailAddress;
                obj.ProductId = vObj.ProductId;
                if (vObj.ImageFile != null)
                {
                    DeleteExistingImage(obj.ImageName);
                    string fileName = Path.GetFileNameWithoutExtension(vObj.ImageFile.FileName);
                    string extension = Path.GetExtension(vObj.ImageFile.FileName);
                    fileName = fileName + extension;
                    obj.ImageName = fileName;
                    obj.ImageUrl = "~/Images/" + obj.ImageName;
                    fileName = Path.Combine(Server.MapPath(obj.ImageUrl));
                    vObj.ImageFile.SaveAs(fileName);
                }
                else
                {
                    obj.ImageName=obj.ImageName;
                    obj.ImageUrl=obj.ImageUrl;
                }
                repo.UpdateProduct(obj);
                result = true;
            }
            if(result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                List<Supplier> suppliers = repo.GetSuppliers();
                ViewBag.Suppliers = suppliers;
                return View();
            }          
           
        }
        public ActionResult Delete(int id)
        {
            List<Supplier> list = repo.GetSuppliers();
            ViewBag.Suppliers = list;
            Product vObj = repo.GetProductById(id);
            ProductViewModel obj = new ProductViewModel();
            obj.ProductName = vObj.ProductName;
            obj.PurchaseDate = vObj.PurchaseDate;
            obj.Quantity = vObj.Quantity;
            obj.UnitPrice = vObj.UnitPrice;
            obj.SupplierId = vObj.SupplierId;
            obj.EmailAddress = vObj.EmailAddress;
            obj.ImageName = vObj.ImageName;
            obj.ImageUrl = vObj.ImageUrl;
            obj.ProductId = vObj.ProductId;
            return View(obj);
        }
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirm(ProductViewModel vObj)
        {
            bool result=false;
            Product obj=repo.GetProductById(vObj.ProductId);
            string imageName = vObj.ImageName;
            DeleteExistingImage(imageName);
            try
            {
                repo.DeleteProduct(obj.ProductId);
                result = true;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                result = false;
                return View(vObj);
            }           

           
        }
        public PartialViewResult Details(int id)
        {
            List<Supplier> list = repo.GetSuppliers();
            ViewBag.Suppliers = list;
            Product vObj = repo.GetProductById(id);
            ProductViewModel obj = new ProductViewModel();
            obj.ProductName = vObj.ProductName;
            obj.PurchaseDate = vObj.PurchaseDate;
            obj.Quantity = vObj.Quantity;
            obj.UnitPrice = vObj.UnitPrice;
            obj.SupplierId = vObj.SupplierId;
            obj.EmailAddress = vObj.EmailAddress;
            obj.ImageName = vObj.ImageName;
            obj.ImageUrl = vObj.ImageUrl;
            obj.ProductId = vObj.ProductId;
            ViewBag.Details = "Show";
            return PartialView("_DetailsRecord", obj);
        }
        private void DeleteExistingImage(string imageName)
        {
            string root=Server.MapPath("~");
            string parent=Path.GetDirectoryName(root);
            string path = parent + "/Images/" + imageName;
            FileInfo fileObj= new FileInfo(path);
            if (fileObj.Exists)
            {
                fileObj.Delete();
            }
        }
    }
}