using DataLibrary.DataAccess;
using DataLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataLibrary.BusinessLogic
{
    public static class InventoryProcessor
    {
        public static void CreateInventory(int inventoryID, int productID, int binID, int qty)
        {
            // test
            var inventory = LoadInventory(productID);
            foreach (var bin in inventory)
            {
                if (binID == bin.BinID)
                {
                    UpdateInventory(inventoryID, productID, binID, qty);
                } else
                {
                    InventoryModel data = new InventoryModel
                    {
                        InventoryID = inventoryID,
                        ProductID = productID,
                        BinID = binID,
                        QTY = qty
                    };

                    string sql = @"insert into dbo.inventory (ProductID, BinID, QTY)
                           values (@ProductID, @BinID, @QTY);";

                    SqlDataAccess.Execute(sql, data);
                }
            }
            //end test

            //code below is original code

            //InventoryModel data = new InventoryModel
            //{
            //    InventoryID = inventoryID,
            //    ProductID = productID,
            //    BinID = binID,
            //    QTY = qty
            //};

            //string sql = @"insert into dbo.inventory (ProductID, BinID, QTY)
            //               values (@ProductID, @BinID, @QTY);";

            //SqlDataAccess.Execute(sql, data);
        }

        public static List<InventoryModel> LoadInventory()
        {
            string sql = @"select InventoryID, ProductID, BinID, QTY
                            from dbo.inventory;";

            return SqlDataAccess.Query<InventoryModel>(sql);
        }

        public static List<InventoryModel> LoadInventory(int productID)
        {

            string sql = @"select InventoryID, ProductID, BinID, QTY
                            from dbo.inventory WHERE ProductID = @ProductID;";

            return SqlDataAccess.Query<InventoryModel>(sql, new { ProductID = productID});
        }

        public static void UpdateInventory(int inventoryID, int productID, int binID, int qty)
        {
            // Remove inventory when quantity goes below zero
            //if(qty <= 0)
            //{
            //    RemoveInventory(inventoryID);
            //    return;
            //}

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

            SqlDataAccess.Execute(sql, data);
        }

        public static void RemoveInventory(int inventoryID)
        {
            InventoryModel data = new InventoryModel
            {
                InventoryID = inventoryID
            };

            string sql = @"delete from dbo.inventory 
                          WHERE InventoryID = @InventoryID;";

            SqlDataAccess.Execute(sql, data);
        }

        public static int CheckInventory(int productID)
        {
            InventoryModel data = new InventoryModel
            {
                ProductID = productID
            };

            string sql = @"select sum(QTY) from dbo.inventory
                          WHERE ProductID = @ProductID";

            var inventory = SqlDataAccess.QueryScalar(sql, data);
            return inventory;
        }

        public static void AdjustInventory(int productID, int AmountToTake)
        {
            if(AmountToTake > 0)
            {
                TakeOutInventory(productID, AmountToTake);
            }
            else if (AmountToTake < 0)
            {
                PutBackInventory(productID, AmountToTake * -1);
            }
        }

        private static void TakeOutInventory(int productID, int totalToTake)
        {
            var totalToSubtract = totalToTake;
            var inventory = LoadInventory(productID);
            foreach (var bin in inventory)
            {
                int numberToSubtractFromBin = totalToSubtract <= bin.QTY ? totalToSubtract : bin.QTY;
                totalToSubtract = totalToSubtract - numberToSubtractFromBin;
                bin.QTY = bin.QTY - numberToSubtractFromBin;

                UpdateInventory(bin.InventoryID,
                    bin.ProductID,
                    bin.BinID,
                    bin.QTY);

                if (totalToSubtract <= 0)
                {
                    break;
                }
            }
        }

        private static void PutBackInventory(int productID, int totalToPutBack)
        {
            var totalToAdd = totalToPutBack;
            var inventory = LoadInventory(productID);

            var lowestInventoryBin = inventory.OrderBy(bin => bin.QTY).First();

            UpdateInventory(lowestInventoryBin.InventoryID,
                lowestInventoryBin.ProductID,
                lowestInventoryBin.BinID,
                lowestInventoryBin.QTY + totalToAdd);
        }
    }
}
