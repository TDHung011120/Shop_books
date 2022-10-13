using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using MVC_BookStore.Models;
using PagedList;
namespace MVC_BookStore.Areas.Admin.Controllers
{
    public class OdersController : Controller
    {
        // GET: Admin/Oders
        DBBookStore db= new DBBookStore();
        public ActionResult Index(int page=1)
        {
            var oders = db.DonDatHangs.OrderByDescending(x => x.NgayDH).ToList();
            return View(oders.ToPagedList(page, 10));
        }
        public ActionResult Detail(int id)
        {
            var TinhTrang = new ArrayList();
            TinhTrang.Add(new { check = true ,TT="Đã giao"}) ;
            TinhTrang.Add(new { check = false, TT = "Chưa giao" });
            ViewBag.TinhTrang = new SelectList(TinhTrang, "check", "TT");
            var oder = db.DonDatHangs.Find(id);
            return View(oder);
        }
        [HttpPost]
        public ActionResult Update(DonDatHang model)
        {
            var oder = db.DonDatHangs.FirstOrDefault(a=>a.SoDH==model.SoDH);
            if (model.Dagiao == true)
            {
                oder.Dagiao = true;
            }
            else
            {
                oder.Dagiao = false;
            }
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var oder = db.DonDatHangs.Find(id);
            var CT = db.CTDatHangs.Where(x => x.SoDH == id).ToList();
            foreach (var item in CT)
            {
                db.CTDatHangs.Remove(item);
            }
            db.DonDatHangs.Remove(oder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}