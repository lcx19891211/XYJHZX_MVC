using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using XYJHZX_MVC.Lib;

namespace XYJHZX_MVC.Models
{
    public class SchedulController : Controller
    {
        IDataCon _ISchedulCon = new DataCon();
        GetData _GetData = new GetData();
        string str_msg = "";

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public ActionResult SchedulConfig()
        {
            List<GroupModel> arr_Group = _GetData.GetGroupModel(out str_msg);
            SelectList arr_GroupList = new SelectList(arr_Group, "GroupId", "GroupName");
            ViewBag.GroupListDate = arr_GroupList;
            
            List<SplitDateModel> _splitDateModels = _GetData.GetSplitDateModels(out str_msg);
            SelectList arr_SplitDateList = new SelectList(_splitDateModels, "SplitId", "SplitName");
            ViewBag.SplitDateList = arr_SplitDateList;
            return View();
        }

        /// <summary>
        /// 返回检查浏览
        /// </summary>
        /// <returns></returns>
        public ActionResult SchedulExplorer()
        {
            List<GroupModel> arr_Group = _GetData.GetGroupModel(out str_msg);
            SelectList arr_GroupList = new SelectList(arr_Group, "GroupId", "GroupName");
            ViewBag.GroupListDate = arr_GroupList;

            List<SplitDateModel> _splitDateModels = _GetData.GetSplitDateModels(out str_msg);
            SelectList arr_SplitDateList = new SelectList(_splitDateModels, "SplitId", "SplitName");
            ViewBag.SplitDateList = arr_SplitDateList;

            return View();
        }
        /// <summary>
        /// 返回签到重置确认表
        /// </summary>
        /// <returns></returns>
        public ActionResult SchedulSignInRedo()
        {
            List<GroupModel> arr_Group = _GetData.GetGroupModel(out str_msg);
            SelectList arr_GroupList = new SelectList(arr_Group, "GroupId", "GroupName");
            ViewBag.GroupListDate = arr_GroupList;

            List<SplitDateModel> _splitDateModels = _GetData.GetSplitDateModels(out str_msg);
            SelectList arr_SplitDateList = new SelectList(_splitDateModels, "SplitId", "SplitName");
            ViewBag.SplitDateList = arr_SplitDateList;
            return View();
        }

        /// <summary>
        /// 返回打印格式
        /// </summary>
        /// <param name="SchedulDate"></param>
        /// <param name="GroupId"></param>
        /// <param name="SchedulTime"></param>
        /// <returns></returns>
        public ActionResult SchedulPrintView(string SchedulDate, string GroupId = "2", string SchedulTime = "上午")
        {
            bool isFirst = false;
            if (string.IsNullOrEmpty(SchedulDate))
            {
                isFirst = true;
                SchedulDate = DateTime.Now.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            }
            List<List<SchedulPrint>> arr2_schedulPrints = new List<List<SchedulPrint>>();
            List<SchedulPrint> arr_schedulPrints = _GetData.GetSchedulPrint(out str_msg, GroupId, SchedulDate, SchedulTime);
            string str_teamName = "";
            List<SchedulPrint> tmp_schedulPrints = new List<SchedulPrint>();
            foreach (SchedulPrint sp in arr_schedulPrints)
            {
                sp.SchedulDate = SchedulDate;
                sp.SchedulTime = SchedulTime;
                if (str_teamName != sp.TeamName)
                {
                    if (str_teamName != "")
                    {
                        arr2_schedulPrints.Add(tmp_schedulPrints);
                        tmp_schedulPrints = new List<SchedulPrint>();
                    }
                    tmp_schedulPrints.Add(sp);
                    str_teamName = sp.TeamName;
                }
                else
                    tmp_schedulPrints.Add(sp);
            }
            arr2_schedulPrints.Add(tmp_schedulPrints);
            tmp_schedulPrints = new List<SchedulPrint>();
            ViewBag.SchedulDate = arr2_schedulPrints;
            if (isFirst)
                return View();
            else
                return PartialView("/Views/Schedul/SchedulPrintView.cshtml");
        }

