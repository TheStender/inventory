using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DataLibrary.BusinessLogic.BinProcessor;

namespace InventoryManagement.Controllers
{
    public class BinsController : Controller
    {
        // GET: Bin
        public ActionResult Index()
        {
            ViewBag.Message = "Bin List";

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

            return View(bins);
        }

        // GET: Bin/Details/5
        public ActionResult BinDetails(int id)
        {
            var bins = LoadBins();

            try
            {
                var p = bins.Where(x => x.BinID == id).First();
                return View(p);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Bin/Create
        public ActionResult AddBin()
        {
            return View();
        }

        // POST: Bin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBin(BinModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CreateBin(model.BinID,
                        model.BinName);
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

        // GET: Bin/Edit/5
        public ActionResult EditBin(int id)
        {
            var bins = LoadBins();

            try
            {
                var b = bins.Where(x => x.BinID == id).First();
                return View(b);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Bin/Edit/5
        [HttpPost]
        public ActionResult EditBin(BinModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UpdateBin(model.BinID,
                        model.BinName);
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

        // GET: Bin/Delete/5
        public ActionResult DeleteBin(int id)
        {
            var bins = LoadBins();

            try
            {
                var b = bins.Where(x => x.BinID == id).First();
                return View(b);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Bin/Delete/5
        [HttpPost]
        public ActionResult DeleteBin(BinModel model)
        {
            RemoveBin(model.BinID);
            return RedirectToAction("Index");
        }
    }
}
