using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DataLibrary.BusinessLogic.OrderLinesProcessor;
using static DataLibrary.BusinessLogic.OrderProcessor;
using static DataLibrary.BusinessLogic.ProductProcessor;
using static DataLibrary.BusinessLogic.InventoryProcessor;

namespace InventoryManagement.Controllers
{
    public class OrderLinesController : Controller
    {
        public ActionResult Index(int orderID)
        {
            ViewBag.Message = "Order Lines";

            var data = LoadOrderLines(orderID);
            List<OrderLinesModel> orderLines = new List<OrderLinesModel>();
            var orders = LoadOrders();

            foreach (var row in data)
            {
                orderLines.Add(ConvertFromDataModel(row));
            }

            ViewBag.Order = orders.Where(x => x.OrderID == orderID).FirstOrDefault();
            return View(orderLines);
        }

        public ActionResult AddOrderLines(int orderID)
        {
            

            ViewBag.Products = LoadProductModels();

            return View(new OrderLinesModel {
                OrderID = orderID,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrderLines(OrderLinesModel model)
        {
            ViewBag.Products = LoadProductModels();

            if (ModelState.IsValid)
            {
                try
                {
                    var productRemaining = CheckInventory(model.ProductID);
                    if(productRemaining < model.QTY)
                    {
                        ViewBag.ErrorMessage = "Not enough inventory, only " + productRemaining + " remaining";
                        return View(model);
                    }
                    CreateOrderLine(model.OrderID,
                        model.ProductID,
                        model.QTY);
                    return RedirectToAction("Index/" + model.OrderID);
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = e.Message;
                    return View(model);
                }
            }
            return View(model);
        }

        public ActionResult EditOrderLines(int orderID, int id)
        {
            var orderLines = LoadOrderLines(orderID);
            ViewBag.OrderID = orderID;

            var orderLine = orderLines.Where(x => x.OrderLineID == id).FirstOrDefault();
            return View(ConvertFromDataModel(orderLine));
        }

        [HttpPost]
        public ActionResult EditOrderLines(OrderLinesModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UpdateOrderLines(model.OrderLineID,
                        model.OrderID,
                        model.ProductID,
                        model.QTY);
                    return RedirectToAction("Index/" + model.OrderID);
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = e.Message;
                    return View(model);
                }
            }

            return View();
        }

        public ActionResult DeleteOrderLines(int orderID, int id)
        {
            var orderLines = LoadOrderLines(orderID);
            ViewBag.OrderID = orderID;

            var orderLine = orderLines.Where(x => x.OrderLineID == id).FirstOrDefault();
            return View(ConvertFromDataModel(orderLine));
        }

        [HttpPost]
        public ActionResult DeleteOrderLines(OrderLinesModel model)
        {
            var previousState = LoadOrderLine(model.OrderLineID);

            try
            {
                RemoveOrderLine(model.OrderLineID); 
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View(model);
            }

            return RedirectToAction("Index/" + model.OrderID);

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

        private static OrderLinesModel ConvertFromDataModel(DataLibrary.Models.OrderLinesModel row)
        {
            return new OrderLinesModel
            {
                OrderLineID = row.OrderLineID,
                OrderID = row.OrderID,
                ProductID = row.ProductID,
                QTY = row.QTY
            };
        }
    }
}
