using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DataLibrary.BusinessLogic.InventoryProcessor;

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
                var stock = inventory.Where(x => x.InventoryID == id).First();
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
            return View();
        }

        // POST: Inventory/Create
        [HttpPost]
        public ActionResult AddInventory(InventoryModel model)
        {
            if (ModelState.IsValid)
            {
                CreateInventory(model.InventoryID,
                    model.ProductID,
                    model.BinID,
                    model.QTY);
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Inventory/Edit/5
        public ActionResult EditInventory(int id)
        {
            return View();
        }

        // POST: Inventory/Edit/5
        [HttpPost]
        public ActionResult EditInventory(InventoryModel model)
        {
            if (ModelState.IsValid)
            {
                UpdateInventory(model.InventoryID,
                    model.ProductID,
                    model.BinID,
                    model.QTY);
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Inventory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inventory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
