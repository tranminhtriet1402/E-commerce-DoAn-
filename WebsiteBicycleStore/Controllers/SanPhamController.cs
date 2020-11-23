using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBicycleStore.Models;

namespace WebsiteBicycleStore.Controllers
{
    public class SanPhamController : Controller
    {
        private DB_BicycleStoreEntities db = new DB_BicycleStoreEntities();
        // GET: SanPham
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "TenSanPham")
            {
                return View(db.Products.Where(s => s.NameProduct.Contains(search)).ToList());
            }
            else if (searchBy == "Price")
            {
                return View(db.Products.Where(s => s.UnitPrice.ToString().StartsWith(search)).ToList());
            }
            else
                return View(db.Products.ToList());
        }
        public ActionResult XeDapDua()
        {
            //return View(db.Products.ToList());
            return View(db.Products.Where(s => s.IDCategory == 2).ToList());
        }
        public ActionResult XeDapDiaHinh()
        {

            return View(db.Products.Where(s => s.IDCategory == 3).ToList());
        }
        public ActionResult XeDapDuongPho()
        {
            return View(db.Products.Where(s => s.IDCategory == 4).ToList());
        }
        public ActionResult chi_tiet_san_pham(int id)
        {
            return View(db.Products.Where(s => s.IDProduct.ToString().StartsWith(id.ToString())).ToList());
        }
    }
}