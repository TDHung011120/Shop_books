using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer.Symbols;
using MVC_BookStore.Models;
using PagedList;

namespace MVC_BookStore.Areas.Admin.Controllers
{
    public class ADMBooksController : Controller
    {
        DBBookStore db = new DBBookStore();
        //action liệt kê các sách cần quản lý
        public ActionResult Index(int page = 1, string keyword = "")
        {
            var kq = db.Saches.ToList();
            if (keyword != "")
            {
                kq = kq.Where(x =>
               x.TenSach.ToLower().Contains(keyword.ToLower())).ToList();
            }
            int pageSize = 10;
            return View(kq.ToPagedList(page, pageSize));
        }
        
        public ActionResult Delete(int id)
        {
            //tìm thí sinh cần xoá theo sbd
            var s = db.Saches.Find(id);
            //xoá thí sinh
            db.Saches.Remove(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            //tìm thông tin sách cần sửa
            
            var s = db.Saches.SingleOrDefault(x => x.MaSach == id);
            //truyen danh sach lua chon macd, manxb cho View de sinh ra dieu khien DropDownList
            ViewBag.MaCD = new SelectList(db.ChuDes.ToList(), "MaCD", "TenChuDe", s.MaCD);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList(), "MaNXB", "TenNXB", s.MaNXB);
            if (s == null)
                return HttpNotFound();
            return View(s);
        }
        [HttpPost]
        public ActionResult Edit(Sach model)
        {
            var f = Request.Files["NewImage"];
            if (f != null && f.ContentLength > 0)
            {
                var path = Server.MapPath("~/Bia_sach/") + f.FileName;
                f.SaveAs(path);
                model.AnhBia = f.FileName;
            }
            //cập nhật đối tượng thí sinh vào CSDL
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            ViewBag.MaCD = new SelectList(db.ChuDes.ToList(), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList(), "MaNXB", "TenNXB");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Sach model)
        {

            //Xử lý uploads
            var f = Request.Files["NewImage"];
            if (f != null && f.ContentLength > 0)
            {
                var path = Server.MapPath("~/Bia_sach/") + f.FileName;
                f.SaveAs(path);
                model.AnhBia = f.FileName;
            }
            else//nếu không chọn ảnh bìa
            {
                model.AnhBia = "no_image.png";
            }
            model.Ngaycapnhat = DateTime.Now;
            db.Saches.Add(model);   
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}