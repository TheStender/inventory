using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static int CreateOrderLine(int orderID, int productID, int totalQuantity)
        {
            var totalToSubtract = totalQuantity;
            var inventory = LoadInventory(productID);
            foreach(var bin in inventory)
            {
                int numberToSubtractFromBin = totalToSubtract <= bin.QTY ? totalToSubtract : bin.QTY;
                totalToSubtract = totalToSubtract - numberToSubtractFromBin;
                bin.QTY = bin.QTY - numberToSubtractFromBin;

                UpdateInventory(bin.InventoryID,
                    bin.ProductID,
                    bin.BinID,
                    bin.QTY);

                if(totalToSubtract <= 0)
                {
                    break;
                }
            }

            OrderLinesModel data = new OrderLinesModel
            {
                OrderID = orderID,
                ProductID = productID,
                QTY = totalQuantity
            };

            string sql = @"insert into dbo.orderlines (OrderID, ProductID, QTY)
                          values (@OrderID, @ProductID, @QTY);";

            //string sql = @"insert into dbo.orderlines (OrderID, ProductID, QTY) 
            //                values (@OrderID, @ProductID, @QTY);";

            return SqlDataAccess.Execute(sql, data);
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

            //string sql = @"update dbo.orderlines 
            //            set ProductID = @ProductID, QTY = @QTY 
            //            WHERE OrderLineID = @OrderLineID;";

            string sql = @"begin transaction; 
                           update dbo.orderlines 
                           set ProductID = @ProductID, QTY = @QTY 
                           WHERE OrderLineID = @OrderLineID;

                           update dbo.inventory 
                           set QTY = @qty 
                           WHERE ProductID = @ProductID;

                           commit;";

            return SqlDataAccess.Execute(sql, data);
        }

        public static int RemoveOrderLine(int orderLineID)
        {
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
