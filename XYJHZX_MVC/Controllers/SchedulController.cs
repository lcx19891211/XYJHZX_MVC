using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.Mvc;
using XYJHZX_MVC.Lib;
using XYJHZX_MVC.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace XYJHZX_MVC.Models
{
    public class SchedulController : Controller
    {
        IDataCon _ISchedulCon = new DataCon();
        GetData _GetData = new GetData();
        string str_msg = "";
        // GET: Schedul
        public ActionResult SchedulConfig()
        {
            return View();
        }

        public ActionResult GetNewSchedul()
        {
            List<GroupModel> arr_Group = _GetData.GetGroupModel(out str_msg);
            SelectList arr_GroupList = new SelectList(arr_Group, "GroupId", "GroupName");
            ViewBag.GroupListDate = arr_GroupList;

            List<PatientModel> arr_Patient = _GetData.GetPatientModels(out str_msg);
            PatientModel _patientModelEmpty = new PatientModel();
            arr_Patient.Insert(0, _patientModelEmpty);
            //SelectList arr_PatList = new SelectList(arr_Patient, "PatId", "PatName");
            ViewBag.PatientListDate = arr_Patient;

            ViewBag.SchedulDate = _GetData.SchedulInitTable(out str_msg, "1");
            return View();
        }

        public ActionResult GetOldSchedul()
        {
            ViewBag.SchedulDate = _GetData.SchedulInitTable(out str_msg, "1");
            return View();
        }

        public ActionResult GetSelectNewSchedul()
        {
            return View();
        }
        public ActionResult GetSelectOldSchedul()
        {
            return View();
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SetSchedulMain(List<SchedulMainModel> arr_schedulList)
        {
            try
            {
                if (arr_schedulList != null)
                {
                    List<string[]> arr2_updateValues = new List<string[]>();
                    List<string> arr_updateid = new List<string>();
                    List<string[]> arr2_insertValues = new List<string[]>();
                    foreach (SchedulMainModel _SchedulMainModel in arr_schedulList)
                    {
                        if (_SchedulMainModel.status == 1)
                        {
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
                                    _SchedulMainModel.anticoagulantName.ToString(),
                                    _SchedulMainModel.remark.ConvertSqlCondition(),
                                    DateTime.Now.ToLongDateString()
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
                    //_ISchedulCon.UpdateSchedulMain(out str_msg, arr2_updateValues, arr_updateid.ToArray());
                    //_ISchedulCon.InsertSchedulMain(out str_msg, arr2_insertValues);
                }
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