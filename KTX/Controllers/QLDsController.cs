using KTX.Models;
using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KTX.Controllers
{
    public class QLDsController : BaseController
    {
        //
        // GET: /QLDs/
        public ActionResult Index(string searchString)
        {

            var dien = new QLDsModel();
            if (searchString == "")
            {
                SetAlert("Vui lòng nhập nội dung tìm kiếm", "error");
            }
            var model = dien.ListWhereAll(searchString);
            @ViewBag.SearchString = searchString;
            return View(model);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Create(DIEN dien)
        {
            if (ModelState.IsValid)
            {
                var dao = new QLDsModel();

                if (dao.Find(dien.MaDien) != null)
                {
                    SetAlert("Mã điện đã tồn tại", "error");
                    return RedirectToAction("Create", "QLDs");
                }

                String result = dao.Insert(dien);
                if (!String.IsNullOrEmpty(result))
                {
                    SetAlert("Thêm điện thành công", "success");
                    return RedirectToAction("Index", "QLDs");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm điện không thành công");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(string maDien)
        {
            var dien = new QLDsModel().getByMaDien(maDien);
            return View(dien);
        }
        [HttpPost]
        public ActionResult Edit(DIEN dien)
        {
            if (ModelState.IsValid)
            {
                var dao = new QLDsModel();

                var result = dao.Update(dien);
                if (result)
                {
                    SetAlert("Cập thông tin điện thành công", "success");
                    return RedirectToAction("Index", "QLDs");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thông tin điện không thành công");
                }
            }
            return View();
        }

        public ActionResult Delete(string MaDien)
        {
            new QLDsModel().Delete(MaDien);
            SetAlert("Bạn đã xóa thông tin điện ra khỏi phòng!", "success");
            return RedirectToAction("Index", "QLDs");
        }

    }
}