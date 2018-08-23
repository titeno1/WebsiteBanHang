using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    [MetadataTypeAttribute(typeof(QuyenMetadata))]
    public partial class Quyen
    {
        internal sealed class QuyenMetadata {

            [Display(Name = "Mã Quyền")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            public string MaQuyen { get; set; }

            [Display(Name = "Tên Quyền")]
            [Required(ErrorMessage = " Hãy Nhập {0} !")]
            public string TenQuyen { get; set; }

        }
    }
}