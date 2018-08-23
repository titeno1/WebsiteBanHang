using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    [MetadataTypeAttribute(typeof(NhaCungCapMetadata))]
    public partial class NhaCungCap
    {
        internal sealed class NhaCungCapMetadata {
            [Display(Name = "Mã Nhà Cung Cấp")]
            public int MaNCC { get; set; }

            [Display(Name = "Tên Nhà Cung Cấp")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            public string TenNCC { get; set; }

            [Display(Name = "Địa Chỉ Nhà Cung Cấp")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            public string DiaChi { get; set; }

            [Display(Name = "Email Nhà Cung Cấp")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            [RegularExpression(@"^[a-z][a-z0-9_\.]{5,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$", ErrorMessage = "Email không hợp lệ!")]
            public string Email { get; set; }

            [Display(Name = "SĐT Nhà Cung Cấp")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            [RegularExpression(@"(09|01[2|6|8|9])+([0-9]{8})\b", ErrorMessage = "{0} không hợp lệ!")]
            public string SoDienThoai { get; set; }

            [Display(Name = "Fax Nhà Cung Cấp")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            public string Fax { get; set; }
        }
    }
}