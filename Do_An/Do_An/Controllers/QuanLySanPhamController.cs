using Do_An.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Web.UI;
using System.Data.Entity;
using System.Web.Configuration;

namespace Do_An.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();

        // GET: QuanLySanPham
        public ActionResult Index(int? page)
        {
            KhachHang kh = Session["TaiKhoan"] as KhachHang;
            if (kh != null && kh.quyen == 1)
            {
                // Tạo biến số sản phẩm trên trang
                int pageSize = 12;
                // Tạo biến số trang
                int pageNumber = (page ?? 1);
                return View(db.Saches.Where(n => n.Moi == 1).OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            var chuDeList = db.ChuDes.ToList();
            ViewBag.MaChuDe = new SelectList(chuDeList, "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Sach sach)
        {
            if (ModelState.IsValid)
            {
                db.Saches.Add(sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB", sach.MaNXB);
            return View(sach);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var chuDeList = db.ChuDes.ToList();
            ViewBag.MaChuDe = new SelectList(chuDeList, "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB");

            // Lấy sách từ cơ sở dữ liệu dựa trên id và truyền vào view
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }

            return View(sach);
        }
        [HttpGet]
        public ActionResult Xoa(int MaSach)
        {
            Sach sach = db.Saches.Find(MaSach);
            if (sach == null)
            {
                return HttpNotFound();
            }

            return View(sach);
        }

        [HttpPost]
        public ActionResult XacNhanXoa(int MaSach)
        {
            Sach sach = db.Saches.Find(MaSach);
            if (sach == null)
            {
                return HttpNotFound();
            }

            db.Saches.Remove(sach);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}