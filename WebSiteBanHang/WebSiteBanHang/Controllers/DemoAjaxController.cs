using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class DemoAjaxController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: DemoAjax
        public ActionResult DemoAjax() {
            return View();
        }
        // xử lý ajax actionlink
        public ActionResult loadAjaxActionLink() {

            System.Threading.Thread.Sleep(2000);
            return Content("hello Ajax");
        }
        // xử lý ajax BeginForm
        public ActionResult loadAjaxBeginForm(FormCollection f)
        {
            System.Threading.Thread.Sleep(2000);
            string KQ = f["txt1"].ToString();
            return Content(KQ);
        }
        //xử lý ajax jquery
        public ActionResult loadAjaxJquery(int a,int b)
        {
            System.Threading.Thread.Sleep(2000);
            return Content((a + b).ToString());
        }
        //trả về kết quả là 1 partial view
        public ActionResult SanPhamPartial()
        {
            var lstSanPham = db.SanPhams.ToList().Take(10);
            // var lstSanPham = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1);
            return PartialView(lstSanPham);

        }
    }
}