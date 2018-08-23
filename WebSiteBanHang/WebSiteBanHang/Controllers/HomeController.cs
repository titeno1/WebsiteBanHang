using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
using System.Web.Security;

namespace WebSiteBanHang.Controllers
{
    public class HomeController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: Home
        public ActionResult Index()
        {
            var lstSanPham = db.SanPhams.Where(n => n.MaLoaiSP == 1 && n.Moi == 1);
            var lstSanPham2 = db.SanPhams.Where(m => m.MaLoaiSP == 2 && m.Moi == 1);
            var lstSanPham3 = db.SanPhams.Where(y => y.MaLoaiSP == 3 && y.Moi == 1);
            var lstSanPham4 = db.SanPhams.Where(o => o.MaLoaiSP == 1).ToList().Take(4);
            var lstSanPham5 = db.SanPhams.Where(p => p.MaLoaiSP == 2).ToList().Take(4);
            var lstSanPham6 = db.SanPhams.Where(u => u.MaLoaiSP == 3).ToList().Take(4);
            ViewBag.ListSP = lstSanPham;
            ViewBag.ListSP2 = lstSanPham2;
            ViewBag.ListSP3 = lstSanPham3;
            ViewBag.ListSP4 = lstSanPham4;
            ViewBag.ListSP5 = lstSanPham5;
            ViewBag.ListSP6 = lstSanPham6;
            return View();
        }
        //Tạo partial view
        public ActionResult SanPhamPartial()
        {
            // var lstSanPham = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1);
            return PartialView();

        }
        public ActionResult MenuPartial()
        {
            var lstSP = db.SanPhams;
            return PartialView(lstSP);
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            ViewBag.CauHoi = new SelectList(LoadCauHoi());
            return PartialView();
        }
        [HttpPost]
        public ActionResult DangKy(ThanhVien tv, FormCollection f)
        {
            ViewBag.CauHoi = new SelectList(LoadCauHoi());
            if (this.IsCaptchaValid("Capcha is not valid"))
            {
                if (ModelState.IsValid)
                {
                    ViewBag.ThongBao = "Thêm thành công";
                    //thêm khách hàng
                    tv.MaLoaiTV = 2;
                    db.ThanhViens.Add(tv);
                    db.SaveChanges();
                    
                }
                return Content("<script>window.location.reload();</script>");
            }
           
            ViewBag.ThongBao = "Thêm thất bại";
            return Content("Thêm thất bại");
        }
        //tạo câu hỏi
        public List<string> LoadCauHoi()
        {
            List<string> lstCauHoi = new List<string>();
            lstCauHoi.Add("Bộ phim mà bạn yêu thích ?");
            lstCauHoi.Add("Con vật mà bạn yêu thích ?");
            lstCauHoi.Add("Ngôi trường câp 1 của bạn tên gì ?");
            lstCauHoi.Add("Diễn viên mà bạn yêu thích ?");
            return lstCauHoi;
        }
        // đăng nhập tài khoản thành viên
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {

            //kiểm tra đăng nhập
            string sTaiKhoan = f["txtTenDangNhap"].ToString();
            string sMatKhau = f["txtMatKhau"].ToString();

            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
            if (tv != null)
            {
                //Lấy ra list quyền của thành viên tương ứng với loại thành viên
                var lstQuyen = db.LoaiThanhVien_Quyen.Where(n => n.MaLoaiTV == tv.MaLoaiTV);
                //Duyệt list quyền
                string Quyen = "";
                if (lstQuyen.Count() != 0)
                {
                    foreach (var item in lstQuyen)
                    {
                        Quyen += item.Quyen.MaQuyen + ",";
                    }
                    Quyen = Quyen.Substring(0, Quyen.Length - 1); //Cắt dấu ","
                    PhanQuyen(tv.TaiKhoan.ToString(), Quyen);
                    Session["TaiKhoan"] = tv;
                    return Content("<script>window.location.reload();</script>");
                }
            }
            return Content("Tài Khoản hoặc Mật Khẩu không đúng !");
        }
        public void PhanQuyen(string TaiKhoan, string Quyen)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1,
                                          TaiKhoan, //user
                                          DateTime.Now, //Thời gian bắt đầu
                                          DateTime.Now.AddHours(3), //Thời gian kết thúc
                                          false, //Ghi nhớ?
                                          Quyen, // "DangKy,QuanLyDonHang,QuanLySanPham"
                                          FormsAuthentication.FormsCookiePath);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        public ActionResult LoiPhanQuyen()
        {

            return View();
        }
        public ActionResult dangkydemo()
        {
            return View();
        }
        public ActionResult Contact() {
            return View();
        }
        public ActionResult About() {
            return View();
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