using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DataLibrary.BusinessLogic.OrderLinesProcessor;
using static DataLibrary.BusinessLogic.OrderProcessor;
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
                orderLines.Add(new OrderLinesModel
                {
                    OrderLineID = row.OrderLineID,
                    OrderID = row.OrderID,
                    ProductID = row.ProductID,
                    QTY = row.QTY
                });
            }

            ViewBag.Order = orders.Where(x => x.OrderID == orderID).FirstOrDefault();
            return View(orderLines);
        }

        public ActionResult AddOrderLines(int orderID)
        {

            return View(new OrderLinesModel {
                OrderID = orderID,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrderLines(OrderLinesModel model)
        {
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
                    //return View();
                    //return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = e.Message;
                    return View(model);
                }

            }
            return RedirectToAction("Index/" + model.OrderID);

            //return View();
        }

        public ActionResult EditOrderLines(int orderID, int id)
        {
            var orderLines = LoadOrderLines(orderID);
            ViewBag.OrderID = orderID;

            var orderLine = orderLines.Where(x => x.OrderLineID == id).First();
            return View(orderLine);

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
                    //return View("Index", new { orderID = ViewBag.OrderID });
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = e.Message;
                    return View(model);
                }

            }

            return View();
        }

        public ActionResult DeleteOrderLines(int orderID)
        {
            var orderLines = LoadOrderLines(orderID);
            ViewBag.OrderID = orderID;

            try
            {
                var p = orderLines.Where(x => x.OrderID == orderID).First();
                return RedirectToAction("Index/" + orderID);
            }
            catch
            {
                return RedirectToAction("Index/" + orderID);
            }
        }

        [HttpPost]
        public ActionResult DeleteOrderLines(OrderLinesModel model)
        {
            RemoveOrderLine(model.OrderLineID);
            //return RedirectToAction("Index");
            return RedirectToAction("Index/" + model.OrderID);
        }

    }
}
