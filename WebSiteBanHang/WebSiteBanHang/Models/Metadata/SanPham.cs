using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    [MetadataTypeAttribute(typeof(SanPhamMetadata))]
    public partial class SanPham
    {
        internal sealed class SanPhamMetadata {
            [Display(Name = "Mã Sản Phẩm")]
            public int MaSP { get; set; }

            [Display(Name ="Tên Sản Phẩm :")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} từ 5 đến 20 ký tự !")]
            public string TenSP { get; set; }

            [Display(Name = "Đơn Giá:")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            [RegularExpression(@"^[0-9]*$", ErrorMessage = "{0} không hợp lệ!")]
            public Nullable<decimal> DonGia { get; set; }

            [Display(Name = "Ngày Cập Nhật:")]
            [DataType(DataType.Date)]
            public Nullable<System.DateTime> NgayCapNhap { get; set; }

            [Display(Name = "Cấu Hình:")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            public string CauHinh { get; set; }

            [Display(Name = "Mô Tả:")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            public string MoTa { get; set; }

            [Display(Name = "Hình Ảnh:")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            public string HinhAnh { get; set; }

            [Display(Name = "Số Lượng Tồn")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            [RegularExpression(@"^[0-9]*$", ErrorMessage = "{0} không hợp lệ!")]
            public Nullable<int> SoLuongTon { get; set; }

            [Display(Name = "lượt Xem:")]
            public Nullable<int> LuotXem { get; set; }

            [Display(Name = "Lượt Bình Chọn:")]
            public Nullable<int> LuotBinhChon { get; set; }

            [Display(Name = "Lượt Bình Luận:")]
            public Nullable<int> LuotBinhLuan { get; set; }

            [Display(Name = "Số Lần Mua:")]
            public Nullable<int> SoLanMua { get; set; }
            public Nullable<int> Moi { get; set; }

            [Display(Name = "Nhà Cung Cấp:")]
            public Nullable<int> MaNCC { get; set; }

            [Display(Name = "Nhà Sản Xuất:")]
            public Nullable<int> MaNSX { get; set; }

            [Display(Name = "Loại Sản Phẩm:")]
            public Nullable<int> MaLoaiSP { get; set; }

            public Nullable<bool> DaXoa { get; set; }
            public string HinhAnh1 { get; set; }
            public string HinhAnh2 { get; set; }
            public string HinhAnh3 { get; set; }
            public string HinhAnh4 { get; set; }

        }
    }
}