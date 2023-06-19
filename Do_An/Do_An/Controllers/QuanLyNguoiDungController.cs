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

namespace Do_An.Controllers
{
    public class QuanLyNguoiDungController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: QuanLyNguoiDung
        public ActionResult Index(int? page)
        {
            KhachHang kh = Session["TaiKhoan"] as KhachHang;
            if (kh != null && kh.quyen == 1)
            {
                // Tạo biến số sản phẩm trên trang
                int pageSize = 12;
                // Tạo biến số trang
                int pageNumber = (page ?? 1);
                return View(db.KhachHangs.OrderBy(n => n.MaKH).ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuanLyNguoiDung/Create
        [HttpPost]
        public ActionResult Create(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                // Thực hiện lưu khách hàng vào cơ sở dữ liệu
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();

                // Chuyển hướng về trang danh sách người dùng
                return RedirectToAction("Index");
            }

            return View(khachHang);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Lấy sách từ cơ sở dữ liệu dựa trên id và truyền vào view
            KhachHang kh = db.KhachHangs.Find(id);
            if (kh == null)
            {
                return HttpNotFound();
            }

            return View(kh);
        }
        [HttpGet]
        public ActionResult Xoa(int MaKH)
        {
            KhachHang kh = db.KhachHangs.Find(MaKH);
            if (kh == null)
            {
                return HttpNotFound();
            }

            return View(kh);
        }

        [HttpPost]
        public ActionResult XacNhanXoa(int MaKH)
        {
            KhachHang kh = db.KhachHangs.Find(MaKH);
            if (kh == null)
            {
                return HttpNotFound();
            }

            db.KhachHangs.Remove(kh);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}