        /// <summary>
        /// 返回打印体重视图
        /// </summary>
        /// <param name="SchedulDate"></param>
        /// <param name="GroupId"></param>
        /// <param name="SchedulTime"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SchedulWeightPrint(string SchedulDate, string GroupId = "2", string SchedulTime = "上午")
        {
            if (string.IsNullOrEmpty(SchedulDate))
            {
                SchedulDate = DateTime.Now.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            }

            List<List<SchedulPrint>> arr2_schedulPrints = new List<List<SchedulPrint>>();
            List<SchedulPrint> arr_schedulPrints = _GetData.GetSchedulPrint(out str_msg, GroupId, SchedulDate, SchedulTime);
            string str_teamName = "";
            List<SchedulPrint> tmp_schedulPrints = new List<SchedulPrint>();
            foreach (SchedulPrint sp in arr_schedulPrints)
            {
                sp.SchedulDate = SchedulDate;
                sp.SchedulTime = SchedulTime;
                if (str_teamName != sp.TeamName)
                {
                    if (str_teamName != "")
                    {
                        arr2_schedulPrints.Add(tmp_schedulPrints);
                        tmp_schedulPrints = new List<SchedulPrint>();
                    }
                    tmp_schedulPrints.Add(sp);
                    str_teamName = sp.TeamName;
                }
                else
                    tmp_schedulPrints.Add(sp);
            }
            arr2_schedulPrints.Add(tmp_schedulPrints);
            tmp_schedulPrints = new List<SchedulPrint>();
            ViewBag.SchedulDate = arr2_schedulPrints;
            return PartialView("/Views/Schedul/SchedulPrintWeight.cshtml");
        }
        /// <summary>
        /// 签到界面
        /// </summary>
        /// <returns></returns>
        public ActionResult SchedulSignIn()
        {
            string GroupId = RouteData.Values["groupid"] + "";
            if (string.IsNullOrEmpty(GroupId))
                GroupId = "2";
            ViewBag.GroupId = GroupId;

            return View();
        }

        /// <summary>
        /// 签到显示分部视图
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public ActionResult SchedulSignInView(string GroupId)
        {
            string SchedulDate = DateTime.Now.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            string dt_SchedulDate = DateTime.Now.ToString("HH:mm:dd", DateTimeFormatInfo.InvariantInfo);
            string SchedulTime = _GetData.GetDateSplit(out str_msg, dt_SchedulDate);
            List<List<SchedulPrint>> arr2_schedulPrints = new List<List<SchedulPrint>>();
            List<SchedulPrint> arr_schedulPrints = _GetData.GetSchedulPrint(out str_msg, GroupId, SchedulDate, SchedulTime);
            string str_teamName = "";
            List<SchedulPrint> tmp_schedulPrints = new List<SchedulPrint>();
            foreach (SchedulPrint sp in arr_schedulPrints)
            {
                if (str_teamName != sp.TeamName)
                {
                    if (str_teamName != "")
                    {
                        arr2_schedulPrints.Add(tmp_schedulPrints);
                        tmp_schedulPrints = new List<SchedulPrint>();
                    }
                    tmp_schedulPrints.Add(sp);
                    str_teamName = sp.TeamName;
                }
                else
                    tmp_schedulPrints.Add(sp);
            }
            arr2_schedulPrints.Add(tmp_schedulPrints);
            tmp_schedulPrints = new List<SchedulPrint>();
            ViewBag.SchedulDate = arr2_schedulPrints;
            return PartialView("/Views/Schedul/SchedulSignInView.cshtml");
        }

