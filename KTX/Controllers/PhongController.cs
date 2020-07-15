using KTX.Models;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KTX.Controllers
{
    public class PhongController : BaseController
    {
        // GET: Phong
        public ActionResult Index(string searchString)
        {
            if (searchString == "")
            {
                SetAlert("Vui lòng nhập thông tin tìm kiếm", "error");
            }
            var sv = new PhongModel();
            var model = sv.ListWhereAll(searchString);
            @ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(string maPhong)
        {
            var phong = new PhongModel().getByMaPhong(maPhong);
            return View(phong);
        }
        [HttpPost]

        public ActionResult Create(PHONG phong)
        {
            if (ModelState.IsValid)
            {
                var dao = new PhongModel();
                if (dao.Find(phong.MaPhong) != null)
                {
                    SetAlert("Mã phòng này đã có trong hệ thống", "error");
                    return RedirectToAction("Create", "Phong");
                }
                String result = dao.Insert(phong);
                if (!String.IsNullOrEmpty(result))
                {
                    SetAlert("Thêm phòng thành công", "success");
                    return RedirectToAction("Index", "Phong");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm phòng không thành công");
                }
            }
            return View();
        }
        public ActionResult Edit(PHONG phong)
        {
            if (ModelState.IsValid)
            {
                var dao = new PhongModel();

                var result = dao.Update(phong);
                if (result)
                {
                    SetAlert("Chỉnh sửa thông tin phòng thành công ", "success");
                    return RedirectToAction("Index", "Phong");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật người dùng không thành công");
                }
            }
            return View();
        }
        public ActionResult Delete(string MaPhong)
        {
            try
            {
                PhongModel ph = new PhongModel();
                ph.Delete(MaPhong);

                return RedirectToAction("Index", "Phong");
            }
            catch (Exception ex)
            {
                SetAlert("Phòng này còn có sinh viên ở ", "error");
                return RedirectToAction("Index", "Phong");
            }
        }
    }
}