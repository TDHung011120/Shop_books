using MVC_BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_BookStore.Controllers
{
    public class UsersController : Controller
    {
        DBBookStore db = new DBBookStore();
        // GET: Users
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult ProcessRegister(RegisterUser u)
        {
            var kh = new KhachHang
            {
                HoTenKH = u.Name,
                TenDN = u.User,
                Matkhau = u.Pass,
                Ngaysinh = u.Born,
                Diachi = u.Address,
                Dienthoai = u.Phone,
                Email = u.Email,
            };
            db.KhachHangs.Add(kh);
            db.SaveChanges();
            return RedirectToAction("Login");


        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult ProcessLogin(LoginUser u)
        {
            var kh = db.KhachHangs.SingleOrDefault(k => k.TenDN == u.User && k.Matkhau ==u.Pass);
            if (kh == null)
            {
                ViewBag.TBLogin = "<div class=\"alert alert-danger\"> Tên Đăng nhập hoặc mật khẩu không đúng</div>";
                return View("Login");
            }
            else
            {
                Session["User"] = kh;
                return RedirectToAction("Index", "Books");
            }
        }
        public ActionResult Logout()
        {
            if (Session["User"] != null)
            {
                Session.Remove("User");
            }

            return RedirectToAction("Index", "Books");
        }
    }
}