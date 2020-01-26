using System;

namespace DataLibrary.Models
{
    public class OrderModel
    {
        public int OrderID { get; set; }
        public string OrderNumber { get; set; }
        public DateTime DateOrdered { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
    }
}
