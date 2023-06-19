using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Do_An.Models;

namespace Do_An.Controllers
{
    public class NhaXuatBanController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: NhaXuatBan

        public PartialViewResult NhaXuatBanPartial()
        {
            return PartialView(db.NhaXuatBans.Take(5).OrderBy(n=>n.TenNXB).ToList());
        }
        public ViewResult SachTheoNXB(int MaNXB = 0)
        {
            //kiểm tra nhà xuất bản có tồn tại?
            NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //truy xuất các quyển sách theo nhà xuất bản
            List<Sach> sach = db.Saches.Where(n => n.MaNXB == MaNXB).OrderBy(n => n.GiaBan).ToList();
            if (sach.Count == 0)
            {
                ViewBag.sach = "Không có sách gì trong chủ đề này";
            }
            return View(sach);
        }

        public ViewResult DanhMucNXB()
        {
            return View(db.NhaXuatBans.ToList());
        }
    }
}