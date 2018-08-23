using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    [MetadataTypeAttribute(typeof(ThanhVienMetadata))]
    public partial class ThanhVien
    {
        internal sealed class ThanhVienMetadata {
            [Display(Name = "Mã Thành Viên")]
            public int MaThanhVien { get; set; }

            [Display(Name = "Tài Khoản")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            [StringLength(20, MinimumLength = 5, ErrorMessage = "Tài Khoản từ 5 đến 20 ký tự !")]
            public string TaiKhoan { get; set; }

            [Display(Name = "Mật Khẩu")]
            [Required(ErrorMessage = " Hãy nhập {0} !")]
            [StringLength(20, MinimumLength = 3, ErrorMessage = "Mật khẩu từ 3 đến 20 ký tự !")]
            public string MatKhau { get; set; }

            [Display(Name = "Nhập Lại Mật Khẩu")]
            [Required(ErrorMessage = " {0} !")]
            [Compare("MatKhau", ErrorMessage = "Mật Khẩu không trùng khớp !")]
            public string NhapLaiMatKhau { get; set; }

            [Display(Name = "Họ Tên")]
            [Required(ErrorMessage = " Hãy nhập {0} !")]
            [StringLength(50, MinimumLength = 10, ErrorMessage = "{0} không được quá dài hay quá ngắn !")]
            public string HoTen { get; set; }

            [Display(Name = "Địa Chỉ")]
            [Required(ErrorMessage = " Hãy nhập {0} !")]
            [StringLength(200, MinimumLength = 10, ErrorMessage = "{0} ít nhất nhập 10 ký tự !")]
            public string DiaChi { get; set; }

            [Display(Name = "Email")]
            [Required(ErrorMessage = " Hãy nhập {0} !")]
            [RegularExpression(@"^[a-z][a-z0-9_\.]{5,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$", ErrorMessage = "Email không hợp lệ!")]
            public string Email { get; set; }

            [Display(Name = "Số Điện Thoại")]
            [Required(ErrorMessage = " Hãy nhập {0} !")]
            [RegularExpression(@"(09|01[2|6|8|9])+([0-9]{8})\b", ErrorMessage = "{0} không hợp lệ!")]
            public string SoDienThoai { get; set; }

            [Display(Name = "Câu Hỏi")]
            public string CauHoi { get; set; }

            [Display(Name = "Câu Trả Lời")]
            [Required(ErrorMessage = " Hãy nhập {0} !")]
            public string CauTraLoi { get; set; }

            [Display(Name = "Mã Loại Thành Viên")]
            public Nullable<int> MaLoaiTV { get; set; }
        }
    }
}