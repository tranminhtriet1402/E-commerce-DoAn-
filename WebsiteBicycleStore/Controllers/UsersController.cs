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
    public class UsersController : Controller
    {
        private DB_BicycleStoreEntities db = new DB_BicycleStoreEntities();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.LoaiNguoiDung);           
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.IDPhanLoai = new SelectList(db.LoaiNguoiDungs, "IDPhanLoai", "TenPhanLoai");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDUser,FirstName,LastName,Email,Password,DiemTichLuy,IDPhanLoai")] User user)
        {
            if (ModelState.IsValid)
            {
                user.DiemTichLuy = 0;
                if (user.DiemTichLuy==0)
                {
                    user.IDPhanLoai = 1;
                }              
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDPhanLoai = new SelectList(db.LoaiNguoiDungs, "IDPhanLoai", "TenPhanLoai", user.IDPhanLoai);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPhanLoai = new SelectList(db.LoaiNguoiDungs, "IDPhanLoai", "TenPhanLoai", user.IDPhanLoai);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDUser,FirstName,LastName,Email,Password,DiemTichLuy,IDPhanLoai")] User user)
        {
            if (ModelState.IsValid)
            {
                user.DiemTichLuy = 0;
                if (user.DiemTichLuy == 0)
                {
                    user.IDPhanLoai = 1;
                }
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDPhanLoai = new SelectList(db.LoaiNguoiDungs, "IDPhanLoai", "TenPhanLoai", user.IDPhanLoai);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
