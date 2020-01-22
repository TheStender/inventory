using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DataLibrary.BusinessLogic.OrderProcessor;

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

        // GET: Order/Create
        public ActionResult AddOrder()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult AddOrder(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                CreateOrder(model.OrderID,
                            model.OrderNumber, 
                            model.DateOrdered, 
                            model.CustomerName, 
                            model.CustomerAddress);
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Order/Edit/5
        public ActionResult EditOrder(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult EditOrder(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                UpdateOrder(model.OrderID,
                            model.OrderNumber,
                            model.DateOrdered,
                            model.CustomerName,
                            model.CustomerAddress);
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
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
