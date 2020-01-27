using DataLibrary.DataAccess;
using DataLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using static DataLibrary.BusinessLogic.InventoryProcessor;

namespace DataLibrary.BusinessLogic
{
    public static class OrderLinesProcessor
    {
        public static List<OrderLinesModel> LoadOrderLines(int orderID)
        {
            string sql = @"select OrderLineID, OrderID, ProductID, QTY
                        from dbo.orderlines WHERE OrderID = @orderID ;";

            return SqlDataAccess.Query<OrderLinesModel>(sql, new { orderID = orderID });
        }

        public static OrderLinesModel LoadOrderLine(int orderLineID)
        {
            string sql = @"select OrderLineID, OrderID, ProductID, QTY
                        from dbo.orderlines WHERE OrderLineID = @orderLineID ;";

            return SqlDataAccess.Query<OrderLinesModel>(sql, new { orderLineID = orderLineID }).FirstOrDefault();
        }

        public static void CreateOrderLine(int orderID, int productID, int totalQuantity)
        {
            AdjustInventory(productID, totalQuantity);

            OrderLinesModel data = new OrderLinesModel
            {
                OrderID = orderID,
                ProductID = productID,
                QTY = totalQuantity
            };

            string sql = @"insert into dbo.orderlines (OrderID, ProductID, QTY)
                          values (@OrderID, @ProductID, @QTY);";
            try
            {
                SqlDataAccess.Execute(sql, data);
            }
            catch (System.Exception)
            {
                AdjustInventory(productID, totalQuantity * -1);
                throw;
            }  
        }

        public static int UpdateOrderLines(int orderLineID, int orderID, int productID, int qty)
        {
            OrderLinesModel data = new OrderLinesModel
            {
                OrderLineID = orderLineID,
                OrderID = orderID,
                ProductID = productID,
                QTY = qty
            };

            string sql = @"update dbo.orderlines 
                           set ProductID = @ProductID, QTY = @QTY 
                           WHERE OrderLineID = @OrderLineID;";

                return SqlDataAccess.Execute(sql, data);
        }

        public static int RemoveOrderLine(int orderLineID)
        {
            var previousState = LoadOrderLine(orderLineID);
            AdjustInventory(previousState.ProductID, previousState.QTY * -1);

            OrderLinesModel data = new OrderLinesModel
            {
                OrderLineID = orderLineID
            };

            string sql = @"delete from dbo.orderlines 
                          WHERE OrderLineID = @OrderLineID;";

            return SqlDataAccess.Execute(sql, data);
        }

    }
}
