using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using PagedList;
using PagedList.Mvc;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class SanPhamController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: SanPham
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SanPham(int? MaLoaiSP, int? MaNSX,int? page) {
            
            if (MaLoaiSP == null || MaNSX == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lstSanPham = db.SanPhams.Where(n => n.MaLoaiSP == MaLoaiSP && n.MaNSX == MaNSX && n.DaXoa == false);
            if (lstSanPham.Count() == 0) {
                return HttpNotFound();
            }
            //thực hiện chức năng phân trang
            int PageSize = 8;
            int PageNumber = (page ?? 1);
            ViewBag.MaLoaiSP = MaLoaiSP;
            ViewBag.MaNSX = MaNSX;

            return View(lstSanPham.OrderBy(n=>n.MaSP).ToPagedList(PageNumber,PageSize));
        }
        public ActionResult ChiTietSP(int? id,string tensp) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == id && n.DaXoa == false);
            if (sp == null) {
                return HttpNotFound();
            }
            return View(sp);
        }
        public ActionResult MenuLeftPartial() {
            var lstSP = db.SanPhams;
            return PartialView(lstSP);
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