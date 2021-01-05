using Microsoft.Ajax.Utilities;
using PayPal.Api;
//using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebsiteBicycleStore.Models;
using Order = WebsiteBicycleStore.Models.Order;

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
            if (Session["Email"] != null)
            {
                string email = Session["Email"].ToString();
                var diemtichluy = db.Users.Where(s => s.Email.StartsWith(email)).ToList();
                foreach (var tichluy in diemtichluy)
                {
                    if (tichluy.IDPhanLoai == 2)
                    {
                        ViewBag.phanloai = "Khách Hàng Vip";
                    }
                    else if (tichluy.IDPhanLoai == 1)
                    {
                        ViewBag.phanloai = "Khách Hàng Thường";
                    }
                    else if (tichluy.IDPhanLoai == 3)
                    {
                        ViewBag.phanloai = "Khách Hàng SVIP";
                    }

                }
            }

           
            if (Session["Cart"] == null)
            {
                return RedirectToAction("ShowToCart", "ShoppingCart");
            }
            else
            {
                ViewBag.mess = "Bạn chưa thêm sản phẩm nào";
            }
            Cart cart = Session["Cart"] as Cart;
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
        public ActionResult CheckOut(FormCollection form )
        {
            string email = Session["Email"].ToString();
            var diemtichluy = db.Users.Where(s => s.Email.StartsWith(email)).ToList();
            foreach (var tichluy in diemtichluy)
            {
                tichluy.DiemTichLuy += 1;
                if (tichluy.DiemTichLuy>=5 && tichluy.DiemTichLuy<20)
                {
                    tichluy.IDPhanLoai = 2;
                    Session["CheckID"] = 2;
                }
                else if (tichluy.DiemTichLuy >= 20)
                {
                    tichluy.IDPhanLoai = 3;
                    Session["CheckID"] = 3;
                }
                else if (tichluy.DiemTichLuy < 5)
                {
                    tichluy.IDPhanLoai = 1;
                    Session["CheckID"] = 1;
                }
            }
            try
            {
                Cart cart = Session["Cart"] as Cart;
                Order _order = new Order();
                string check = form["ship"].ToString();

                if ((int)Session["CheckID"] == 1)
                {
                    if (check == "2")
                    {

                        _order.Descriptions = "Thanh Toán Khi Nhận Hàng , đã bao gòm phí ship $" + form["ship"].ToString();
                        _order.Amount = int.Parse(form["amount1"]);
                    }
                    else if (check == "3")
                    {
                        _order.Descriptions = " Thanh Toán Khi Nhận Hàng , đã bao gòm phí ship $" + form["ship"].ToString();
                        _order.Amount = int.Parse(form["amount2"]);
                    }
                }
                else if ((int)Session["CheckID"] == 2)
                {
                    if (check == "2")
                    {

                        _order.Descriptions = "Miễn phí ship (Khách Hàng VIP)" /*+ form["ship"].ToString()*/;
                        _order.Amount = int.Parse(form["amount"]);
                    }
                    else if (check == "3")
                    {
                        _order.Descriptions = "Miễn phí ship (Khách Hàng VIP)" /*+ form["ship"].ToString()*/;
                        _order.Amount = int.Parse(form["amount"]);
                    }

                }
                else if ((int)Session["CheckID"] == 3)
                {
                    if (check == "2")
                    {

                        _order.Descriptions = "Giảm 10% + Miễn phí ship (Khách Hàng SVIP)" /*+ form["ship"].ToString()*/;
                        _order.Amount = double.Parse(form["amount3"]);
                    }
                    else if (check == "3")
                    {
                        _order.Descriptions = "Giảm 10% + Miễn phí ship (Khách Hàng SVIP)" /*+ form["ship"].ToString()*/;
                        _order.Amount = double.Parse(form["amount3"]);
                    }
                }
                    //foreach (var tichluy in diemtichluy)
                    //{
                    //    if (tichluy.IDPhanLoai == 1)
                    //    {
                    //        if (check == "2")
                    //        {

                    //            _order.Descriptions = "Thanh Toán Khi Nhận Hàng, đã bao gòm phí ship $" + form["ship"].ToString();
                    //            _order.Amount = int.Parse(form["amount1"]);
                    //        }
                    //        else if (check == "3")
                    //        {
                    //            _order.Descriptions = "Thanh Toán Khi Nhận Hàng , đã bao gòm phí ship $" + form["ship"].ToString();
                    //            _order.Amount = int.Parse(form["amount2"]);
                    //        }
                    //    }
                    //    else if (tichluy.IDPhanLoai == 2)
                    //    {
                    //        if (check == "2")
                    //        {

                    //            _order.Descriptions = "Thanh Toán Khi Nhận Hàng, Miễn phí ship (Khách Hàng VIP)" + form["ship"].ToString();
                    //            _order.Amount = int.Parse(form["amount"]);
                    //        }
                    //        else if (check == "3")
                    //        {
                    //            _order.Descriptions = "Thanh Toán Khi Nhận Hàng , Miễn phí ship (Khách Hàng VIP)" + form["ship"].ToString();
                    //            _order.Amount = int.Parse(form["amount"]);
                    //        }

                    //    }
                    //    else if (tichluy.IDPhanLoai == 3)
                    //    {
                    //        if (check == "2")
                    //        {

                    //            _order.Descriptions = "Thanh Toán Khi Nhận Hàng, Giảm 10% + Miễn phí ship (Khách Hàng SVIP)" + form["ship"].ToString();
                    //            _order.Amount = int.Parse(form["amount3"]);
                    //        }
                    //        else if (check == "3")
                    //        {
                    //            _order.Descriptions = "Thanh Toán Khi Nhận Hàng , Giảm 10% + Miễn phí ship (Khách Hàng SVIP)" + form["ship"].ToString();
                    //            _order.Amount = int.Parse(form["amount3"]);
                    //        }

                    //    }

                    //}


                    _order.OrderDate = DateTime.Now;
                _order.TinhTrangDonHang = false;
                _order.TinhTrangGiao = false;
                _order.TinhTrangThanhToan = false;
                _order.HuyDon = false;
                _order.TinhTrangDongGoi = false;
                _order.Email = Session["Email"].ToString();
                _order.Address_Cus = form["diachi"];
                //_order.Descriptions = "Thanh Toán Khi Nhận Hàng";
                //_order.Amount = int.Parse(form["amount"]);
                db.Orders.Add(_order);
                //**Check order xong, sửa lại giao diện**//

                foreach (var item in cart.Items)
                {
                    OrderDetail _order_Detail = new OrderDetail();
                    _order_Detail.IDOrder = _order.IDOrder;
                    int var = (int)_order_Detail.IDOrder;
                    Session["mhd"] = var;
                    _order_Detail.ngayDat = _order.OrderDate;
                    _order_Detail.namePro = item._shopping_product.NameProduct;
                    _order_Detail.ngayNhan = DateTime.Now.AddDays(5);                  
                    _order_Detail.IDProduct = item._shopping_product.IDProduct;
                    _order_Detail.UnitPriceSale = item._shopping_product.UnitPrice;
                    _order_Detail.imgPro = item._shopping_product.Images;
                    _order_Detail.QuantitySale = item._shopping_quantity;
                    var check1 = db.Products.Find(item._shopping_product.IDProduct);
                    check1.soLuong -= item._shopping_quantity;

                    db.Entry(check1).State = EntityState.Modified;
                    db.OrderDetails.Add(_order_Detail);
                }
                db.SaveChanges();
                cart.ClearCart();
                return View("SuccessView");
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "SanPham");
            }

        }

        public ActionResult CheckOut1(FormCollection form)
        {

            try
            {
                Cart cart = Session["Cart"] as Cart;
                Order _order = new Order();


                _order.OrderDate = DateTime.Now;

                _order.Email = Session["Email"].ToString();
                _order.Address_Cus = form["diachi"];
                _order.Descriptions = "Đã Chuyển Khoản";
                _order.Amount = int.Parse(form["amount"]);
                _order.TinhTrangDonHang = false;
                _order.TinhTrangGiao = false;
                _order.TinhTrangThanhToan = false;
                _order.HuyDon = false;
                _order.TinhTrangDongGoi = false;
                db.Orders.Add(_order);
                //**Check order xong, sửa lại giao diện**//
                
                foreach (var item in cart.Items)
                {
                    OrderDetail _order_Detail = new OrderDetail();
                    _order_Detail.IDOrder = _order.IDOrder;
                    _order_Detail.ngayDat = _order.OrderDate;
                    _order_Detail.namePro = item._shopping_product.NameProduct;
                    _order_Detail.ngayNhan = DateTime.Now.AddDays(5);
                    _order_Detail.IDProduct = item._shopping_product.IDProduct;
                    _order_Detail.UnitPriceSale = item._shopping_product.UnitPrice;
                    _order_Detail.imgPro = item._shopping_product.Images;
                    _order_Detail.QuantitySale = item._shopping_quantity;
                    
                    db.OrderDetails.Add(_order_Detail);
                }
                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "SanPham");
            }

        }
      

        private Payment payment;

        // Create a paypment using an APIContext
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var listItems = new ItemList() { items = new List<Item>() };
            string email = Session["Email"].ToString();

           
            var diemtichluy = db.Users.Where(s => s.Email.StartsWith(email)).ToList();
            foreach (var tichluy in diemtichluy)
            {
                tichluy.DiemTichLuy += 1;
                if (tichluy.DiemTichLuy > 5)
                {
                    tichluy.IDPhanLoai = 2;
                }
                else if (tichluy.DiemTichLuy > 20)
                {
                    tichluy.IDPhanLoai = 3;
                }
            }
            Cart cart = Session["Cart"] as Cart;
            Order _order = new Order();


            _order.OrderDate = DateTime.Now;

            _order.Email = Session["Email"].ToString();
            _order.Address_Cus = "234 Su Van Hanh";
            _order.Descriptions = "Đã Thanh Toán Bằng PayPal";
            _order.TinhTrangDonHang = false;
            _order.TinhTrangGiao = false;
            _order.TinhTrangThanhToan = false;
            _order.HuyDon = false;
            _order.TinhTrangDongGoi = false;
            _order.Amount = /*nt.Parse(form["amount"]);*/ (int)cart.Items.Sum(x => x._shopping_quantity * x._shopping_product.UnitPrice);
            db.Orders.Add(_order);
            //**Check order xong, sửa lại giao diện**//

            foreach (var item in cart.Items)
            {
                OrderDetail _order_Detail = new OrderDetail();
                _order_Detail.IDOrder = _order.IDOrder;
                _order_Detail.ngayDat = _order.OrderDate;
                _order_Detail.namePro = item._shopping_product.NameProduct;
                _order_Detail.ngayNhan = DateTime.Now.AddDays(5);
                _order_Detail.IDProduct = item._shopping_product.IDProduct;
                _order_Detail.UnitPriceSale = item._shopping_product.UnitPrice;
                _order_Detail.imgPro = item._shopping_product.Images;
                _order_Detail.QuantitySale = item._shopping_quantity;
                var check1 = db.Products.Find(item._shopping_product.IDProduct);
                check1.soLuong -= cart.Total_Quantity();

                db.Entry(check1).State = EntityState.Modified;
                db.OrderDetails.Add(_order_Detail);
            }
            db.SaveChanges();

            foreach (var item in cart.Items)
            {
                listItems.items.Add(new Item()
                {
                    name = item._shopping_product.NameProduct,
                    currency = "USD",
                    price = item._shopping_product.UnitPrice.ToString(),
                    quantity = item._shopping_quantity.ToString(),
                    sku = "sku"

                });
            }

            var payer = new Payer() { payment_method = "paypal" };

            // Do the configuration RedirectURLs here with redirectURLs object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            // Create details object
            var details = new Details()
            {
                tax = "1",
                shipping = "2",
                subtotal = cart.Items.Sum(x => x._shopping_quantity * x._shopping_product.UnitPrice).ToString()
            };

            // Create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(),// tax + shipping + subtotal
                details = details
            };

            // Create transaction
            var transactionList = new List<Transaction>();
            transactionList.Add(new Transaction()
            {
                description = "Chien Testing transaction description",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = listItems
            });

            payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            cart.ClearCart();
            return payment.Create(apiContext);
        }

        // Create ExecutePayment method
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            payment = new Payment() { id = paymentId };
            return payment.Execute(apiContext, paymentExecution);
        }

        // Create PaymentWithPaypal method
        public ActionResult PaymentWithPaypal()
        {




            // Gettings context from the paypal bases on clientId and clientSecret for payment
            APIContext apiContext = PaypalConfiguration.GetAPIContext();

            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    // Creating a payment
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/ShoppingCart/PaymentWithPaypal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid);

                    // Get links returned from paypal response to create call funciton
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = string.Empty;

                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                        }
                    }

                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This one will be executed when we have received all the payment params from previous call
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        //Remove shopping cart session
                        //Session.Remove(strCart);
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                //PaypalLogger.Log("Error: " + ex.Message);
                //Remove shopping cart session
                //Session.Remove(strCart);
                return View("FailureView");
            }

            //Remove shopping cart session
            //Session.Remove(strCart);

            return View("SuccessView");

        }


        

    }
}