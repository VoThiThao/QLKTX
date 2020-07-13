using Models.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KTX.Models
{
    public class QLDsModel
    {
        [Key]
        [Required(ErrorMessage = "Mã điện không được trống")]
        [Display(Name = "Mã điện")]
        [StringLength(15)]
        public string MaDien { set; get; }
        [Required(ErrorMessage = "Bạn cần nhập mã phòng")]
        [Display(Name = "Mã phòng")]
        [StringLength(15)]
        public string MaPhong { set; get; }
        [Required(ErrorMessage = "Bạn cần nhập ngày")]
        [Display(Name = "Ngày ghi")]
        public DateTime NgayGhi { set; get; }
        [Required(ErrorMessage = "Bạn cần nhập CSD")]
        [Display(Name = "Chỉ số đầu")]
        public int CSD { set; get; }
        [Required(ErrorMessage = "Bạn cần nhập CSC")]
        [Display(Name = "Chỉ số cuối")]
        public int CSC { set; get; }
        [Required(ErrorMessage = "Bạn cần nhập đơn giá")]
        [Display(Name = "Đơn giá")]
        public double DonGia { set; get; }
        public double ThanhTienD
        {
            get
            {
                return (CSC - CSD) * DonGia;
            }
        }
        public virtual PHONG PHONG { get; set; }
        private DBKTX db;

        public QLDsModel()
        {
            db = new DBKTX();
        }
        public String Insert(DIEN entityDien)
        {
            db.DIENs.Add(entityDien);
            try { db.SaveChanges(); }
            catch (Exception e)
            {
                Console.WriteLine("Mã phòng không có trong CSDL!", e.Message);
            }
            return entityDien.MaDien;
        }

        public bool Update(DIEN entityDien)
        {
            try
            {
                var dien = db.DIENs.Select(x => x).Where(x => x.MaDien == entityDien.MaDien).FirstOrDefault();

                dien.MaPhong = entityDien.MaPhong;
                dien.NgayGhi = entityDien.NgayGhi;
                dien.CSD = entityDien.CSD;
                dien.CSC = entityDien.CSC;
                dien.DonGia = entityDien.DonGia;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Sửa không thành công vui lòng kiểm tra lại!", e.Message);
                return false;
            }
            return true;
        }

        public DIEN getByMaDien(string MaDien)
        {
            return db.DIENs.SingleOrDefault(x => x.MaDien == MaDien);
        }

        public DIEN Find(string MaDien)
        {
            return db.DIENs.Find(MaDien);

        }

        public List<DIEN> ListAll()
        {
            return db.DIENs.ToList();
        }
        public List<DIEN> ListWhereAll(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
                return db.DIENs.Where(x => x.MaPhong.Contains(searchString)).ToList();
            return db.DIENs.ToList();

        }

        public void Delete(string maDien)
        {
            try
            {

                var dien = db.DIENs.FirstOrDefault(x => x.MaDien.Contains(maDien));
                if (dien != null)
                {
                    db.DIENs.Remove(dien);
                    db.SaveChanges();

                }


            }

            catch (Exception e)
            {
                Console.WriteLine("Xóa không thành công vui lòng kiểm tra lại!", e.Message);
            }


        }


    }

}
