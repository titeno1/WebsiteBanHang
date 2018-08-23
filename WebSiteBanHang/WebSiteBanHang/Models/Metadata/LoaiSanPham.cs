using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    [MetadataTypeAttribute(typeof(LoaiSanPhamMetadata))]
    public partial class LoaiSanPham
    {
        internal sealed class LoaiSanPhamMetadata
        {
            [Display(Name = "Mã Loại Sản Phẩm ")]
            public int MaLoaiSP { get; set; }
            [Display(Name = "Tên Loại Sản Phẩm")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            public string TenLoai { get; set; }

            [Display(Name = "Icon")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            public string Icon { get; set; }

            [Display(Name = "Bí Danh")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            public string BiDanh { get; set; }
        }
        }
}