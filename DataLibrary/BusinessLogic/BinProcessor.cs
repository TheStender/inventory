using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class BinProcessor
    {
        public static int CreateBin(int binID, string binName)
        {
            BinModel data = new BinModel
            {
                BinID = binID,
                BinName = binName
            };

            string sql = @"insert into dbo.bins (BinID, BinName)
                           values (@BinID, @BinName);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<BinModel> LoadBins()
        {
            string sql = @"select BinID, BinName
                            from dbo.bins;";

            return SqlDataAccess.LoadData<BinModel>(sql);
        }

        public static int UpdateBin(int binID, string binName)
        {
            BinModel data = new BinModel
            {
                BinID = binID,
                BinName = binName
            };

            string sql = @"update dbo.bins 
                        set BinName = @BinName WHERE BinID = @BinID;";

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
