using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class ProductProcessor
    {
        public static int CreateProduct(int productID, string sku, string description)
        {
            ProductModel data = new ProductModel
            {
                ProductID = productID,
                SKU = sku,
                ProductDescription = description
            };

            string sql = @"insert into dbo.product (ProductID, SKU, ProductDescription)
                           values (@ProductID, @SKU, @ProductDescription);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<ProductModel> LoadProducts()
        {
            string sql = @"select ProductID, SKU, ProductDescription
                            from dbo.product;";

            return SqlDataAccess.LoadData<ProductModel>(sql);
        }

        //test edit
        public static int UpdateProduct(int productID, string sku, string description)
        {
            ProductModel data = new ProductModel
            {
                ProductID = productID,
                SKU = sku,
                ProductDescription = description
            };

            string sql = @"update dbo.product 
                        set SKU = @SKU, ProductDescription = @ProductDescription
                        WHERE ProductID = @ProductID;";

            return SqlDataAccess.SaveData(sql, data);
        }

        //test delete
        public static int RemoveProduct(int productID, string sku, string description)
        {
            ProductModel data = new ProductModel
            {
                ProductID = productID,
                SKU = sku,
                ProductDescription = description
            };

            string sql = @"delete from dbo.product 
                          WHERE ProductID = @ProductID;";

            return SqlDataAccess.SaveData(sql, data);
        }


    }
}
