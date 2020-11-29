using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

       //Đơn hàng và chi tiết

        public ActionResult DonHang(string email)
        {
            
            email = Session["Email"].ToString();
            //Thống kê đơn hàng
            var count = 0;
            var sum = db.Orders.Where(s=>s.Email.StartsWith(email)).ToList();
            count = sum.Count();
            Session["Count"] = count;


            return View(db.Orders.Where(s => s.Email.StartsWith(email)).ToList());
        }

        public ActionResult chi_tiet_don_hang(int id)
        {
            return View(db.OrderDetails.Where(s => s.IDOrder.ToString().StartsWith(id.ToString())).ToList());
        }

        //Account và chỉnh sửa

        public ActionResult Account(string email)
        {
            email = Session["Email"].ToString();
            return View(db.Users.Where(s => s.Email.StartsWith(email)).ToList());

        }

    }
}