using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class InventoryProcessor
    {
        public static int CreateInventory(int inventoryID, int productID, int binID, int qty)
        {
            InventoryModel data = new InventoryModel
            {
                InventoryID = inventoryID,
                ProductID = productID,
                BinID = binID,
                QTY = qty
            };

            string sql = @"insert into dbo.inventory (InventoryID, ProductID, BinID, QTY)
                           values (@InventoryID, @ProductID, @BinID, @QTY);";

            return SqlDataAccess.Execute(sql, data);
        }

        public static List<InventoryModel> LoadInventory()
        {
            string sql = @"select InventoryID, ProductID, BinID, QTY
                            from dbo.inventory;";

            return SqlDataAccess.Query<InventoryModel>(sql);
        }

        public static int UpdateInventory(int inventoryID, int productID, int binID, int qty)
        {
            InventoryModel data = new InventoryModel
            {
                InventoryID = inventoryID,
                ProductID = productID,
                BinID = binID,
                QTY = qty
            };

            string sql = @"update dbo.inventory 
                        set ProductID = @ProductID, BinID = @BinID, 
                        QTY = @qty 
                        WHERE InventoryID = @InventoryID;";

            return SqlDataAccess.Execute(sql, data);
        }

        public static int RemoveInventory(int inventoryID)
        {
            InventoryModel data = new InventoryModel
            {
                InventoryID = inventoryID
            };

            string sql = @"delete from dbo.inventory 
                          WHERE InventoryID = @InventoryID;";

            return SqlDataAccess.Execute(sql, data);
        }
    }
}