        /// <summary>
        /// 获取签到初始表
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public ActionResult SchedulSignInDetail( string SchedulDate, string SchedulTime, int GroupId = 1)
        {
            ViewBag.SchedulDate = _GetData.GetSchedulPrint(out str_msg, GroupId.ToString(), SchedulDate, SchedulTime);
            return PartialView("/Views/Schedul/SchedulSignInDetail.cshtml");
        }
        /// <summary>
        /// 根据身份证号签到
        /// </summary>
        /// <param name="IDCard"></param>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SignInIDCard(string IDCard, string GroupId)
        {
            if (string.IsNullOrEmpty(IDCard))
                throw  new Exception("卡号为空或卡号读取失败！");
            else
            {
                DataSet _DataResult = new DataSet();
                if (_ISchedulCon.ConInit(out str_msg) && _ISchedulCon.SelMainIDCurrentSchedulForIDCard(out str_msg, out _DataResult, new string[] { IDCard.ToUpper().Trim(), GroupId }) && _DataResult != null && _DataResult.Tables[0].Rows.Count > 0)
                {
                    string str_mainID = _DataResult.Tables[0].Rows[0][0] + "";
                    string str_teamid = _DataResult.Tables[0].Rows[0][1] + "";
                    string[] arr_mainID = { str_mainID };
                    int int_maxseq = 0;
                    _DataResult = new DataSet();
                    int_maxseq = _GetData.GetMaxCurrenSeq(out str_msg, str_teamid);
                    string SchedulDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
                    List<string[]> arr2_value = new List<string[]> { new string[] { SchedulDateTime.ConvertSqlCondition(), int_maxseq.ToString() } };
                    if (_ISchedulCon.UpdateSchedulSigninDate(out str_msg, arr2_value, arr_mainID))
                    {
                    }

                    string SchedulDate = DateTime.Now.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
                    string dt_SchedulDate = DateTime.Now.ToString("HH:mm:dd", DateTimeFormatInfo.InvariantInfo);
                    string SchedulTime = _GetData.GetDateSplit(out str_msg, dt_SchedulDate);
                    List<List<SchedulPrint>> arr2_schedulPrints = new List<List<SchedulPrint>>();
                    List<SchedulPrint> arr_schedulPrints = _GetData.GetSchedulPrint(out str_msg, GroupId, SchedulDate, SchedulTime);
                    string str_teamName = "";
                    List<SchedulPrint> tmp_schedulPrints = new List<SchedulPrint>();
                    foreach (SchedulPrint sp in arr_schedulPrints)
                    {
                        if (str_teamName != sp.TeamName)
                        {
                            if (str_teamName != "")
                            {
                                arr2_schedulPrints.Add(tmp_schedulPrints);
                                tmp_schedulPrints = new List<SchedulPrint>();
                            }
                            tmp_schedulPrints.Add(sp);
                            str_teamName = sp.TeamName;
                        }
                        else
                            tmp_schedulPrints.Add(sp);
                    }
                    arr2_schedulPrints.Add(tmp_schedulPrints);
                    tmp_schedulPrints = new List<SchedulPrint>();
                    ViewBag.SchedulDate = arr2_schedulPrints;
                    return PartialView("/Views/Schedul/SchedulSignInView.cshtml");
                }
                else
                {
                    throw new Exception("签到失败，找不到未签到记录！");
                }
            }
        }

        /// <summary>
        /// 初始化新检查表
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public ActionResult GetNewSchedul(int GroupId = 1)
        {
            List<PatientModel> arr_Patient = _GetData.GetPatientModels(out str_msg);
            PatientModel _patientModelEmpty = new PatientModel();
            arr_Patient.Insert(0, _patientModelEmpty);
            ViewBag.PatientListDate = arr_Patient;

            List<SchedulColumns> arr_schedulColumns = _GetData.GetSchedulColumns(out str_msg, 2);
            ViewBag.SchedulColumnsDate = arr_schedulColumns;
            
            ViewBag.SchedulDate = _GetData.SchedulInitTable(out str_msg, GroupId.ToString(), DateTime.Now.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo),"上午");
            return View();
        }

