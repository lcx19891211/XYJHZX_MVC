using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using XYJHZX_MVC.Lib;
using XYJHZX_MVC.Models;

namespace XYJHZX_MVC.Controllers
{
    public class MacGroupController : Controller
    {
        IDataCon IMacGroupDataCon = new DataCon(); //创建新连接，可替换其他连接；

        public ActionResult Config()
        {
            string str_msg = "";
            string str_GroupIdDefault = "1";
            string str_TeamIdDefault = "1";
            DataSet _DataGroup = new DataSet();
            DataSet _DataTeam = new DataSet();
            if (RouteData.Values != null && RouteData.Values.Count > 3)
            {
                str_GroupIdDefault = RouteData.Values["groupid"] + "";
                str_TeamIdDefault = RouteData.Values["teamid"] + "";
            }

            if (!IMacGroupDataCon.ConInit(out str_msg) || !IMacGroupDataCon.SelGroup(out str_msg, ref _DataGroup)|| !IMacGroupDataCon.SelGroupTeam(out str_msg, ref _DataTeam, str_GroupIdDefault))
            {
                ViewBag.Message = str_msg;
                ViewBag.GroupDate = null;
                ViewBag.TeamDate = null;
                return View();
            }
            else
            {
                if (_DataGroup != null && _DataGroup.Tables.Count > 0 && _DataTeam != null && _DataTeam.Tables.Count > 0)
                {

                    List<SelectListItem> arr_GroupSelectitems = new List<SelectListItem>();
                    List<SelectListItem> arr_TeamSelectitems = new List<SelectListItem>();
                    int index = 0;
                    ViewBag.Message = str_msg;
                    DataTable _TableGroup = _DataGroup.Tables[0];
                    DataTable _TableTeam = _DataTeam.Tables[0];
                    if (_TableGroup.Rows.Count < 1 && str_GroupIdDefault != "0")
                        return RedirectToRoute(new { controller = "MacGroup", action = "Config", groupid = "0", teamid = "0" });
                    if (_TableTeam.Rows.Count < 1 && str_TeamIdDefault != "0")
                        return RedirectToRoute(new { controller = "MacGroup", action = "Config", groupid = str_GroupIdDefault, teamid = "0" });
                    for (int i = 0; i < _TableGroup.Rows.Count; i++)
                    {
                        SelectListItem _GroupRow = new SelectListItem();
                        _GroupRow.Value = _TableGroup.Rows[i]["Groupid"].ToString();
                        _GroupRow.Text = _TableGroup.Rows[i]["GroupName"] + "";
                        if (index == 0 || str_GroupIdDefault == _GroupRow.Value)
                        {
                            _GroupRow.Selected = true;
                            index++;
                        }
                        arr_GroupSelectitems.Add(_GroupRow);
                    }
                    for (int i = 0; i < _TableTeam.Rows.Count; i++)
                    {
                        SelectListItem _TeamRow = new SelectListItem();
                        _TeamRow.Value = _TableTeam.Rows[i]["Teamid"].ToString();
                        _TeamRow.Text = _TableTeam.Rows[i]["TeamName"] + "";
                        if (index == 0 || str_TeamIdDefault == _TeamRow.Value)
                        {
                            _TeamRow.Selected = true;
                            index++;
                        }
                        arr_TeamSelectitems.Add(_TeamRow);
                    }
                    ViewBag.GroupDate = arr_GroupSelectitems;
                    ViewBag.TeamDate = arr_TeamSelectitems;
                    return View();
                }
                else
                {
                    ViewBag.Message = "查无数据！";
                    ViewBag.GroupDate = null;
                    return View();
                }
            }
        }

