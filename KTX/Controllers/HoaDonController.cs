using KTX.Models;
using Models.EF;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace KTX.Controllers
{
    public class HoaDonController : BaseController
    {
        // GET: HoaDon
        public ActionResult Index(string searchString)
        {

            var hoaDon = new HoaDonModel();
            if (searchString == "")
            {
                SetAlert("Vui lòng nhập thông tin tìm kiếm", "error");
            }
            var model = hoaDon.ListWhereAll(searchString);
            return View(model);
        }

        //tạo mới
        public ActionResult Create(HOADON hoaDon)
        {
            if (ModelState.IsValid)
            {
                TinhTongTien();
                var hdon = new HoaDonModel();
                var hdon1 = new NguoiDungModel();
                var hdon2 = new PhongModel();
                if (hdon.Find(hoaDon.MaHD) != null)
                {
                    SetAlert("Mã hóa đơn đã tồn tại", "error");
                    return RedirectToAction("Create", "HoaDon");
                }
                if (hdon1.Find(hoaDon.MaNV) == null)
                {
                    SetAlert("Mã nhân viên không có trong CSDL", "error");
                    return RedirectToAction("Create", "HoaDon");
                }
                if (hdon2.Find(hoaDon.MaPhong) == null)
                {
                    SetAlert("Mã phòng không có trong CSDL", "error");
                    return RedirectToAction("Create", "HoaDon");
                }
                String result = hdon.Insert(hoaDon);
                if (!String.IsNullOrEmpty(result))
                {
                    SetAlert("Tạo mới hóa đơn thành công", "success");
                    return RedirectToAction("Index", "HoaDon");
                }
                else
                {
                    ModelState.AddModelError("", "Tạo mới hóa đơn không thành công");

                }
            }
            return View();
        }
        //thêm
        public ActionResult Edit(string maHD)
        {
            var hoaDon = new HoaDonModel().GetByMaHD(maHD);
            return View(hoaDon);
        }
        [HttpPost]
        public ActionResult Edit(HOADON hoaDon)
        {
            if (ModelState.IsValid)
            {
                var hd = new HoaDonModel();
                var result = hd.Update(hoaDon);
                if (result)
                {
                    SetAlert("Sửa hóa đơn thành công", "success");
                    return RedirectToAction("Index", "HoaDon");

                }
                else
                {
                    ModelState.AddModelError("", "Sửa hóa đơn không thành công");
                }
            }
            return View();
        }

        public ActionResult Delete(string maHD)
        {
            new HoaDonModel().Delete(maHD);
            return RedirectToAction("Index", "HoaDon");
        }
        public void ExportExcel()
        {
            HoaDonModel hoaDon = new HoaDonModel();
            try
            {
                ExcelPackage pck = new ExcelPackage();
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

                ws.Cells["A1"].Value = "Đơn vị";
                ws.Cells["B1"].Value = "Kí túc zá ĐHQN";

                ws.Cells["A2"].Value = "Ngày";
                ws.Cells["B2"].Value = string.Format("{0: dd MM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

                ws.Cells["C3"].Value = "HÓA ĐƠN";

                ws.Cells["A5"].Value = "Mã hóa đơn";
                ws.Cells["B5"].Value = "Mã nhân viên";
                ws.Cells["C5"].Value = "Mã phòng";
                ws.Cells["D5"].Value = "Ngày ghi";
                ws.Cells["E5"].Value = "Tổng tiền";

                int rowStar = 6;
                foreach (var item in hoaDon.listAll())
                {
                   
                    ws.Cells[string.Format("A{0}", rowStar)].Value = item.MaHD;
                    ws.Cells[string.Format("B{0}", rowStar)].Value = item.MaNV;
                    ws.Cells[string.Format("C{0}", rowStar)].Value = item.MaPhong;
                    ws.Cells[string.Format("D{0}", rowStar)].Value = item.NgayGhi.ToString("MM/dd/yyyy");
                    //tính tổng tiền
                    ws.Cells[string.Format("E{0}", rowStar)].Value = hoaDon.tongTien();
                    rowStar++;
                }
                ws.Cells["A1 : AZ"].AutoFitColumns();
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelExport.xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
            }
            catch(Exception ex)
            {
                ViewBag.Result = ex.Message;
            }
           
        }
        public double TinhTongTien()
        {
            QLDsModel qld = new QLDsModel();
            QLNsModel qln = new QLNsModel();
            HoaDonModel donModel = new HoaDonModel();
            double tongTien = 0;

            if (qld.MaPhong == donModel.MaPhong && qln.MaPhong == donModel.MaPhong)
            {

                tongTien = ((qld.CSC - qld.CSD) * qld.DonGia) + ((qln.CSC - qln.CSD) * qln.DonGia);
                return tongTien;
            }
            return 0;
        }
    }
}