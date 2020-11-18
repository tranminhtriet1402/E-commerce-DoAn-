using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBicycleStore.Models;

namespace WebsiteBicycleStore.Controllers
{
    public class ProductsController : Controller
    {
        DB_BicycleStoreEntities _db = new DB_BicycleStoreEntities();
        // GET: Products
        public ActionResult Index()
        {
            
            return View(_db.Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View(_db.Products.Where(s=>s.IDProduct==id).FirstOrDefault());
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            Product pro = new Product();
            return View(pro);
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(Product pro)
        {
            try
            {
                if (pro.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(pro.ImageUpload.FileName);
                    string extension = Path.GetExtension(pro.ImageUpload.FileName);

                    fileName = fileName + extension;

                    pro.Images = "~/Content/images/" + fileName;

                    pro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images"), fileName));
                    pro.ProductDate = DateTime.Now;

                }
                _db.Products.Add(pro);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_db.Products.Where(s => s.IDProduct == id).FirstOrDefault());
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product pro)
        {
            try
            {
                if (pro.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(pro.ImageUpload.FileName);
                    string extension = Path.GetExtension(pro.ImageUpload.FileName);

                    fileName = fileName + extension;

                    pro.Images = "~/Content/images/" + fileName;

                    pro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images"), fileName));
                    pro.ProductDate = DateTime.Now;

                }
                _db.Entry(pro).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_db.Products.Where(s => s.IDProduct == id).FirstOrDefault());
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Product pro)
        {
            try
            {
                // TODO: Add delete logic here
                pro = _db.Products.Where(s => s.IDProduct == id).FirstOrDefault();
                _db.Products.Remove(pro);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ChooseCategory()
        {
            Category cate = new Category();
            cate.CateCollection = _db.Categories.ToList<Category>();
            return PartialView(cate);
        }
    }
}
