using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class OrderProcessor
    {
        public static int CreateOrder(int orderID, string orderNumber, DateTime dateordered, string customerName, string customerAddress)
        {
            OrderModel data = new OrderModel
            {
                OrderID = orderID,
                OrderNumber = orderNumber,
                DateOrdered = dateordered,
                CustomerName = customerName,
                CustomerAddress = customerAddress
            };

            string sql = @"insert into dbo.orders (OrderID, OrderNumber, DateOrdered, CustomerName, CustomerAddress)
                           values (@OrderID, @OrderNumber, @DateOrdered, @CustomerName, @CustomerAddress);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<OrderModel> LoadOrders()
        {
            string sql = @"select OrderID, OrderNumber, DateOrdered, CustomerName, CustomerAddress
                            from dbo.orders;";

            return SqlDataAccess.LoadData<OrderModel>(sql);
        }

        public static int UpdateOrder(int orderID, string orderNumber, DateTime dateordered, string customerName, string customerAddress)
        {
            OrderModel data = new OrderModel
            {
                OrderID = orderID,
                OrderNumber = orderNumber,
                DateOrdered = dateordered,
                CustomerName = customerName,
                CustomerAddress = customerAddress
            };

            string sql = @"update dbo.orders 
                        set OrderNumber = @OrderNumber, DateOrdered = @DateOrdered, 
                        CustomerName = @CustomerName, CustomerAddress = @CustomerAddress 
                        WHERE OrderID = @OrderID;";

            return SqlDataAccess.SaveData(sql, data);
        }

        //public static int RemoveProduct(int productID, string sku, string description)
        //{
        //    ProductModel data = new ProductModel
        //    {
        //        ProductID = productID,
        //        SKU = sku,
        //        ProductDescription = description
        //    };

        //    string sql = @"delete from dbo.product 
        //                  WHERE ProductID = @ProductID;";

        //    return SqlDataAccess.SaveData(sql, data);
        //}
    }
}
