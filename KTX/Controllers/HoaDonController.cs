using KTX.Models;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
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
                var hdon = new HoaDonModel();
                if (hdon.Find(hoaDon.MaHD) != null)
                {
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
                    SetAlert("Tạo mới hóa đơn không thành công", "success");
                    return RedirectToAction("Create", "HoaDon");

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
                    return RedirectToAction("Index", "HoaDon");

                }
                else
                {
                    ModelState.AddModelError("", "Thêm người dùng không thành công");
                }
            }
            return View();
        }

        public ActionResult Delete(string maHD)
        {
            new HoaDonModel().Delete(maHD);
            return RedirectToAction("Index", "HoaDon");
        }
        public void Export()
        {
            try
            {
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet worksheet = Ep.Workbook.Worksheets.Add("Export");
                HoaDonModel hoaDon = new HoaDonModel();
                worksheet.Cells["A1"].Value = "Đơn vị";
                worksheet.Cells["B1"].Value = "Kí túc zá ĐHQN";

                worksheet.Cells["A2"].Value = "Ngày";
                worksheet.Cells["B2"].Value = string.Format("{0: dd MM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

                worksheet.Cells["C3"].Value = "HÓA ĐƠN";

                worksheet.Cells["A5"].Value = "Mã hóa đơn";
                worksheet.Cells["B5"].Value = "Mã nhân viên";
                worksheet.Cells["C5"].Value = "Mã phòng";
                worksheet.Cells["D5"].Value = "Ngày ghi";
                worksheet.Cells["E5"].Value = "Tổng tiền";
                int row = 6;
                foreach (var p in hoaDon.listAll())
                {


                    worksheet.Cells[string.Format("A{0}", row)].Value = p.MaHD;
                    worksheet.Cells[string.Format("B{0}", row)].Value = p.MaNV;
                    worksheet.Cells[string.Format("C{0}", row)].Value = p.MaPhong;
                    worksheet.Cells[string.Format("D{0}", row)].Value = p.NgayGhi;
                    //worksheet.Cells[string.Format("E{0}", row)].Value = item.Country;
                    row++;
                }
                //formatHeading


                worksheet.Cells["A1 : AZ"].AutoFitColumns();
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment: filename=" + "ExportHoaDon.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();

            }
            catch (Exception ex)
            {
                ViewBag.Result = ex.Message;
            }


        }

    }
}