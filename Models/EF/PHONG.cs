namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHONG")]
    public partial class PHONG
    {
        [Key]
        [StringLength(15)]
        public string MaPhong { get; set; }

        public int? SoCho { get; set; }

        [StringLength(30)]
        public string TinhTrang { get; set; }

        [Required]
        [StringLength(15)]
        public string MaCTD { get; set; }

        [Required]
        [StringLength(15)]
        public string MaCTN { get; set; }
    }
}
