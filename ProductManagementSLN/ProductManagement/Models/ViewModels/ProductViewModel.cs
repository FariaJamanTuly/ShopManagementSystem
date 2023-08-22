using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace ProductManagement.Models.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        [Display(Name ="Product Id")]
        public int ProductId { get; set; }
        [Display(Name = "Product Name")]
       // [StringLength()]
        [Required(ErrorMessage ="Product Name Required")]
        public string ProductName { get; set; }
        [Display(Name = "Purchase Date")]
        [Required(ErrorMessage = "Purchase Date Required")]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="{0:MM/dd/yyyy}")]
        [ValidatePurchaseDate]
        public System.DateTime PurchaseDate { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address Required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Quantity Required")]
        [Range(1, 100, ErrorMessage ="Quantity must be within 1-100")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Unit Price Required")]
        public decimal UnitPrice { get; set; }       
        [Required(ErrorMessage = "Supplier Required")]
        public int SupplierId { get; set; }
        public string PageTitle { get; set; }
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }
        [Display(Name = "Image Name")]
        public string ImageName { get; set; }
        [Display(Name = "Product Image")]
        public string ImageUrl { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

    }
}