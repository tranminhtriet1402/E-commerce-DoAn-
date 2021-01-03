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
    public class LoaiNguoiDungsController : Controller
    {
        private DB_BicycleStoreEntities db = new DB_BicycleStoreEntities();

        // GET: LoaiNguoiDungs
        public ActionResult Index()
        {
            return View(db.LoaiNguoiDungs.ToList());
        }

        // GET: LoaiNguoiDungs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiNguoiDung loaiNguoiDung = db.LoaiNguoiDungs.Find(id);
            if (loaiNguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(loaiNguoiDung);
        }

        // GET: LoaiNguoiDungs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiNguoiDungs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPhanLoai,TenPhanLoai")] LoaiNguoiDung loaiNguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.LoaiNguoiDungs.Add(loaiNguoiDung);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiNguoiDung);
        }

        // GET: LoaiNguoiDungs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiNguoiDung loaiNguoiDung = db.LoaiNguoiDungs.Find(id);
            if (loaiNguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(loaiNguoiDung);
        }

        // POST: LoaiNguoiDungs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPhanLoai,TenPhanLoai")] LoaiNguoiDung loaiNguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiNguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiNguoiDung);
        }

        // GET: LoaiNguoiDungs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiNguoiDung loaiNguoiDung = db.LoaiNguoiDungs.Find(id);
            if (loaiNguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(loaiNguoiDung);
        }

        // POST: LoaiNguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiNguoiDung loaiNguoiDung = db.LoaiNguoiDungs.Find(id);
            db.LoaiNguoiDungs.Remove(loaiNguoiDung);
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
