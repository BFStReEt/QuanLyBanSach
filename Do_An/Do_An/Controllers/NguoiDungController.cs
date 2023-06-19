using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Do_An.Models;

namespace Do_An.Controllers
{
    public class NguoiDungController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DangKy(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                //Chèn dữ liệu vào bảng khách hàng
                db.KhachHangs.Add(kh);
                //Lưu vào csdl
                db.SaveChanges();
            }
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string sTaiKhoan = f["txtTaiKhoan"].ToString();
            string sMatKhau = f.Get("txtMatKhau").ToString();
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
            if (kh != null)
            {
                ViewBag.ThongBao = "Đăng nhập thành công!";
                Session["TaiKhoan"] = kh;

                if (kh.quyen == 1) // Quyền là admin
                {
                    // Điều hướng tới trang admin
                    return RedirectToAction("Index", "QuanLySanPham");
                }
                else if (kh.quyen == 0) // Quyền là user
                {
                    // Điều hướng tới trang người dùng
                    return RedirectToAction("Index", "Home");
                }
            }

            // Nếu không có quyền hoặc không đăng nhập thành công
            ViewBag.ThongBao = "Tên tài khoản hoặc mật khẩu không đúng!";
            return View();
        }
        public ActionResult DangXuat()
        {
            // Xóa dữ liệu phiên làm việc
            Session.Clear();

            // Đăng xuất người dùng
            FormsAuthentication.SignOut();

            // Chuyển hướng về trang chủ
            return RedirectToAction("Index", "Home");
        }



    }
}