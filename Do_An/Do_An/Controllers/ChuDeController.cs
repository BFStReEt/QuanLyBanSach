using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Do_An.Models;

namespace Do_An.Controllers
{
    public class ChuDeController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: ChuDe
        public ActionResult ChuDePartial()
        {

            return PartialView(db.ChuDes.Take(7).ToList());
        }
        public ViewResult SachTheoChuDe(int MaChuDe = 0)
        {
            //kiểm tra chủ đề có tồn tại?
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaChuDe == MaChuDe);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //truy xuất các quyển sách theo chủ đề
            List<Sach> sach = db.Saches.Where(n => n.MaChuDe == MaChuDe).OrderBy(n => n.GiaBan).ToList();
            if (sach.Count == 0)
            {
                ViewBag.sach = "Không có sách gì trong chủ đề này";
            }
            return View(sach);
        }
        //Hiển thị các chủ đề trong button Chủ Đề Khác
        public ViewResult DanhMucChuDe()
        {
            return View(db.ChuDes.ToList());
        }
    }
}