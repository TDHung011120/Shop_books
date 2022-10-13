using MVC_BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_BookStore.Areas.Admin.Controllers
{
    public class AdminsController : Controller
    {
        DBBookStore db= new DBBookStore();
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult ProcessRegister(RegisterUser u)
        {
            var qtv = new QTV
            {
                HoTen = u.Name,
                TenDN = u.User,
                MK = u.Pass,
                NgaySinh = u.Born,
                
                SDT = u.Phone,
                Email = u.Email,
            };
            db.QTVs.Add(qtv);
            db.SaveChanges();
            return RedirectToAction("Login");


        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult ProcessLogin(LoginUser u)
        {
            var qTV = db.QTVs.SingleOrDefault(k => k.TenDN == u.User && k.MK == u.Pass);
            if (qTV == null)
            {
                ViewBag.TBLogin = "<div class=\"alert alert-danger\"> Tên Đăng nhập hoặc mật khẩu không đúng</div>";
                return View("Login");
            }
            else
            {
                Session["QTV"] = qTV;
                return RedirectToAction("Index", "ADMBooks");
            }
        }
        public ActionResult Logout()
        {
            if (Session["QTV"] != null)
            {
                Session.Remove("QTV");
            }

            return RedirectToAction("Index", "Books", new {Areas=""});
        }
    }
}