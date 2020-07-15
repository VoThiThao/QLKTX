namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HOADON")]
    public partial class HOADON
    {
        [Key]
        [Required(ErrorMessage = "Nhập mã hóa đơn")]
        [StringLength(15)]
        public string MaHD { get; set; }

        [Required(ErrorMessage = "Nhập mã nhân viên")]
        [StringLength(15)]
        public string MaNV { get; set; }

        [Required(ErrorMessage = "Nhập mã phòng")]
        [StringLength(15)]
        public string MaPhong { get; set; }

        [Required(ErrorMessage = "Nhập mã ngày ghi")]
        public DateTime NgayGhi { get; set; }


        //tinh tong tien
     
        public virtual NGUOIDUNG NGUOIDUNG { get; set; }

        public virtual PHONG PHONG { get; set; }
    }
}
