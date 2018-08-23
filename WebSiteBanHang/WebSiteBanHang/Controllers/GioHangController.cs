using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        //lấy giỏ hàng
        public List<itemGioHang> LayGioHang()
        {
            //giỏ hang đã tồn tại
            List<itemGioHang> lstGioHang = Session["GioHang"] as List<itemGioHang>;
            if (lstGioHang == null) {
                //Nếu giỏ hàng chưa tồn tại thì khởi tạo giỏ hàng 
                lstGioHang = new List<itemGioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;

        }
        //thêm giỏ hàng
        public ActionResult ThemGioHang(int id, string strURL) {
            //kiểm tra sản phẩm có tồn tại trong CSDL hay không
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy list giỏ hàng
            List<itemGioHang> lstGioHang = LayGioHang();
            //trường hợp 1 sản phẩm đã tồn tại trong giỏ hàng
            itemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == id);
            if (spCheck != null) {
                if (sp.SoLuongTon < spCheck.SoLuong) {
                    return Content("ahihi");
                }
                spCheck.SoLuong++;
                spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                return Redirect(strURL);
            }
            
            itemGioHang itemGH = new itemGioHang(id);
            if (sp.SoLuongTon < itemGH.SoLuong)
            {
                return Content("ahihi");
            }
            lstGioHang.Add(itemGH);
            return Redirect(strURL);

        }

        public double TinhTongSoLuong() {
            List<itemGioHang> lstGioHang = Session["GioHang"] as List<itemGioHang>;
            if(lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.SoLuong);
        }
        //Tính tổng tiền
        public decimal TinhTongTien()
        {
            List<itemGioHang> lstGioHang = Session["GioHang"] as List<itemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.ThanhTien);

        }

        // GET: GioHang
        public ActionResult XemGioHang()
        {
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            List<itemGioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        public ActionResult GioHangPartial() {
            if (TinhTongSoLuong() == 0) {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            List<itemGioHang> lstGioHang = LayGioHang();
            return PartialView(lstGioHang);
        }
        //Sửa item giỏ hàng
        public ActionResult SuaGioHang(int MaSP) {
            //kiểm tra session giỏ hang tồn tại chưa
            if (Session["GioHang"] == null) {
                return RedirectToAction("Index", "Home");
            }
            //kiểm tra sản phẩm có tồn tại trong CSDL hay không
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy list giỏ hàng từ session
            List<itemGioHang> lstGioHang = LayGioHang();
            itemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.GioHang = lstGioHang;
            return View(spCheck);
        }
        //cập nhat giỏ hàng
        public ActionResult CapNhatGioHang(itemGioHang itemGH) {
            SanPham spCheck = db.SanPhams.SingleOrDefault(n => n.MaSP == itemGH.MaSP);
            if (spCheck.SoLuongTon < itemGH.SoLuong) {
                return View("ThongBao");
            }
            List<itemGioHang> lstGH = LayGioHang();
            itemGioHang itemUpdate = lstGH.Find(n => n.MaSP == itemGH.MaSP);
            itemUpdate.SoLuong = itemGH.SoLuong;
            itemUpdate.ThanhTien = itemUpdate.SoLuong * itemUpdate.DonGia;
            return RedirectToAction("XemGioHang");
        }
        //xóa Giỏ hàng
        public ActionResult XoaGioHang(int MaSP) {
            //Kiểm tra session giỏ hàng tồn tại chưa 
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Kiểm tra sản phẩm có tồn tại trong CSDL hay không
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                //TRang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            //Lấy list giỏ hàng từ session
            List<itemGioHang> lstGioHang = LayGioHang();
            //Kiểm tra xem sản phẩm đó có tồn tại trong giỏ hàng hay không
            itemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Xóa item trong giỏ hàng
            lstGioHang.Remove(spCheck);
            return RedirectToAction("XemGioHang", lstGioHang);
        }
        //Đặt Hàng
        public ActionResult DatHang(KhachHang kh) {
            //kiểm tra session giỏ hang tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            KhachHang khachHang= new KhachHang(); ;
            if (Session["TaiKhoan"] == null)
            {
                //khách hàng vảng lai
                khachHang = kh;
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
            }
            else {
                // đối với khách hàng là thành viên
                ThanhVien tv = Session["TaiKhoan"] as ThanhVien;
                khachHang.TenKH = tv.HoTen;
                khachHang.DiaChi = tv.DiaChi;
                khachHang.Email = tv.Email;
                khachHang.SoDienThoai = tv.SoDienThoai;
                khachHang.MaThanhVien = tv.MaLoaiTV =2;
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
            }
            //thêm đơn hàng
            DonDatHang ddh = new DonDatHang();
            ddh.MaKH = khachHang.MaKH;
            ddh.NgayDat = DateTime.Now;
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;
            ddh.UuDai = 0;
            ddh.DaHuy = false;
            ddh.DaXoa = false;
            db.DonDatHangs.Add(ddh);
            db.SaveChanges();
            //them chi tiết đơn hàng
            List<itemGioHang> lstGH = LayGioHang();
            foreach (var item in lstGH) {
                ChiTietDonDatHang ctdh = new ChiTietDonDatHang();
                ctdh.MaDDH = ddh.MaDDH;
                ctdh.MaSP = item.MaSP;
                ctdh.TenSP = item.TenSP;
                ctdh.SoLuong = item.SoLuong;
                ctdh.DonGia = item.DonGia;
                db.ChiTietDonDatHangs.Add(ctdh);
               
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XemGioHang");
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