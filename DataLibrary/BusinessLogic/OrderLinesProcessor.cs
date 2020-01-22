using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class OrderLinesProcessor
    {
        public static List<OrderLinesModel> LoadOrderLines()
        {
            string sql = @"select OrderLineID, OrderID, ProductID, QTY
                        from dbo.orderlines;";

            return SqlDataAccess.LoadData<OrderLinesModel>(sql);
        }
    }
}
