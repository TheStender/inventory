using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DataLibrary.BusinessLogic.OrderLinesProcessor;

namespace InventoryManagement.Controllers
{
    public class OrderLinesController : Controller
    {
        // GET: OrderLines
        public ActionResult Index()
        {
            ViewBag.Message = "Order Lines";

            var data = LoadOrderLines();
            List<OrderLinesModel> orderLines = new List<OrderLinesModel>();

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

            return View(orderLines);
        }


    }
}