        /// <summary>
        /// 初始化旧检查表
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public ActionResult GetOldSchedul(int GroupId = 1)
        {
            ViewBag.SchedulDate = _GetData.SchedulInitTable(out str_msg, GroupId.ToString(), DateTime.Now.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo), "上午");
            return View();
        }

        /// <summary>
        /// 查询新检查表
        /// </summary>
        /// <param name="GroupId"></param>
        /// <param name="SchedulDate"></param>
        /// <param name="SchedulTime"></param>
        /// <returns></returns>
        public ActionResult GetSelectNewSchedul(string GroupId, string SchedulDate, string SchedulTime)
        {
            try
            {
                if (!_ISchedulCon.ConInit(out str_msg))
                {
                    ViewBag.Message = str_msg;
                    ViewBag.PatientDate = null;
                    return View();
                }
                else
                {
                    ViewBag.SchedulDate = _GetData.SchedulInitTable(out str_msg, GroupId, SchedulDate, SchedulTime);
                    List<PatientModel> arr_Patient = _GetData.GetPatientModels(out str_msg);
                    PatientModel _patientModelEmpty = new PatientModel();
                    arr_Patient.Insert(0, _patientModelEmpty);
                    ViewBag.PatientListDate = arr_Patient;

                    List<SchedulColumns> arr_schedulColumns = _GetData.GetSchedulColumns(out str_msg, 2);
                    ViewBag.SchedulColumnsDate = arr_schedulColumns;
                    
                    PartialViewResult x = PartialView("/Views/Schedul/GetNewSchedul.cshtml");
                    return x;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 查询旧检查表
        /// </summary>
        /// <param name="GroupId"></param>
        /// <param name="SchedulDate"></param>
        /// <param name="SchedulTime"></param>
        /// <returns></returns>
        public ActionResult GetSelectOldSchedul(string GroupId, string SchedulDate, string SchedulTime)
        {
            try
            {
                if (!_ISchedulCon.ConInit(out str_msg))
                {
                    ViewBag.Message = str_msg;
                    ViewBag.PatientDate = null;
                    return View();
                }
                else
                {
                    ViewBag.SchedulDate = _GetData.SchedulInitTable(out str_msg, GroupId, SchedulDate, SchedulTime);
                    PartialViewResult x = PartialView("/Views/Schedul/GetOldSchedul.cshtml");
                    return x;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 保存新检查表
        /// </summary>
        /// <param name="arr_SchedulList"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SetSchedulMain(List<SchedulMainModel> arr_SchedulList)
        {
            try
            {
                string str_rq = HttpContext.Request.Params.ToString();
                if (arr_SchedulList != null && _ISchedulCon.ConInit(out str_msg))
                {
                    List<string[]> arr2_updateValues = new List<string[]>();
                    List<string> arr_updateid = new List<string>();
                    List<string[]> arr2_insertValues = new List<string[]>();
                    List<string> arr_patId = new List<string>();
                    foreach (SchedulMainModel _SchedulMainModel in arr_SchedulList)
                    {
                        if (_SchedulMainModel.status == 1)
                        {
                            arr_patId.Add(_SchedulMainModel.patid.ToString());
                            string[] arr_updateValues =
                            {
                                    _SchedulMainModel.SchedulDate.ConvertSqlCondition(),
                                    _SchedulMainModel.SchedulTime.ConvertSqlCondition(),
                                    _SchedulMainModel.patid.ToString(),
                                    _SchedulMainModel.teamid.ToString(),
                                    _SchedulMainModel.groupid.ToString(),
                                    _SchedulMainModel.macid.ToString(),
                                    _SchedulMainModel.dialyzerName.ConvertSqlCondition(),
                                    _SchedulMainModel.routeName.ConvertSqlCondition(),
                                    _SchedulMainModel.anticoagulantName.ConvertSqlCondition(),
                                    _SchedulMainModel.remark.ConvertSqlCondition(),
                                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss",DateTimeFormatInfo.InvariantInfo).ConvertSqlCondition()
                            };
                            if (_SchedulMainModel.mainid == 0)
                            {
                                arr2_insertValues.Add(arr_updateValues);
                            }
                            else
                            {
                                arr_updateid.Add(_SchedulMainModel.mainid.ToString());
                                arr2_updateValues.Add(arr_updateValues);
                            }
                        }
                    }
                    _ISchedulCon.UpdateSchedulMain(out str_msg, arr2_updateValues, arr_updateid.ToArray());
                    _ISchedulCon.InsertSchedulMain(out str_msg, arr2_insertValues);
                    _ISchedulCon.UpadatePatIsRead(out str_msg, arr_patId.ToArray());
                }

                ViewBag.SchedulDate = _GetData.SchedulInitTable(out str_msg, arr_SchedulList[0].groupid.ToString(), arr_SchedulList[0].SchedulDate, arr_SchedulList[0].SchedulTime);
                List<PatientModel> arr_Patient = _GetData.GetPatientModels(out str_msg);
                PatientModel _patientModelEmpty = new PatientModel();
                arr_Patient.Insert(0, _patientModelEmpty);
                ViewBag.PatientListDate = arr_Patient;

                List<SchedulColumns> arr_schedulColumns = _GetData.GetSchedulColumns(out str_msg, 2);
                ViewBag.SchedulColumnsDate = arr_schedulColumns;
                
                PartialViewResult _partialViewResult = PartialView("/Views/Schedul/GetNewSchedul.cshtml");
                return _partialViewResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// 重置签到病人
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SchedulRedo(string MainId)
        {
            List<SchedulPrint> SchedulData = new List<SchedulPrint>();
            DataSet _SchedulData = new DataSet();
            if (_ISchedulCon.ConInit(out str_msg) && _ISchedulCon.SelSchedulWithMainId(out str_msg, ref _SchedulData, MainId))
            {
                DataTable _SchedulTable = _SchedulData.Tables[0];
                string str_GroupId = _SchedulTable.Rows[0]["Groupid"] + "";
                string str_SchedulDate = _SchedulTable.Rows[0]["SchedulDate"] + "";
                string str_SchedulTime = _SchedulTable.Rows[0]["SchedulTime"] + "";

                List<string[]> arr2_value = new List<string[]> { new string[] { ("").ConvertSqlCondition(), ("").ConvertSqlCondition() } };
                if (_ISchedulCon.UpdateSchedulSigninDate(out str_msg, arr2_value, new string[]{ MainId }))
                {
                    SchedulData = _GetData.GetSchedulPrint(out str_msg, str_GroupId, str_SchedulDate, str_SchedulTime);
                }
            }
            ViewBag.SchedulDate = SchedulData;
            return PartialView("/Views/Schedul/SchedulSignInDetail.cshtml");
        }
        /// <summary>
        /// 手工签到病人
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SchedulSignIn(string MainId)
        {
            List<SchedulPrint> SchedulData = new List<SchedulPrint>();
            DataSet _SchedulData = new DataSet();
            if (_ISchedulCon.ConInit(out str_msg) && _ISchedulCon.SelSchedulWithMainId(out str_msg, ref _SchedulData, MainId))
            {
                DataTable _SchedulTable = _SchedulData.Tables[0];
                string str_GroupId = _SchedulTable.Rows[0]["Groupid"] + "";
                string str_SchedulDate = _SchedulTable.Rows[0]["SchedulDate"] + "";
                string str_SchedulTime = _SchedulTable.Rows[0]["SchedulTime"] + "";
                string str_Teamid = _SchedulTable.Rows[0]["Teamid"] + "";
                _SchedulData = new DataSet();
                _ISchedulCon.SelMaxSchedulSeq(out str_msg,out _SchedulData, new string[] { str_Teamid, str_SchedulDate, str_SchedulTime });
                int int_maxseq = Int32.Parse("0" + _SchedulData.Tables[0].Rows[0][0]);
                string SchedulDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
                List<string[]> arr2_value = new List<string[]> { new string[] { SchedulDateTime.ConvertSqlCondition(), (int_maxseq + 1).ToString() } };
                if (_ISchedulCon.UpdateSchedulSigninDate(out str_msg, arr2_value, new string[] { MainId }))
                {
                    SchedulData = _GetData.GetSchedulPrint(out str_msg, str_GroupId, str_SchedulDate, str_SchedulTime);
                }
            }
            ViewBag.SchedulDate = SchedulData;
            return PartialView("/Views/Schedul/SchedulSignInDetail.cshtml");
        }

    }
}