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
        public static int CreateOrder(string orderNumber, DateTime dateordered, string customerName, string customerAddress)
        {
            OrderModel data = new OrderModel
            {
                OrderNumber = orderNumber,
                DateOrdered = dateordered,
                CustomerName = customerName,
                CustomerAddress = customerAddress
            };

            string sql = @"insert into dbo.orders (OrderNumber, DateOrdered, CustomerName, CustomerAddress)
                           values (@OrderNumber, @DateOrdered, @CustomerName, @CustomerAddress); SELECT CAST(SCOPE_IDENTITY() as int)";

            return SqlDataAccess.Query<int>(sql, data).Single();
        }

        public static List<OrderModel> LoadOrders()
        {
            string sql = @"select OrderID, OrderNumber, DateOrdered, CustomerName, CustomerAddress
                            from dbo.orders;";

            return SqlDataAccess.Query<OrderModel>(sql);
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

            return SqlDataAccess.Execute(sql, data);
        }

        public static int RemoveOrder(int orderID)
        {
            OrderModel data = new OrderModel
            {
                OrderID = orderID
            };

            string sql = @"delete from dbo.orders 
                          WHERE OrderID = @OrderID;";

            return SqlDataAccess.Execute(sql, data);
        }
    }
}
