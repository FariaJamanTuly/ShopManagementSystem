using ProductManagement.Models;
using ProductManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductManagement.Repository
{
    public class ProductRepository: IProductRepository
    {
        ProductManagemenrDBEntities db=new ProductManagemenrDBEntities();
        public IEnumerable<ProductViewModel> GetAllProduct() 
        { 
            List<ProductViewModel> list=db.Products.Select(p => new ProductViewModel 
            { 
                ProductId=p.ProductId,
                ProductName=p.ProductName,
                PurchaseDate=p.PurchaseDate,
                SupplierId=p.SupplierId,
                EmailAddress=p.EmailAddress,
                ImageName=p.ImageName,
                Quantity=p.Quantity,
                UnitPrice=p.UnitPrice,
                ImageUrl=p.ImageUrl,
                SupplierName=p.Supplier.SupplierName,
                PageTitle="Product List"
            }).ToList();
            return list;
        }
        public void SaveProduct(Product obj) 
        {
            db.Products.Add(obj);
            db.SaveChanges();
        }
        public Product GetProductById(int id) 
        {
            Product obj = db.Products.SingleOrDefault(p => p.ProductId == id);
            return obj;
        }
        public void UpdateProduct(Product obj)
        {
            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void DeleteProduct( int id)
        {
            Product obj = GetProductById(id);
            db.Products.Remove(obj);
            db.SaveChanges();
        }
        public List<Supplier> GetSuppliers()
        {
            List<Supplier> list = db.Suppliers.ToList();
            return list;
        }
        public void SaveSupplier(Supplier obj)
        {
            db.Suppliers.Add(obj);
            db.SaveChanges();
        }
    }
}