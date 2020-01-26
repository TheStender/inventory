using DataLibrary.DataAccess;
using DataLibrary.Models;
using System.Collections.Generic;

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

            string sql = @"insert into dbo.bins (BinName)
                           values (@BinName);";

            return SqlDataAccess.Execute(sql, data);
        }

        public static List<BinModel> LoadBins()
        {
            string sql = @"select BinID, BinName
                            from dbo.bins;";

            return SqlDataAccess.Query<BinModel>(sql);
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

            return SqlDataAccess.Execute(sql, data);
        }

        public static int RemoveBin(int binID)
        {
            BinModel data = new BinModel
            {
                BinID = binID
            };

            string sql = @"delete from dbo.bins 
                          WHERE BinID = @BinID;";

            return SqlDataAccess.Execute(sql, data);
        }
    }
}
