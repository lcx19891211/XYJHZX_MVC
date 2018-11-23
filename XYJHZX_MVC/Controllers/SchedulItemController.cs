using XYJHZX_MVC.Lib;
using XYJHZX_MVC.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace XYJHZX_MVC.Controllers
{
    public class SchedulItemController : Controller
    {
        IDataCon _ICon = new OraCon();
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
            ViewBag.ColMainID = ColMainID;
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
        public ActionResult SaveMain(List<SchedulColumnMainModel> schedulColumnMains)
        {
            if (_ICon.ConInit(out str_msg) && schedulColumnMains != null)
            {
                if (schedulColumnMains.Count > 0)
                {
                    List<string[]> arr2_insertList = new List<string[]>();
                    List<string[]> arr2_updateValList = new List<string[]>();
                    List<string> arr_updateIdLIst = new List<string>();
                    List<string> arr_dropList = new List<string>();
                    for (int i = 0; i < schedulColumnMains.Count; i++)
                    {
                        string str_colMainId = schedulColumnMains[i].ColMainId.ToString();
                        switch (schedulColumnMains[i].Status)
                        {
                            case "1":
                                if (string.IsNullOrEmpty(str_colMainId) || str_colMainId == "0")
                                {
                                    string[] arr_insertList = { schedulColumnMains[i].ColName.ConvertSqlCondition(), schedulColumnMains[i].Desciption.ConvertSqlCondition(), schedulColumnMains[i].ColType.ToString() };
                                    arr2_insertList.Add(arr_insertList);
                                }
                                else
                                {
                                    string[] arr_updateValList = { schedulColumnMains[i].ColName.ConvertSqlCondition(), schedulColumnMains[i].Desciption.ConvertSqlCondition(), schedulColumnMains[i].ColType.ToString() };
                                    arr2_updateValList.Add(arr_updateValList);
                                    arr_updateIdLIst.Add(str_colMainId);
                                }
                                break;
                            case "-1":
                                if (!string.IsNullOrEmpty(str_colMainId) && str_colMainId != "0")
                                {
                                    arr_dropList.Add(str_colMainId);
                                }
                                break;
                            default: break;
                        }
                    }
                    _ICon.InsertSchedulColumnMain(out str_msg, arr2_insertList);
                    _ICon.UpdateSchedulColumnMain(out str_msg, arr2_updateValList, arr_updateIdLIst.ToArray());
                    _ICon.DropSchedulColumnMain(out str_msg, "0", arr_dropList.ToArray());
                    List<SchedulColumnMainModel> _schedulColumnMainModels = _GetData.GetSchedulColumnMain(out str_msg);
                    ViewBag.MainData = _schedulColumnMainModels;
                    List<SchedulColumnTypeModel> _schedulColumnTypeModels = _GetData.GetSchedulColumnType(out str_msg);
                    _schedulColumnTypeModels.Insert(0, new SchedulColumnTypeModel());
                    ViewBag.TypeData = _schedulColumnTypeModels;
                }
                else
                {
                    ViewBag.DetailData = null;
                }
            }
            else
            {
                ViewBag.DetailData = null;
            }
            PartialViewResult x = PartialView("/Views/SchedulItem/Titles.cshtml");
            return x;
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SaveItems(List<SchedulColumnDetailModel> Items)
        {
            if(_ICon.ConInit(out str_msg)&&Items!= null)
            {
                if(Items.Count>0)
                {
                    List<string[]> arr2_insertList = new List<string[]>();
                    List<string[]> arr2_updateValList = new List<string[]>();
                    List<string> arr_updateIdLIst = new List<string>();
                    List<string> arr_dropList = new List<string>();
                    string str_colMainID = "";
                    for(int i = 0;i<Items.Count;i++)
                    {
                        str_colMainID = Items[i].ColMainId.ToString();
                        switch (Items[i].Status)
                        {
                            case "1":
                                if(string.IsNullOrEmpty(Items[i].ColDetailId.ToString()) || Items[i].ColDetailId.ToString() == "0")
                                {
                                    string[] arr_insertList = { str_colMainID, Items[i].ColDetailName.ConvertSqlCondition(), Items[i].Desciption.ConvertSqlCondition() };
                                    arr2_insertList.Add(arr_insertList);
                                }
                                else
                                {
                                    string[] arr_updateValList = { Items[i].ColDetailName.ConvertSqlCondition(), Items[i].Desciption.ConvertSqlCondition() };
                                    arr2_updateValList.Add(arr_updateValList);
                                    arr_updateIdLIst.Add(Items[i].ColDetailId.ToString());
                                }
                                break;
                            case "-1":
                                if (!string.IsNullOrEmpty(Items[i].ColDetailId.ToString()) && Items[i].ColDetailId.ToString() != "0")
                                {
                                    arr_dropList.Add(Items[i].ColDetailId.ToString());
                                }
                                break;
                            default:break;
                        }
                    }
                    _ICon.InsertSchedulColumnDetail(out str_msg, arr2_insertList);
                    _ICon.UpdateSchedulColumnDetail(out str_msg, arr2_updateValList, arr_updateIdLIst.ToArray());
                    _ICon.DropSchedulColumnDetail(out str_msg, "0", arr_dropList.ToArray());
                    List<SchedulColumnDetailModel> _schedulColumnDetailModels = _GetData.GetSchedulColumnDetail(out str_msg, str_colMainID);
                    ViewBag.DetailData = _schedulColumnDetailModels;
                }
                else
                {
                    ViewBag.DetailData = null;
                }
            }
            else
            {
                ViewBag.DetailData = null;
            }
            PartialViewResult x = PartialView("/Views/SchedulItem/Items.cshtml");
            return x;
        }
    }

}