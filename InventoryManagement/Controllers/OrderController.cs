using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DataLibrary.BusinessLogic.OrderProcessor;
using static DataLibrary.BusinessLogic.ProductProcessor;
using static DataLibrary.BusinessLogic.InventoryProcessor;

namespace InventoryManagement.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            ViewBag.Message = "Order List";

            var data = LoadOrders();
            List<OrderModel> orders = new List<OrderModel>();

            foreach (var row in data)
            {
                orders.Add(new OrderModel
                {
                    OrderID = row.OrderID,
                    OrderNumber = row.OrderNumber,
                    DateOrdered = row.DateOrdered,
                    CustomerName = row.CustomerName,
                    CustomerAddress = row.CustomerAddress
                });
            }

            return View(orders);
        }

        // GET: Order/Details/5
        public ActionResult OrderDetails(int id)
        {
            var orders = LoadOrders();

            try
            {
                var order = orders.Where(x => x.OrderID == id).First();
                return View(order);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult AddOrder()
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

            var inventoryData = LoadInventory();
            List<InventoryModel> inventory = new List<InventoryModel>();

            foreach (var row in inventoryData)
            {
                inventory.Add(new InventoryModel
                {
                    InventoryID = row.InventoryID,
                    ProductID = row.ProductID,
                    BinID = row.BinID,
                    QTY = row.QTY
                });
            }

            ViewBag.Products = products;
            ViewBag.Inventory = inventory;

            return View();
        }

        [HttpPost]
        public ActionResult AddOrder(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var orderID = CreateOrder(model.OrderNumber,
                            model.DateOrdered,
                            model.CustomerName,
                            model.CustomerAddress);
                    return RedirectToAction("../OrderLines/Index/" + orderID);
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = e.Message;
                    return View(model);
                } 
            }

            return View();
        }

        public ActionResult EditOrder(int id)
        {
            var orders = LoadOrders();

            try
            {
                var order = orders.Where(x => x.OrderID == id).First();
                return View(order);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EditOrder(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UpdateOrder(model.OrderID,
                            model.OrderNumber,
                            model.DateOrdered,
                            model.CustomerName,
                            model.CustomerAddress);
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

        public ActionResult DeleteOrder(int id)
        {
            var orders = LoadOrders();

            try
            {
                var order = orders.Where(x => x.OrderID == id).First();
                return View(order);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult DeleteOrder(OrderModel model)
        {
            RemoveOrder(model.OrderID);
            return RedirectToAction("Index");
        }
    }
}
