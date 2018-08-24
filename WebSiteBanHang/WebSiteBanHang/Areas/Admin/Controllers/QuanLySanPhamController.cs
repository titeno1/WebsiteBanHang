using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    [Authorize(Roles= "QuanLySanPham")]
    public class QuanLySanPhamController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: Admin/QuanLySanPham
       
        public ActionResult Index(int? page, string searchTerm)
        {
            var lstSP = db.SanPhams.Where(n => n.DaXoa == false);
            if (!String.IsNullOrEmpty(searchTerm))
    {
                lstSP = db.SanPhams.Where(b => b.TenSP.Contains(searchTerm));
    }
            int PageSize = 6;
            int PageNumber = (page ?? 1);
            return View(lstSP.OrderBy(n => n.MaSP).ToPagedList(PageNumber, PageSize));
        }
       
        [HttpGet]
        // GET: Admin/SanPhams/Create
        public ActionResult ThemPartial()
        {
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoai), "MaLoaiSP", "TenLoai");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            return PartialView();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ThemPartial(SanPham sp,HttpPostedFileBase[] HinhAnh)
        {
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoai), "MaLoaiSP", "TenLoai");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            int loi = 0;
            for (var i = 0; i < HinhAnh.Count(); i++) {
                if (HinhAnh[i] != null) {
                    //kiểm tra hình ảnh
                    if (HinhAnh[i].ContentLength > 0)
                    {
                        //kiem tra định dạng hình ảnh
                        if (HinhAnh[i].ContentType != "image/jpeg" && HinhAnh[i].ContentType != "image/png" && HinhAnh[i].ContentType != "image/gif" && HinhAnh[i].ContentType != "image/jpg")
                        {
                            ViewBag.upload += "hình ảnh" + i + "không hợp lệ <br/>";
                            loi++;
                        }
                        else {
                            //lấy tên hình ảnh
                            var filename = Path.GetFileName(HinhAnh[0].FileName);
                            //lấy hình ảnh chuyển vào thư mục hình ảnh
                            var path = Path.Combine(Server.MapPath("~/Content/images/ImagesSanPham"), filename);
                            //nếu hình ảnh chứa trong thu mục đó rồi thì xuất thông báo
                          
                                sp.HinhAnh = HinhAnh[0].FileName;
                                //sp.HinhAnh = HinhAnh[1].FileName;
                                //sp.HinhAnh = HinhAnh[2].FileName;
                                //sp.HinhAnh = HinhAnh[3].FileName;
                                //sp.HinhAnh = HinhAnh[4].FileName;
                            
                        }
                    }
                }
            }         
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
       }       


        [ValidateInput(false)]
        [HttpGet]
        public ActionResult ChinhSua(int? id)
        {
            //lấy sản phẩm cần chỉnh sửa
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoai), "MaLoaiSP", "TenLoai", sp.MaLoaiSP);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX", sp.MaNSX);
            return View(sp);
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChinhSua(SanPham sp,HttpPostedFileBase[] HinhAnh)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoai), "MaLoaiSP", "TenLoai", sp.MaLoaiSP);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX", sp.MaNSX);
            int loi = 0;
            for (var i = 0; i < HinhAnh.Count(); i++)
            {
                if (HinhAnh[i] != null)
                {
                    //kiểm tra hình ảnh
                    if (HinhAnh[i].ContentLength > 0)
                    {
                        //kiem tra định dạng hình ảnh
                        if (HinhAnh[i].ContentType != "image/jpeg" && HinhAnh[i].ContentType != "image/png" && HinhAnh[i].ContentType != "image/gif" && HinhAnh[i].ContentType != "image/jpg")
                        {
                            ViewBag.upload += "hình ảnh" + i + "không hợp lệ <br/>";
                            loi++;
                        }
                        else
                        {
                            //lấy tên hình ảnh
                            var filename = Path.GetFileName(HinhAnh[0].FileName);
                            //lấy hình ảnh chuyển vào thư mục hình ảnh
                            var path = Path.Combine(Server.MapPath("~/Content/images/ImagesSanPham"), filename);
                            //nếu hình ảnh chứa trong thu mục đó rồi thì xuất thông báo
                            
                                sp.HinhAnh = HinhAnh[0].FileName;
                                //sp.HinhAnh = HinhAnh[1].FileName;
                                //sp.HinhAnh = HinhAnh[2].FileName;
                                //sp.HinhAnh = HinhAnh[3].FileName;
                                //sp.HinhAnh = HinhAnh[4].FileName;
                           
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(sp).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
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