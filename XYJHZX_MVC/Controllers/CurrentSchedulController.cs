using System.Collections.Generic;
using System.Web.Mvc;
using XYJHZX_MVC.Lib;
using XYJHZX_MVC.Models;

namespace XYJHZX_MVC.Controllers
{
    public class CurrentSchedulController : Controller
    {
        IDataCon _ICon = new OraCon();
        GetData _GetData = new GetData();
        string str_msg = "";

        public ActionResult CurrentManage()
        {
            List<TeamModel> _teamModels = _GetData.GetTeamModel(out str_msg);
            List<GroupModel> _groupModels = _GetData.GetGroupModel(out str_msg);
            _teamModels.Insert(0, null);
            _groupModels.Insert(0, null);
            SelectList arr_GroupSelectitems = new SelectList(_groupModels, "Groupid", "Desciption");
            SelectList arr_TeamSelectitems = new SelectList(_teamModels, "teamid", "Desciption");

            ViewBag.GroupDate = arr_GroupSelectitems;
            ViewBag.TeamDate = arr_TeamSelectitems;

            return View();
        }

        // GET: CurrentSchedul
        public ActionResult ViewForTeam()
        {
            string str_GroupIdDefault = "1";
            string str_TeamIdDefault = null;
            if (RouteData.Values != null && RouteData.Values.Count > 3)
            {
                str_GroupIdDefault = RouteData.Values["groupid"] + "";
                str_TeamIdDefault = RouteData.Values["teamid"] + "";
            }
            if (str_TeamIdDefault == "x")
                str_TeamIdDefault = null;
            List<CurrentSchedulModel> _currentSchedulModels = _GetData.GetCurrentSchedulModels(out str_msg, str_GroupIdDefault, str_TeamIdDefault);
            if (_currentSchedulModels.Count > 0)
            {
                if (str_TeamIdDefault == null)
                    ViewBag.Message = _currentSchedulModels[0].GroupDescption;
                else
                    ViewBag.Message = _currentSchedulModels[0].teamDescption;
            }
            ViewBag.CurrentData = _currentSchedulModels;
            return View();
        }
    }
}