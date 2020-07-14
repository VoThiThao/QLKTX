using KTX.Models;
using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KTX.Controllers
{
    public class QLNsController : BaseController
    {
        //
        // GET: /QLNs/
        public ActionResult Index(string searchString)
        {

            var nuoc = new QLNsModel();
            if (searchString == "")
            {
                SetAlert("Vui lòng nhập nội dung tìm kiếm", "error");
            }

            var model = nuoc.ListWhereAll(searchString);
            @ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Create(NUOC nuoc)
        {
            if (ModelState.IsValid)
            {
                var dao = new QLNsModel();
                if (dao.Find(nuoc.MaNuoc) != null)
                {
                    SetAlert("Mã nước đã tồn tại", "error");
                    return RedirectToAction("Create", "QLNs");
                }
                String result = dao.Insert(nuoc);
                if (!String.IsNullOrEmpty(result))
                {
                    SetAlert("Thêm nước thành công", "success");
                    return RedirectToAction("Index", "QLNs");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm nước không thành công");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(string maNuoc)
        {
            var nuoc = new QLNsModel().getByMaNuoc(maNuoc);
            return View(nuoc);
        }
        [HttpPost]
        public ActionResult Edit(NUOC nuoc)
        {
            if (ModelState.IsValid)
            {
                var dao = new QLNsModel();
                if (dao.Find(nuoc.MaNuoc) != null)
                {
                    SetAlert("Mã nước không sửa được", "error");
                    return RedirectToAction("Edit", "QLNs");
                }
                var result = dao.Update(nuoc);
                if (result)
                {
                    SetAlert("Cập thông tin nước thành công", "success");
                    return RedirectToAction("Index", "QLNs");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thông tin nước không thành công");
                }
            }
            return View();
        }

        public ActionResult Delete(string MaNuoc)
        {
            new QLNsModel().Delete(MaNuoc);
            SetAlert("Bạn đã xóa thông tin nước ra khỏi phòng!", "success");
            return RedirectToAction("Index", "QLNs");
        }

    }
}