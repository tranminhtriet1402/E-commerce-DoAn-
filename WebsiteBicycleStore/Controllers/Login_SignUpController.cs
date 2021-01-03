using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBicycleStore.Models;

namespace WebsiteBicycleStore.Controllers
{
    public class Login_SignUpController : Controller
    {
        DB_BicycleStoreEntities _db = new DB_BicycleStoreEntities();
        // GET: Login_SignUp
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }



        //Xử lý Đăng Nhập
        [HttpPost]
        public ActionResult Login(User _user ,FormCollection form)
        {
            var check = _db.Users.Where(s => s.Email.Equals(_user.Email) && s.Password.Equals(_user.Password)).FirstOrDefault();
            if (check == null)
            {
                //_user.LoginErrorMessage = "Error Email or Password! Try again please!";
                ViewBag.loidangnhap = "Lỗi đăng nhập!!!!!!";
                return View("Login", _user);
            }
            else
            {
                //var email = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
                var test = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (test.Email != "Admin@gmail.com")
                {
                    var user = new User();

                    Session["Email"] = _user.Email;
                    Session["PassWord"] = _user.Password;
                    Session["ID"] = _user.IDUser;
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    Session["EmailAdmin"] = _user.Email;
                    Session["Email"] = _user.Email;
                    return RedirectToAction("Index", "Users");
                }

            }


        }
        //Xử lý Đăng Ký
        [HttpPost]
        public ActionResult SignUp(User _user, FormCollection form)
        {
            var check = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
            if (check == null)
            {
                if (_user.Password == form["confirmpassword"])
                {
                    _user.DiemTichLuy = 0;
                    _user.IDPhanLoai = 1;
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Users.Add(_user);
                    _db.SaveChanges();
                    return RedirectToAction("Login", "Login_SignUp");
                }
                else
                {
                    ViewBag.error = "Mật khẩu không trùng khớp!!!!";

                    return View("SignUp");
                }
            }
            else
            {
                ViewBag.error1 = "Email đã được sử dụng!!!!";

                return View("Index");
            }
        }

        //Xử lý log out
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}