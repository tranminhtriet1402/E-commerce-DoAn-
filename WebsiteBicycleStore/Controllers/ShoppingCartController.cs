using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebsiteBicycleStore.Models;

namespace WebsiteBicycleStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private DB_BicycleStoreEntities db = new DB_BicycleStoreEntities();
        // GET: ShoppingCart
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddtoCart(int id)
        {
            var pro = db.Products.SingleOrDefault(s => s.IDProduct == id);
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult ShowToCart()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("ShowToCart", "ShoppingCart");
            }
            else
            {
                ViewBag.mess = "Bạn chưa thêm sản phẩm nào";
            }
            Cart cart = Session["Cart"] as Cart;
            db.Users.ToList();
            return View(cart);
        }

        // Lay gia tri tu 1 form Formcolleciton
        public ActionResult Update_Quantity_Cart(FormCollection form)
        {
            //Lay so hien tai gan len quantity new
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["ID_Product"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.Update_Quantity_Shopping(id_pro, quantity);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);

            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            int total_quantity_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                total_quantity_item = cart.Total_Quantity();
                Session["Gio"] = total_quantity_item;
            }
            ViewBag.QuantityCart = total_quantity_item;
            return PartialView("BagCart");
        }
        public ActionResult CheckOut(FormCollection form)
        {
            
            try
            {
                Cart cart = Session["Cart"] as Cart;
                Order _order = new Order();


                _order.OrderDate = DateTime.Now;

                _order.Email = Session["Email"].ToString();
                _order.Address_Cus = form["diachi"];
                _order.Descriptions = form["des"];
                _order.Amount = int.Parse(form["amount"]);
                db.Orders.Add(_order);
                //**Check order xong, sửa lại giao diện**//



                //foreach (var item in cart.Items)
                //{
                //    OrderDetail _order_Detail = new OrderDetail();
                //    _order_Detail.IDOrder = _order.IDOrder;
                //    _order_Detail.ngayDat = _order.OrderDate;
                //    _order_Detail.namePro = item._shopping_product.NameProduct;
                //    _order_Detail.ngayNhan = DateTime.Now.AddDays(5);
                //    _order_Detail.IDProduct = item._shopping_product.IDProduct;
                //    _order_Detail.UnitPriceSale = item._shopping_product.UnitPrice;
                //    _order_Detail.imgPro = item._shopping_product.Images;
                //    _order_Detail.QuantitySale = item._shopping_quantity;
                //    db.OrderDetails.Add(_order_Detail);
                //}
                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "SanPham");
            }

        }
    }
}