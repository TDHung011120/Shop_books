
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using MVC_BookStore.Models;

namespace MVC_BookStore.Controllers
{
    public class AddTocartController : Controller
    {
        // GET: AddTocart
        DBBookStore db = new DBBookStore();
        public ActionResult AddToCart(int id)
        {
            //lấy giỏ hàng trong Session
            Cart cart = (Cart)Session["GIOHANG"];
            if (cart == null) //nếu giỏ hàng rỗng
            {
                cart = new Cart(); //tạo giỏ hàng
                Session["GIOHANG"] = cart;//lưu vào Session
            }
            //tìm đối tượng sách theo id
            var sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            //thêm sản phẩm sách vào giỏ hàng
            cart.AddItem(sach, 1);
            //chuyển hướng đến action Xem giỏ hàng
            return RedirectToAction("ViewCart");
        }
        //action trình bày giỏ hàng
        public ActionResult ViewCart()
        {
            Cart cart = (Cart)Session["GIOHANG"];
            if (cart == null)
            {
                cart = new Cart();
                Session["GIOHANG"] = cart;
            }
            return View(cart);
        }
        public ActionResult Update(int id, int soluong)
        {
            //tìm đối tượng trong CSDL
            var sach = db.Saches.Find(id);
            //lấy giỏ hàng trong session
            Cart cart = (Cart)Session["GIOHANG"];
            if (cart != null)
            {
                cart.UpdateItem(sach, soluong);
            }
            //chuyển hướng về lại actuon ViewCart
            return RedirectToAction("ViewCart");
        }
        public ActionResult Delete(int id)
        {
            //tìm đối tượng trong CSDL
            var sach = db.Saches.Find(id);
            //lấy giỏ hàng trong session
            Cart cart = (Cart)Session["GIOHANG"];
            if (cart != null)
            {
                cart.DeleteItem(sach);
            }
            //chuyển hướng về lại actuon ViewCart
            return RedirectToAction("ViewCart");
        }
        //xóa toang bộ giỏ hàng
        public ActionResult DeleteCart()
        {
            //lấy giỏ hàng trong session
            Cart cart = (Cart)Session["GIOHANG"];
            if (cart != null)
            {
                cart.Items.Clear();
            }
            //chuyển hướng về lại actuon ViewCart
            return RedirectToAction("ViewCart");
        }
        public ActionResult CoutItem()
        {
            int dem = 0;
            //lấy giỏ hàng trong sesstion của khách
            Cart cart = (Cart)Session["GIOHANG"];
            if (cart != null)
            {
                dem = cart.Items.Count;
            }
            return Content(dem.ToString());
        }
        public ActionResult Checkout()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                ViewBag.GIOHANG = Session["GIOHANG"];
                ViewBag.KHACHHANG = Session["User"];
            }
            return View(); 
        }
        public ActionResult ProcessOder(DonDatHang model)
        {
            //lay thong tin gio hang
            var cart = (Cart)Session["GIOHANG"];
            var kh = (KhachHang)Session["User"];
            //khoi tao don hang
            model.MaKH = kh.MaKH;
            model.NgayDH = DateTime.Today;
            model.Trigia = (decimal)cart.TongThanhTien;
            model.Dagiao = false;
            //add don hang vao csdl
            db.DonDatHangs.Add(model);
            db.SaveChanges();
            //add du lieu chi tiet don dat hang vao csdl
            foreach(var item in cart.Items)
            {
                db.CTDatHangs.Add(new CTDatHang
                {
                    SoDH = model.SoDH,
                    MaSach = item.Sach.MaSach,
                    Soluong = item.SoLuong,
                    Dongia=item.Sach.Dongia,
                    Thanhtien=(decimal)item.ThanhTien
                }) ;
                db.SaveChanges();
            }
            Session.Remove("GIOHANG");
            return RedirectToAction("Notify");
        }
        public ActionResult Notify()
        {
            return View();
        }
    }
}