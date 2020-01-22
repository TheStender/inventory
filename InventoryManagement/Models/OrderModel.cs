using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models
{
    public class OrderModel
    {
        [Display(Name = "Order ID")]
        [Range(0, 9999999, ErrorMessage = "Please enter a valid order ID(0-9999999)")]
        [Required(ErrorMessage = "Please enter an order ID")]
        public int OrderID { get; set; }

        [Display(Name = "Order Number")]
        [Required(ErrorMessage = "Please enter an order number")]
        public string OrderNumber { get; set; }

        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter an order date")]
        public DateTime DateOrdered { get; set; }

        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "Please enter the customer's name")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Address")]
        [Required(ErrorMessage = "Please enter the customer's address")]
        public string CustomerAddress { get; set; }
    }
}