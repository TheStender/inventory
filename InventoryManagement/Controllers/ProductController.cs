using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DataLibrary.BusinessLogic.ProductProcessor;

namespace InventoryManagement.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            ViewBag.Message = "Product List";

            var data = LoadProducts();
            List<ProductModel> products = new List<ProductModel>();

            foreach (var row in data)
            {
                products.Add(new ProductModel
                {
                    ProductID = row.ProductID,
                    SKU = row.SKU,
                    ProductDescription = row.ProductDescription
                });
            }

            return View(products);
        }

        // GET: Product/Details/5
        public ActionResult ProductDetails(int id)
        {
            var products = LoadProducts();

            try
            {
                var p = products.Where(x => x.ProductID == id).First();
                return View(p);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }


        // GET: Product/Create
        public ActionResult AddProduct()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CreateProduct(model.ProductID,
                    model.SKU,
                    model.ProductDescription);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = e.Message;
                    return View(model);
                }

            }

            return View();
        }

        // GET: Product/Edit/5
        public ActionResult EditProduct(int id)
        {
            var products = LoadProducts();

            try
            {
                var p = products.Where(x => x.ProductID == id).First();
                return View(p);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult EditProduct(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UpdateProduct(model.ProductID,
                    model.SKU,
                    model.ProductDescription);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = e.Message;
                    return View(model);
                }
                
            }

            return View();
        }

        // GET: Product/Delete/5
        public ActionResult DeleteProduct(int id)
        {
            var products = LoadProducts();

            try
            {
                var p = products.Where(x => x.ProductID == id).First();
                return View(p);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult DeleteProduct(ProductModel model)
        {
            RemoveProduct(model.ProductID);
            return RedirectToAction("Index");
        }
    }
}
