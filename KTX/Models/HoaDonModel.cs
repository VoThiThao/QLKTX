using Models.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KTX.Models
{
    public class HoaDonModel
    {
        private DBKTX context;

        public HoaDonModel()
        {
            context = new DBKTX();
        }

        [Required(ErrorMessage = "Nhập mã hóa đơn")]
        [Display(Name = "Mã hóa đơn")]
        public string MaHD
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Nhập mã nhân viên")]
        [Display(Name = "Mã nhân viên")]
        public string MaNV
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Nhập mã phòng")]
        [Display(Name = "Mã phòng")]
        public string MaPhong
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Nhập mã ngày ghi")]
        [Display(Name = "Ngày ghi")]
        public DateTime NgayGhi
        {
            get;
            set;
        }

        // models
        public List<HOADON> listAll()
        {
            return context.HOADONs.ToList();
        }


        public List<HOADON> ListWhereAll(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
                return context.HOADONs.Where(x => x.MaHD.Contains(searchString)).ToList();
            return context.HOADONs.ToList();

        }
        public String Insert(HOADON enity)
        {
            context.HOADONs.Add(enity);
            context.SaveChanges();

            return enity.MaHD;
        }
        public HOADON Find(string maHD)
        {
            return context.HOADONs.Find(maHD);

        }
        public HOADON GetByMaHD(string maHD)
        {
            return context.HOADONs.SingleOrDefault(x => x.MaHD == maHD);
        }

        public bool Update(HOADON maHD)
        {
            try
            {
                var hd = context.HOADONs.Select(x => x).Where(x => x.MaHD == maHD.MaHD).FirstOrDefault();
                hd.MaHD = maHD.MaHD;
                hd.MaNV = maHD.MaNV;
                hd.MaPhong = maHD.MaPhong;
                hd.NgayGhi = maHD.NgayGhi;
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public void Delete(string maHD)
        {
            try
            {
                var hd = context.HOADONs.FirstOrDefault(x => x.MaHD.Contains(maHD));
                context.HOADONs.Remove(hd);
                context.SaveChanges();

            }
            catch (Exception e)
            {

            }
        }
    }
}