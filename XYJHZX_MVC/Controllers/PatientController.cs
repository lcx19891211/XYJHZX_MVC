using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.Mvc;
using XYJHZX_MVC.Lib;
using XYJHZX_MVC.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace XYJHZX_MVC.Controllers
{
    public class PatientController : Controller
    {
        IDataCon IPatCon = new DataCon();
        string str_msg = "";
        // GET: Patient
        public ActionResult PatientManage()
        {
            return View();
        }

        public ActionResult GetPatient()
        {
            DataSet _PatData = new DataSet();
            if (!IPatCon.ConInit(out str_msg) || !IPatCon.SelPatInformation(out str_msg, out _PatData, string.Empty))
            {
                ViewBag.Message = str_msg;
                ViewBag.PatientDate = null;
                return View();
            }
            else
            {
                var _PatTable = _PatData.Tables[0];
                var arr_PatModel = Convert<PatientModel>.ConvertToList(_PatTable);

                ViewBag.PatientDate = arr_PatModel.ToList<PatientModel>();

                return View();
            }
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult GetSelectPatient(string condition)
        {
            try
            {
                DataSet _PatData = new DataSet();
                if (!IPatCon.ConInit(out str_msg) || !IPatCon.SelPatInformation(out str_msg, out _PatData, condition))
                {
                    ViewBag.Message = str_msg;
                    ViewBag.PatientDate = null;
                    return View();
                }
                else
                {
                    var _PatTable = _PatData.Tables[0];
                    var arr_PatModel = Convert<PatientModel>.ConvertToList(_PatTable);

                    ViewBag.PatientDate = arr_PatModel.ToList<PatientModel>();
                    PartialViewResult x = PartialView("/Views/Patient/GetPatient.cshtml");
                    return x;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SetPatient(List<PatientModel> _patList)
        {
            DataSet _PatData = new DataSet();
            if(_patList!=null)
            {
                List<string> arr_dropid = new List<string>();
                List<string[]> arr2_updateValues = new List<string[]>();
                List<string> arr_updateid = new List<string>();
                List<string[]> arr2_insertValues = new List<string[]>();
                List<string[]> arr2_Status = new List<string[]>();
                arr2_Status.Add(new string[] { "0" });
                if (IPatCon.ConInit(out str_msg))
                {
                    foreach (PatientModel _patModel in _patList)
                    {
                        string[] arr_check = { _patModel.PatName, _patModel.PatIdCardNo };
                        switch (_patModel.IsRead)
                        {
                            case 1:
                                string[] arr_ValueTmp = {
                                        _patModel.PatName.ConvertSqlCondition()
                                        , _patModel.PatSex.ConvertSqlCondition()
                                        , _patModel.PatBrithday.ConvertSqlCondition()
                                        ,_patModel.PatAge.ToString()
                                        ,_patModel.PatIdCardNo.ConvertSqlCondition()
                                        ,_patModel.SendDeptId.ConvertSqlCondition()
                                        ,_patModel.SendDeptName.ConvertSqlCondition()
                                        ,_patModel.PatOutCardNo.ConvertSqlCondition()
                                        ,_patModel.TelphoneNo.ConvertSqlCondition()
                                        ,_patModel.PaymentDate.ConvertSqlCondition()
                                        ,_patModel.Remark.ConvertSqlCondition()
                                        };
                                if (string.IsNullOrEmpty(_patModel.PatId + "") || _patModel.PatId == 0)
                                {
                                    if (IPatCon.CheckPatIsRead(out str_msg, arr_check))
                                    {
                                        arr2_insertValues.Add(arr_ValueTmp);
                                    }
                                    else
                                        throw new Exception(str_msg);
                                }
                                else
                                {
                                    arr_updateid.Add(_patModel.PatId.ToString());
                                    arr2_updateValues.Add(arr_ValueTmp);
                                }
                                break;
                            case -1:
                                arr_dropid.Add(_patModel.PatId.ToString());
                                break;
                            default: break;
                        }
                    }
                }
                else
                    throw new Exception(str_msg);
                if (arr_dropid != null)
                    IPatCon.DropPatInformation(out str_msg, arr2_Status, arr_dropid.ToArray());
                if (arr2_insertValues != null)
                    IPatCon.InsertPatInformation(out str_msg, arr2_insertValues);
                if (arr_updateid != null)
                    IPatCon.UpdatePatInformation(out str_msg, arr2_updateValues, arr_updateid.ToArray());
            }
            return RedirectToRoute(new { controller = "Patient", action = "PatientManage" });
        }
        
    }
}