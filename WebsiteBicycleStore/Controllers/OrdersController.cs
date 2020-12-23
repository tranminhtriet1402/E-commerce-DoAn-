using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBicycleStore.Models;

namespace WebsiteBicycleStore.Controllers
{
    
    public class OrdersController : Controller
    {
        public int checkdon;
        private DB_BicycleStoreEntities db = new DB_BicycleStoreEntities();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.User);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.IDUser = new SelectList(db.Users, "IDUser", "FirstName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDOrder,IDUser,Email,OrderDate,Address_Cus,Amount,Descriptions,TinhTrangGiao,TinhTrangDonHang,TinhTrangThanhToan,HuyDon,TinhTrangDongGoi")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDUser = new SelectList(db.Users, "IDUser", "FirstName", order.IDUser);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDUser = new SelectList(db.Users, "IDUser", "FirstName", order.IDUser);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDOrder,IDUser,Email,OrderDate,Address_Cus,Amount,Descriptions,TinhTrangGiao,TinhTrangDonHang,TinhTrangThanhToan,HuyDon,TinhTrangDongGoi")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDUser = new SelectList(db.Users, "IDUser", "FirstName", order.IDUser);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {          
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            foreach (var ec in db.OrderDetails.Where(x => x.IDOrder == id))
            {
                db.OrderDetails.Remove(ec);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteConfirmed1(int id)
        {
            checkdon = id;
            return RedirectToAction("Index","OrderDetails");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult TinhTrangGiao(int? id)
        {
            var check = db.Orders.Find(id);

            if (check.TinhTrangGiao == true)
            {
                check.TinhTrangGiao = false;
            }
            else if (check.TinhTrangGiao == false)
            {
                check.TinhTrangGiao = true;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Orders");
        }
        public ActionResult TinhTrangDonHang(int? id)
        {
            var check = db.Orders.Find(id);
            if (check.TinhTrangDonHang == true)
            {
                check.TinhTrangDonHang = false;
            }
            else if (check.TinhTrangDonHang == false)
            {
                check.TinhTrangDonHang = true;
            }


            db.SaveChanges();
            return RedirectToAction("Index", "Orders");
        }
        public ActionResult TinhTrangThanhToan(int? id)
        {
            var check = db.Orders.Find(id);
            if (check.TinhTrangThanhToan == true)
            {
                check.TinhTrangThanhToan = false;
            }
            else if (check.TinhTrangThanhToan == false)
            {
                check.TinhTrangThanhToan = true;
            }


            db.SaveChanges();
            return RedirectToAction("Index", "Orders");
        }
        public ActionResult TinhTrangDongGoi(int? id)
        {
            var check = db.Orders.Find(id);
            if (check.TinhTrangDongGoi == true)
            {
                check.TinhTrangDongGoi = false;
            }
            else if (check.TinhTrangDongGoi == false)
            {
                check.TinhTrangGiao = false;
                check.TinhTrangDongGoi = true;
            }


            db.SaveChanges();
            return RedirectToAction("Index", "Orders");
        }
        public ActionResult HuyDon(int? id)
        {
            var check = db.Orders.Find(id);
            if (check.HuyDon == true)
            {
                check.HuyDon = false;
            }
            else if (check.HuyDon == false)
            {
                check.HuyDon = true;
                check.TinhTrangDongGoi = false;
                check.TinhTrangDonHang = false;
            }


            db.SaveChanges();
            return RedirectToAction("Index", "Orders");
        }

        
    }
}
