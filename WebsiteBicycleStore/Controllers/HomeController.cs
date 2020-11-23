﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBicycleStore.Models;

namespace WebsiteBicycleStore.Controllers
{
    public class HomeController : Controller
    {
        private DB_BicycleStoreEntities db = new DB_BicycleStoreEntities();
        public ActionResult Index()
        {
            return View(db.Products.Take(4).ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}