        // GET: MacGroup
        public ActionResult GetGroup()
        {
            string str_msg = "";
            DataSet _DataGroup = new DataSet();

            if (!IMacGroupDataCon.ConInit(out str_msg) || !IMacGroupDataCon.SelGroup(out str_msg, ref _DataGroup))
            {
                ViewBag.Message = str_msg;
                ViewBag.GroupDate = null;
                return View();
            }
            else
            {
                if (_DataGroup != null && _DataGroup.Tables.Count > 0)
                {
                    List<GroupModel> arr_Group = new List<GroupModel>();
                    ViewBag.Message = str_msg;
                    DataTable _TableGroup = _DataGroup.Tables[0];
                    for (int i = 0;i< _TableGroup.Rows.Count;i++)
                    {
                        GroupModel _GroupRow = new GroupModel();
                        _GroupRow.Groupid = Int32.Parse(_TableGroup.Rows[i]["Groupid"].ToString());
                        _GroupRow.GroupName = _TableGroup.Rows[i]["GroupName"] + "";
                        _GroupRow.Desciption = _TableGroup.Rows[i]["Desciption"] + "";
                        _GroupRow.SeqID = _TableGroup.Rows[i]["SeqID"] + "";
                        arr_Group.Add(_GroupRow);
                    }
                    ViewBag.GroupDate = arr_Group;
                    return View();
                }
                else
                {
                    ViewBag.Message = "查无数据！";
                    ViewBag.GroupDate = null;
                    return View();
                }
            }
        }

        public ActionResult GetTeam()
        {
            string str_msg = "";
            DataSet _DataTeam = new DataSet();

            string str_GroupIdDefault = "1";
            if (RouteData.Values != null && RouteData.Values.Count > 3)
            {
                str_GroupIdDefault = RouteData.Values["groupid"] + "";
            }

            if (!IMacGroupDataCon.ConInit(out str_msg) || !IMacGroupDataCon.SelGroupTeam(out str_msg, ref _DataTeam, str_GroupIdDefault))
            {
                ViewBag.Message = str_msg;
                ViewBag.TeamDate = null;
                return View();
            }
            else
            {
                if (_DataTeam != null && _DataTeam.Tables.Count > 0)
                {
                    List<TeamModel> arr_Team = new List<TeamModel>();
                    ViewBag.Message = str_msg;
                    DataTable _TableTeam = _DataTeam.Tables[0];
                    for (int i = 0; i < _TableTeam.Rows.Count; i++)
                    {
                        TeamModel _TeamRow = new TeamModel();
                        _TeamRow.Teamid = Int32.Parse(_TableTeam.Rows[i]["Teamid"].ToString());
                        _TeamRow.TeamName = _TableTeam.Rows[i]["TeamName"] + "";
                        _TeamRow.Desciption = _TableTeam.Rows[i]["Desciption"] + "";
                        _TeamRow.SeqID = _TableTeam.Rows[i]["SeqID"] + "";
                        arr_Team.Add(_TeamRow);
                    }
                    ViewBag.TeamDate = arr_Team;
                    return View();
                }
                else
                {
                    ViewBag.Message = "查无数据！";
                    ViewBag.TeamDate = null;
                    return View();
                }
            }
        }

