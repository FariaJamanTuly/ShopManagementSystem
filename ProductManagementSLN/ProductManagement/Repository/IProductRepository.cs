using ProductManagement.Models;
using ProductManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Repository
{
    public interface IProductRepository
    {
        IEnumerable<ProductViewModel> GetAllProduct();
        void SaveProduct(Product obj);
        Product GetProductById(int id);
        void UpdateProduct(Product obj);
        void DeleteProduct(int id);
        List<Supplier> GetSuppliers();
        void SaveSupplier(Supplier obj);
    }
}
