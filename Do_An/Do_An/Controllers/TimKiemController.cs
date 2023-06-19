using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Do_An.Models;
using PagedList;

namespace Do_An.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection f,int? page)
        {
            string sTuKhoa = f["txtTimKiem"].ToString();
            List<Sach> ListKQTK = db.Saches.Where(n => n.TenSach.Contains(sTuKhoa)).ToList();


            //Phân trang
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            if(ListKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào ";
                return View(db.Saches.OrderBy(n=>n.TenSach).ToPagedList(pageNumber,pageSize));
            }

            ViewBag.ThongBao = "Đã tìm thấy " + ListKQTK.Count + "kết quả";
            return View(ListKQTK.OrderBy(n=>n.TenSach).ToPagedList(pageNumber,pageSize));
        }
    }
}