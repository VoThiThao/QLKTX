namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NGUOIDUNG")]
    public partial class NGUOIDUNG
    {
        [Display(Name = "Tên đăng nhập")]
        [StringLength(20)]
        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        public string TenDangNhap { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        [StringLength(20)]
        public string MatKhau { get; set; }


        [Key]
        [Display(Name = "Mã NV")]
        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        [StringLength(15)]
        public string MaNV { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        [Display(Name = "Họ tên NV")]
        public string HoTenNV { get; set; }

        [Display(Name = "Chức vụ")]
        public int? ChucVu { get; set; }
    }
}
