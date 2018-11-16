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
        IDataCon IPatOraCon = new OraCon();
        string str_msg = "";
        // GET: Patient
        public ActionResult PatientManage()
        {
            return View();
        }
        /// <summary>
        /// 测试用
        /// </summary>
        /// <returns></returns>
        public ActionResult TestView()
        {
            IPatCon.ConInit(out str_msg);
            DataSet ds = new DataSet();
            IPatCon.SelDateTimeSplit(out str_msg, out ds);
            ViewBag.Message = str_msg;
            return View();
        }
        /// <summary>
        /// 获取已登记病人信息
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 导入已缴费病人信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult GetPayPateint()
        {
            try
            {
                DataSet _PatData = new DataSet();
                if (!IPatCon.ConInit(out str_msg))
                {
                    ViewBag.Message = str_msg;
                    ViewBag.PatientDate = null;
                    return View();
                }
                else
                {
                    if (IPatOraCon.ConInit(out str_msg))
                    {
                        DataSet _dataSet = new DataSet();
                        if (IPatOraCon.SelPayPatient(out str_msg, out _dataSet))
                        {
                            List<string[]> arr2_insertValues = new List<string[]>();
                            DataTable _dataTable = _dataSet.Tables[0];
                            for (int i = 0; i < _dataTable.Rows.Count; i++)
                            {
                                PatientModel _patientModel = new PatientModel();
                                _patientModel.PatName = _dataTable.Rows[i]["病人姓名"] + "";
                                _patientModel.PatIdCardNo = _dataTable.Rows[i]["身份证"] + "";
                                _patientModel.PatOutCardNo = _dataTable.Rows[i]["卡号"] + "";
                                _patientModel.PatSex = _dataTable.Rows[i]["性别"] + "";
                                _patientModel.SendDeptName = _dataTable.Rows[i]["开方科室"] + "";
                                _patientModel.TelphoneNo = _dataTable.Rows[i]["电话"] + "";
                                string str_paymentDate = _dataTable.Rows[i]["日期"] + "";
                                string str_Brithday = _dataTable.Rows[i]["出生日期"] + "";
                                DateTime _briDateTime = DateTime.Parse(str_Brithday);
                                DateTime _payDateTime = DateTime.Parse(str_paymentDate);
                                _patientModel.PaymentDate = string.IsNullOrEmpty(str_paymentDate) ? string.Empty : string.Format("{0:yyyy-MM-dd}", _payDateTime);
                                _patientModel.PatBrithday = string.IsNullOrEmpty(str_Brithday) ? string.Empty : string.Format("{0:yyyy-MM-dd}", _briDateTime);
                                _patientModel.PatAge = Convert.ToInt32(((_payDateTime - _briDateTime).TotalDays / 365));
                                _patientModel.IsRead = 1;
                                string[] arr_check = { _patientModel.PatName, _patientModel.PatIdCardNo };
                                string[] arr_ValueTmp = {
                                        _patientModel.PatName.ConvertSqlCondition()
                                        , _patientModel.PatSex.ConvertSqlCondition()
                                        , _patientModel.PatBrithday.ConvertSqlCondition()
                                        ,_patientModel.PatAge.ToString()
                                        ,_patientModel.PatIdCardNo.ConvertSqlCondition()
                                        ,_patientModel.SendDeptId.ConvertSqlCondition()
                                        ,_patientModel.SendDeptName.ConvertSqlCondition()
                                        ,_patientModel.PatOutCardNo.ToUpper().ConvertSqlCondition()
                                        ,_patientModel.TelphoneNo.ConvertSqlCondition()
                                        ,_patientModel.PaymentDate.ConvertSqlCondition()
                                        ,_patientModel.Remark.ConvertSqlCondition()
                                        };
                                if (IPatCon.CheckPatIsRead(out str_msg, arr_check))
                                {
                                    arr2_insertValues.Add(arr_ValueTmp);
                                }
                            }
                            if (arr2_insertValues != null)
                                IPatCon.InsertPatInformation(out str_msg, arr2_insertValues);
                            if (IPatCon.SelPatInformation(out str_msg, out _PatData, string.Empty))
                            {
                                var _PatTable = _PatData.Tables[0];
                                var arr_PatModel = Convert<PatientModel>.ConvertToList(_PatTable);
                                ViewBag.PatientDate = arr_PatModel.ToList<PatientModel>();
                            }
                            PartialViewResult x = PartialView("/Views/Patient/GetPatient.cshtml");
                            return x;
                        }
                        else
                            throw new Exception(str_msg);
                    }
                    else
                    {
                        throw new Exception(str_msg);
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取指定条件已登记病人
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 保存病人信息
        /// </summary>
        /// <param name="_patList"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SetPatient(List<PatientModel> _patList)
        {
            DataSet _PatData = new DataSet();
            if (_patList != null)
            {
                List<string> arr_dropid = new List<string>();
                List<string[]> arr2_updateValues = new List<string[]>();
                List<string> arr_updateid = new List<string>();
                List<string[]> arr2_insertValues = new List<string[]>();
                List<string[]> arr2_Status = new List<string[]>();
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
                                arr2_Status.Add(new string[] { "0" });
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

            _PatData = new DataSet();
            if (!IPatCon.SelPatInformation(out str_msg, out _PatData, string.Empty))
            {
                ViewBag.Message = str_msg;
                ViewBag.PatientDate = null;
            }
            else
            {
                var _PatTable = _PatData.Tables[0];
                var arr_PatModel = Convert<PatientModel>.ConvertToList(_PatTable);
                ViewBag.PatientDate = arr_PatModel.ToList<PatientModel>();
            }
            PartialViewResult _partialViewResult = PartialView("/Views/Patient/GetPatient.cshtml");
            return _partialViewResult;
        }
    }
    }