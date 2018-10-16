using System;
using System.Collections.Generic;
using System.Globalization;
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

            List<SchedulColumns> arr_schedulColumns = _GetData.GetSchedul2Columns(out str_msg);
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

                    List<SchedulColumns> arr_schedulColumns = _GetData.GetSchedul2Columns(out str_msg);
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

                List<SchedulColumns> arr_schedulColumns = _GetData.GetSchedul2Columns(out str_msg);
                ViewBag.SchedulColumnsDate = arr_schedulColumns;
                
                PartialViewResult _partialViewResult = PartialView("/Views/Schedul/GetNewSchedul.cshtml");
                return _partialViewResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}