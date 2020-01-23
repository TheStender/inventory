using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models
{
    public class ProductModel
    {
        [Display(Name = "Product ID")]
        [Range(1, 9999999, ErrorMessage = "Please enter a valid product number(1-9999999)")]
        [Required(ErrorMessage = "Please enter a product number")]
        public int ProductID { get; set; }

        [Display(Name = "Product SKU")]
        [Required(ErrorMessage = "Please enter a product SKU")]
        public string SKU { get; set; }

        [Display(Name = "Product Description")]
        [Required(ErrorMessage = "Please enter a description of the product")]
        public string ProductDescription { get; set; }
    }
}