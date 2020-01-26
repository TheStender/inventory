using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DataLibrary.BusinessLogic.InventoryProcessor;
using static DataLibrary.BusinessLogic.ProductProcessor;
using static DataLibrary.BusinessLogic.BinProcessor;

namespace InventoryManagement.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Index()
        {
            ViewBag.Message = "Inventory";

            var data = LoadInventory();
            List<InventoryModel> inventory = new List<InventoryModel>();

            foreach (var row in data)
            {
                inventory.Add(new InventoryModel
                {
                    InventoryID = row.InventoryID,
                    ProductID = row.ProductID,
                    BinID = row.BinID,
                    QTY = row.QTY
                });
            }

            return View(inventory);
        }

        // GET: Inventory/Details/5
        public ActionResult InventoryDetails(int id)
        {
            var inventory = LoadInventory();

            try
            {
                var stock = inventory.Where(x => x.InventoryID == id).FirstOrDefault ();
                return View(stock);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Inventory/Create
        public ActionResult AddInventory()
        {
            ViewBag.Products = LoadProductModels();
            ViewBag.Bins = LoadBinModels();

            return View();
        }

        // POST: Inventory/Create
        [HttpPost]
        public ActionResult AddInventory(InventoryModel model)
        {
            ViewBag.Products = LoadProductModels();
            ViewBag.Bins = LoadBinModels();

            if (ModelState.IsValid)
            {
                try
                {
                    CreateInventory(model.InventoryID,
                    model.ProductID,
                    model.BinID,
                    model.QTY);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = e.Message;
                    return View(model);
                } 
            }
            return View(model);
        }

        // GET: Inventory/Edit/5
        public ActionResult EditInventory(int id)
        {
            var inventory = LoadInventory();
            ViewBag.Products = LoadProductModels();
            ViewBag.Bins = LoadBinModels();

            try
            {
                var inv = inventory.Where(x => x.InventoryID == id).FirstOrDefault();
                return View(inv);
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }

        // POST: Inventory/Edit/5
        [HttpPost]
        public ActionResult EditInventory(InventoryModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UpdateInventory(model.InventoryID,
                    model.ProductID,
                    model.BinID,
                    model.QTY);
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

        // GET: Inventory/Delete/5
        public ActionResult DeleteInventory(int id)
        {
            var inventory = LoadInventory();

            try
            {
                var inv = inventory.Where(x => x.InventoryID == id).FirstOrDefault();
                return View(inv);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Inventory/Delete/5
        [HttpPost]
        public ActionResult DeleteInventory(InventoryModel model)
        {
            RemoveInventory(model.InventoryID);
            return RedirectToAction("Index");
        }

        private static List<ProductModel> LoadProductModels()
        {
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

            return products;
        }

        private static List<BinModel> LoadBinModels()
        {
            var data = LoadBins();
            List<BinModel> bins = new List<BinModel>();

            foreach (var row in data)
            {
                bins.Add(new BinModel
                {
                    BinID = row.BinID,
                    BinName = row.BinName
                });
            }

            return bins;
        }
    }
}
