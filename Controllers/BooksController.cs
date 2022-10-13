using MVC_BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace MVC_BookStore.Controllers
{
    public class BooksController : Controller
    {
        
        // Hiện 6 quyển sách ở mỗi trang từ mới đến cũ
        DBBookStore db= new DBBookStore();
        public ActionResult Index(int page=1)
        {
            var books = db.Saches.OrderByDescending(book => book.Ngaycapnhat).ToList();
            int pageSize = 6;
            return View(books.ToPagedList(page,pageSize));
        }
        // lấy sachs theo đề
        public ActionResult MenuChuDe()
        {
            var dsChuDe = db.ChuDes.ToList();
            return PartialView(dsChuDe);
        }
        public ActionResult SachTheoChuDe(int id, int page = 1)
        {
            var dsSach = db.Saches.Where(x => x.MaCD == id).ToList();
            ViewBag.TieuDe = db.ChuDes.Find(id).Tenchude.ToUpper();
            var pagesize = 6;
            return View(dsSach.ToPagedList(page, pagesize));
        }
        public ActionResult ChiTiet(int? id)
        {
            if (id == null)//nếu action không nhân id
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var sach = db.Saches.Find(id);
            if (sach == null) //nếu không tìm thấy sách
            {
                return HttpNotFound();
            }
            return View(sach);
        }
    }
}