using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLib;
using XYJHZX_MVC.Lib;
using XYJHZX_MVC.Models;

namespace XYJHZX_MVC.Controllers
{
    public class StatisticsController : Controller
    {
        IDataCon _ICon = new DataCon();
        GetData _GetData = new GetData();
        string str_msg = "";
        // GET: Statistics
        public ActionResult MainRecord()
        {
            List<GroupModel> arr_Group = _GetData.GetGroupModel(out str_msg);
            SelectList arr_GroupList = new SelectList(arr_Group, "GroupId", "GroupName");
            ViewBag.GroupListDate = arr_GroupList;

            List<SplitDateModel> _splitDateModels = _GetData.GetSplitDateModels(out str_msg);
            SelectList arr_SplitDateList = new SelectList(_splitDateModels, "SplitId", "SplitName");
            ViewBag.SplitDateList = arr_SplitDateList;
            return View();
        }

        public ActionResult GetRecord(string FromSchedulDate, string ToSchedulDate, string GroupId = "2", string SchedulTime = "上午")
        {
            List<StatisticsModels> arr_StatisticsModels = new List<StatisticsModels>();
            ViewBag.CountData = arr_StatisticsModels;
            return PartialView("/Views/Statistics/GetRecord.cshtml");
        }
    }
}