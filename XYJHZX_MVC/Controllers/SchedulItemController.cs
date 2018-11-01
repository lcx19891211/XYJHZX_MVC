using XYJHZX_MVC.Lib;
using XYJHZX_MVC.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace XYJHZX_MVC.Controllers
{
    public class SchedulItemController : Controller
    {
        IDataCon _ICon = new DataCon();
        GetData _GetData = new GetData();
        string str_msg = "";

        // GET: SchedulItem
        public ActionResult Config()
        {
            return View();
        }

        public ActionResult Titles()
        {
            List<SchedulColumnMainModel> _schedulColumnMainModels = _GetData.GetSchedulColumnMain(out str_msg);
            ViewBag.MainData = _schedulColumnMainModels;
            List<SchedulColumnTypeModel> _schedulColumnTypeModels = _GetData.GetSchedulColumnType(out str_msg);
            _schedulColumnTypeModels.Insert(0, new SchedulColumnTypeModel());
            ViewBag.TypeData = _schedulColumnTypeModels;
            PartialViewResult x = PartialView("/Views/SchedulItem/Titles.cshtml");
            return x;
        }
        
        public ActionResult Items(string ColMainID = "1")
        {
            List<SchedulColumnDetailModel> _schedulColumnDetailModels = _GetData.GetSchedulColumnDetail(out str_msg, ColMainID);
            ViewBag.DetailData = _schedulColumnDetailModels;
            PartialViewResult x = PartialView("/Views/SchedulItem/Items.cshtml");
            return x;
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SaveTitles()
        {
            List<SchedulColumnMainModel> _schedulColumnMainModels = _GetData.GetSchedulColumnMain(out str_msg);
            ViewBag.MainData = _schedulColumnMainModels;
            List<SchedulColumnTypeModel> _schedulColumnTypeModels = _GetData.GetSchedulColumnType(out str_msg);
            _schedulColumnTypeModels.Insert(0, new SchedulColumnTypeModel());
            PartialViewResult x = PartialView("/Views/SchedulItem/Titles.cshtml");
            return x;
        }


        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SaveItems()
        {
            List<SchedulColumnDetailModel> _schedulColumnDetailModels = _GetData.GetSchedulColumnDetail(out str_msg, ColMainID);
            ViewBag.DetailData = _schedulColumnDetailModels;
            PartialViewResult x = PartialView("/Views/SchedulItem/Items.cshtml");
            return x;
        }
    }
}
}