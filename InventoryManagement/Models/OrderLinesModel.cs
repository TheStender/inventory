using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models
{
    public class OrderLinesModel
    {
        [Display(Name = "Order Line ID")]
        [Required(ErrorMessage = "Please enter an order line ID")]
        public int OrderLineID { get; set; }

        [Display(Name = "Order ID")]
        [Range(0, 9999999, ErrorMessage = "Please enter a valid order ID(0-9999999)")]
        [Required(ErrorMessage = "Please enter an order ID")]
        public int OrderID { get; set; }

        [Display(Name = "Product ID")]
        [Range(1, 9999999, ErrorMessage = "Please enter a valid product number(0-9999999)")]
        [Required(ErrorMessage = "Please enter a product number")]
        public int ProductID { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Please enter the quantity")]
        public int QTY { get; set; }
    }
}