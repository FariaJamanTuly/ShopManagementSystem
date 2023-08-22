using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class ValidatePurchaseDate:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime currentDate= DateTime.Now;
            string message = "";
            if (Convert.ToDateTime(value) <= currentDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                message = "Purchase date cannot be Greater than current date";
                return new ValidationResult(message);
            }
        }
    }
}