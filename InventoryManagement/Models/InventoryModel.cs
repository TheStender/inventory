using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models
{
    public class InventoryModel
    {
        [Display(Name = "Inventory ID")]
        [Required(ErrorMessage = "Please enter the Inventory ID")]
        public int InventoryID { get; set; }

        [Display(Name = "Product ID")]
        [Range(0, 9999999, ErrorMessage = "Please enter a valid product number(1-9999999)")]
        [Required(ErrorMessage = "Please enter the Product ID")]
        public int ProductID { get; set; }

        [Display(Name = "Bin ID")]
        [Range(0, 9999999, ErrorMessage = "Please enter a valid bin number(1-9999999)")]
        [Required(ErrorMessage = "Please enter the Bin ID")]
        public int BinID { get; set; }

        [Display(Name = "Quantity")]
        [Range(1, 9999999, ErrorMessage = "Please enter a valid quanity(1-9999999)")]
        [Required(ErrorMessage = "Please enter the quantity")]
        public int QTY { get; set; }
    }
}