using DataLibrary.DataAccess;
using DataLibrary.Models;
using System.Collections.Generic;

namespace DataLibrary.BusinessLogic
{
    public static class ProductProcessor
    {
        public static int CreateProduct(string sku, string description)
        {
            ProductModel data = new ProductModel
            {
                SKU = sku,
                ProductDescription = description
            };

            string sql = @"insert into dbo.product (SKU, ProductDescription)
                           values (@SKU, @ProductDescription);";

            return SqlDataAccess.Execute(sql, data);
        }

        public static List<ProductModel> LoadProducts()
        {
            string sql = @"select ProductID, SKU, ProductDescription
                            from dbo.product;";

            return SqlDataAccess.Query<ProductModel>(sql);
        }

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

            return SqlDataAccess.Execute(sql, data);
        }

        public static int RemoveProduct(int productID)
        {
            ProductModel data = new ProductModel
            {
                ProductID = productID
            };

            string sql = @"delete from dbo.product 
                          WHERE ProductID = @ProductID;";

            return SqlDataAccess.Execute(sql, data);
        }


    }
}
