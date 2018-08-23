using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class TimKiemController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: TimKiem
        public ActionResult KQTimKiem(string sTuKhoa, int? page)
        {
            //tìm kiếm theo sản phẩm
            var lstSP = db.SanPhams.Where(n => n.TenSP.Contains(sTuKhoa));
            //thực hiện chức năng phân trang
            if (Request.HttpMethod != "GET") {
                page = 1;
            }
            int PageSize = 8;
            int PageNumber = (page ?? 1);
            ViewBag.TuKhoa = sTuKhoa;
            return View(lstSP.OrderBy(n=>n.DonGia).ToPagedList(PageNumber,PageSize));
        }
        [HttpPost]
        public ActionResult LayTuKhoa(string sTuKhoa)
        {
            //gọi về hàm get tìm kiếm
            return RedirectToAction("KQTimKiem",new { @sTuKhoa = sTuKhoa }) ;
        }
        public ActionResult KQTimKiemPartial(string sTuKhoa, int? page) {
            var lstSP = db.SanPhams.Where(n => n.TenSP.Contains(sTuKhoa));
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int PageSize = 8;
            int PageNumber = (page ?? 1);
            ViewBag.TuKhoa = sTuKhoa;
            return PartialView(lstSP.OrderBy(n => n.DonGia).ToPagedList(PageNumber, PageSize));
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}