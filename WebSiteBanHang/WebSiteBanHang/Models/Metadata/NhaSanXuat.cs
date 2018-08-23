using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    [MetadataTypeAttribute(typeof(NhaSanXuatMetadata))]
    public partial class NhaSanXuat
    {
        internal sealed class NhaSanXuatMetadata {

            [Display(Name = "Mã Nhà Sản Xuất")]
            public int MaNSX { get; set; }

            [Display(Name = "Tên Nhà Sản Xuất")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            public string TenNSX { get; set; }

            [Display(Name = "Thông Tin Nhà Sản Xuất")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            public string ThongTin { get; set; }

            [Display(Name = "Logo Nhà Sản Xuất")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            public string Logo { get; set; }
        }
    }
}