        public ActionResult GetMachine()
        {
            string str_msg = "";
            DataSet _DataMachine = new DataSet();

            string str_TeamIdDefault = "1";
            if (RouteData.Values != null && RouteData.Values.Count > 3)
            {
                str_TeamIdDefault = RouteData.Values["teamid"] + "";
            }
            if (!IMacGroupDataCon.ConInit(out str_msg) || !IMacGroupDataCon.SelTeamMachine(out str_msg, ref _DataMachine, str_TeamIdDefault))
            {
                ViewBag.Message = str_msg;
                ViewBag.MachineDate = null;
                return View();
            }
            else
            {
                if (_DataMachine != null && _DataMachine.Tables.Count > 0)
                {
                    List<MachineModel> arr_Machine = new List<MachineModel>();
                    ViewBag.Message = str_msg;
                    DataTable _TableMachine = _DataMachine.Tables[0];
                    for (int i = 0; i < _TableMachine.Rows.Count; i++)
                    {
                        MachineModel _MachineRow = new MachineModel();
                        _MachineRow.Macid = Int32.Parse(_TableMachine.Rows[i]["Macid"].ToString());
                        _MachineRow.MacName = _TableMachine.Rows[i]["MacName"] + "";
                        _MachineRow.Desciption = _TableMachine.Rows[i]["Desciption"] + "";
                        _MachineRow.SeqID = _TableMachine.Rows[i]["SeqID"] + "";
                        arr_Machine.Add(_MachineRow);
                    }
                    ViewBag.MachineDate = arr_Machine;
                    return View();
                }
                else
                {
                    ViewBag.Message = "查无数据！";
                    ViewBag.MachineDate = null;
                    return View();
                }
            }
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SetGroup(List<GroupModel> _groupList)
        {
            string str_msg = "";
            DataSet _DataGroup = new DataSet();
            
            if (!IMacGroupDataCon.ConInit(out str_msg))
            {
                ViewBag.Message = str_msg;
                ViewBag.GroupDate = null;
                return View("Config");
            }
            else
            {
                List<string> arr_dropid = new List<string>();
                List<string[]> arr2_updateValues = new List<string[]>();
                List<string> arr_updateid = new List<string>();
                List<string[]> arr2_insertValues = new List<string[]>();
                for (int i = 0; i < _groupList.Count; i++)
                {
                    switch (_groupList[i].Status)
                    {
                        case "-1":
                            arr_dropid.Add(_groupList[i].Groupid.ToString());
                            break;
                        case "0":
                            break;
                        case "1":
                            if (_groupList[i] == null || _groupList[i].Groupid.ToString() == "0")
                            {
                                string[] arr_insertTmp = { _groupList[i].GroupName.ConvertSqlCondition() , _groupList[i].Desciption.ConvertSqlCondition(), _groupList[i].SeqID };
                                arr2_insertValues.Add(arr_insertTmp);
                            }
                            else
                            {
                                string[] arr_updateTmp = { _groupList[i].GroupName.ConvertSqlCondition(), _groupList[i].Desciption.ConvertSqlCondition(), _groupList[i].SeqID };
                                arr_updateid.Add(_groupList[i].Groupid.ToString());
                                arr2_updateValues.Add(arr_updateTmp);
                            }
                            break;
                    }
                }

                if (arr_dropid != null)
                    IMacGroupDataCon.DropGroup(out str_msg, "0", arr_dropid.ToArray());
                if (arr_updateid != null)
                    IMacGroupDataCon.UpdateGroup(out str_msg, arr2_updateValues, arr_updateid.ToArray());
                if (arr2_insertValues != null)
                    IMacGroupDataCon.InsertGroup(out str_msg, arr2_insertValues);

                return RedirectToRoute(new { controller = "MacGroup", action = "Config" });
            }
        }


        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SetTeam(List<TeamModel> _teamList)
        {
            string str_msg = "";
            
            if (!IMacGroupDataCon.ConInit(out str_msg))
            {
                ViewBag.Message = str_msg;
                ViewBag.TeamDate = null;
                return View("Config");
            }
            else
            {
                List<string> arr_dropid = new List<string>();
                List<string[]> arr2_updateValues = new List<string[]>();
                List<string> arr_updateid = new List<string>();
                List<string[]> arr2_insertValues = new List<string[]>();
                for (int i = 0; i < _teamList.Count; i++)
                {
                    switch (_teamList[i].Status)
                    {
                        case "-1":
                            arr_dropid.Add(_teamList[i].Teamid.ToString());
                            break;
                        case "0":
                            break;
                        case "1":
                            if (_teamList[i] == null || _teamList[i].Teamid.ToString() == "0")
                            {
                                string[] arr_insertTmp = { _teamList[i].TeamName.ConvertSqlCondition() , _teamList[i].Desciption.ConvertSqlCondition(), _teamList[i].SeqID };
                                arr2_insertValues.Add(arr_insertTmp);
                            }
                            else
                            {
                                string[] arr_updateTmp = { _teamList[i].TeamName.ConvertSqlCondition(), _teamList[i].Desciption.ConvertSqlCondition(), _teamList[i].SeqID };
                                arr_updateid.Add(_teamList[i].Teamid.ToString());
                                arr2_updateValues.Add(arr_updateTmp);
                            }
                            break;
                    }
                }

                if (arr_dropid != null)
                    IMacGroupDataCon.DropTeam(out str_msg, "0", arr_dropid.ToArray());
                if (arr_dropid != null)
                    IMacGroupDataCon.DeleteTeamWithGroup(out str_msg, arr_dropid.ToArray());
                if (arr_updateid != null)
                    IMacGroupDataCon.UpdateTeam(out str_msg, arr2_updateValues, arr_updateid.ToArray());
                List<string> arr_keyid = new List<string>();
                IMacGroupDataCon.InsertTeam(out str_msg, arr2_insertValues,ref arr_keyid);
                List<string[]> arr2_TeamWithGroup = new List<string[]>();

                foreach(string str_keyid in arr_keyid)
                {
                    string[] str_TeamWithGroup = { str_keyid, _teamList[0].Groupid.ToString() };
                    arr2_TeamWithGroup.Add(str_TeamWithGroup);
                }
                IMacGroupDataCon.InsertTeamWithGroup(out str_msg, arr2_TeamWithGroup);

                return RedirectToRoute(new { controller = "MacGroup", action = "Config" });
            }
        }


        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SetMachine(List<MachineModel> _machineList)
        {
            string str_msg = "";
            if (!IMacGroupDataCon.ConInit(out str_msg))
            {
                ViewBag.Message = str_msg;
                ViewBag.MachineDate = null;
                return View("Config");
            }
            else
            {
                List<string> arr_dropid = new List<string>();
                List<string[]> arr2_updateValues = new List<string[]>();
                List<string> arr_updateid = new List<string>();
                List<string[]> arr2_insertValues = new List<string[]>();
                for (int i = 0; i < _machineList.Count; i++)
                {
                    switch (_machineList[i].Status)
                    {
                        case "-1":
                            arr_dropid.Add(_machineList[i].Macid.ToString());
                            break;
                        case "0":
                            break;
                        case "1":
                            if (_machineList[i] == null || _machineList[i].Macid.ToString() == "0")
                            {
                                string[] arr_insertTmp = { _machineList[i].MacName.ConvertSqlCondition(), _machineList[i].Desciption.ConvertSqlCondition(), _machineList[i].SeqID };
                                arr2_insertValues.Add(arr_insertTmp);
                            }
                            else
                            {
                                string[] arr_updateTmp = { _machineList[i].MacName.ConvertSqlCondition(), _machineList[i].Desciption.ConvertSqlCondition(), _machineList[i].SeqID };
                                arr_updateid.Add(_machineList[i].Macid.ToString());
                                arr2_updateValues.Add(arr_updateTmp);
                            }
                            break;
                    }
                }

                if (arr_dropid != null)
                    IMacGroupDataCon.DropMachine(out str_msg, "0", arr_dropid.ToArray());
                if (arr_dropid != null)
                    IMacGroupDataCon.DeleteMachineWithTeam(out str_msg, arr_dropid.ToArray());
                if (arr_updateid != null)
                    IMacGroupDataCon.UpdateMachine(out str_msg, arr2_updateValues, arr_updateid.ToArray());

                List<string> arr_keyid = new List<string>();
                IMacGroupDataCon.InsertMachine(out str_msg, arr2_insertValues, ref arr_keyid);
                List<string[]> arr2_MachineWithTeam = new List<string[]>();

                foreach (string str_keyid in arr_keyid)
                {
                    string[] str_MachineWithTeam = { str_keyid, _machineList[0].Teamid.ToString() };
                    arr2_MachineWithTeam.Add(str_MachineWithTeam);
                }
                IMacGroupDataCon.InsertMashineWithTeam(out str_msg, arr2_MachineWithTeam);

                return RedirectToRoute(new { controller = "MacGroup", action = "Config" });
            }
        }


        /// <summary>
        /// 限制仅允许Ajax访问
        /// </summary>
        [AttributeUsage(AttributeTargets.Method)]
        public class HandlerAjaxOnlyAttribute : ActionMethodSelectorAttribute
        {
            public bool Ignore { get; set; }
            public HandlerAjaxOnlyAttribute(bool ignore = false)
            {
                Ignore = ignore;
            }
            public override bool IsValidForRequest(ControllerContext controllerContext,
              System.Reflection.MethodInfo methodInfo)
            {
                if (Ignore)
                    return true;
                return controllerContext.RequestContext.HttpContext.Request.IsAjaxRequest();
            }
        }
    }
}