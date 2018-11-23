using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.Mvc;
using XYJHZX_MVC.Lib;
using XYJHZX_MVC.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace XYJHZX_MVC.Lib
{
    public class GetData
    {
        IDataCon _con = new OraCon();
        /// <summary>
        /// 返回检查排队列表
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="str_GroupId"></param>
        /// <param name="str_SchedulDate"></param>
        /// <param name="str_SchedulTime"></param>
        /// <returns></returns>
        public List<SchedulShowModel> SchedulInitTable(out string str_msg, string str_GroupId, string str_SchedulDate, string str_SchedulTime)
        {
            List<SchedulShowModel> arr_schedulShow = new List<SchedulShowModel>();
            DataSet _DataSet = new DataSet();
            if (_con.ConInit(out str_msg) && _con.SelGroupDetail(out str_msg, out _DataSet))
            {
                DataTable _table = _DataSet.Tables[0];
                for (int i = 0; i < _table.Rows.Count; i++)
                {
                    if (_table.Rows[i]["groupid"] + "" == str_GroupId)
                    {
                        SchedulShowModel _SchedulShowModel = new SchedulShowModel();
                        _SchedulShowModel.macid = Int64.Parse(_table.Rows[i]["macid"] + "");
                        _SchedulShowModel.macname = _table.Rows[i]["macname"] + "";
                        arr_schedulShow.Add(_SchedulShowModel);
                    }
                }
                _DataSet = null;
            }
            else
                return null;
            if (!string.IsNullOrEmpty(str_SchedulDate) && !string.IsNullOrEmpty(str_SchedulTime))
            {
                DataSet _DataTmp = new DataSet();
                string[] arr_condition = { str_GroupId, str_SchedulDate, str_SchedulTime };
                if (_con.SelViewSchedulMain(out str_msg, out _DataTmp, arr_condition))
                {
                    DataTable _table = _DataTmp.Tables[0];
                    for (int i = 0; i < _table.Rows.Count; i++)
                    {
                        Int64 str_macid = Int64.Parse(_table.Rows[i]["macid"] + "");
                        foreach (SchedulShowModel _schedulShowModel in arr_schedulShow)
                        {
                            if (_schedulShowModel.macid == str_macid)
                            {
                                _schedulShowModel.mainid = Int64.Parse(_table.Rows[i]["mainid"] + "");
                                _schedulShowModel.patid = Int64.Parse(_table.Rows[i]["patid"] + "");
                                _schedulShowModel.patName = (_table.Rows[i]["patName"] + "");
                                _schedulShowModel.remark = (_table.Rows[i]["remark"] + "");
                                _schedulShowModel.routeName = (_table.Rows[i]["routeName"] + "");
                                _schedulShowModel.SchedulDate = (_table.Rows[i]["SchedulDate"] + "");
                                _schedulShowModel.SchedulTime = (_table.Rows[i]["SchedulTime"] + "");
                                _schedulShowModel.anticoagulantName = (_table.Rows[i]["anticoagulantName"] + "");
                                _schedulShowModel.dialyzerName = (_table.Rows[i]["dialyzerName"] + "");
                                break;
                            }
                        }
                    }
                }
            }

            return arr_schedulShow;
        }

        /// <summary>
        /// 获取分区列表
        /// </summary>
        /// <param name="str_msg"></param>
        /// <returns></returns>
        public List<GroupModel> GetGroupModel(out string str_msg)
        {
            List<GroupModel> arr_Group = new List<GroupModel>();
            DataSet _DataSet = new DataSet();
            if (_con.ConInit(out str_msg) && _con.SelGroup(out str_msg, ref _DataSet))
            {
                DataTable _table = _DataSet.Tables[0];
                for (int i = 0; i < _table.Rows.Count; i++)
                {
                    GroupModel _GroupModel = new GroupModel();
                    _GroupModel.Groupid = int.Parse(_table.Rows[i]["GroupId"] + "");
                    _GroupModel.GroupName = _table.Rows[i]["GroupName"] + "";
                    _GroupModel.Desciption = _table.Rows[i]["Desciption"] + "";
                    _GroupModel.SeqID = _table.Rows[i]["SeqID"] + "";
                    arr_Group.Add(_GroupModel);
                }
            }
            return arr_Group;
        }
        /// <summary>
        /// 返回分组列表
        /// </summary>
        /// <param name="str_msg"></param>
        /// <returns></returns>
        public List<TeamModel> GetTeamModel(out string str_msg)
        {
            List<TeamModel> arr_Group = new List<TeamModel>();
            DataSet _DataSet = new DataSet();
            if (_con.ConInit(out str_msg) && _con.SelTeam(out str_msg, ref _DataSet))
            {
                DataTable _table = _DataSet.Tables[0];
                for (int i = 0; i < _table.Rows.Count; i++)
                {
                    TeamModel _TeamModel = new TeamModel();
                    _TeamModel.Teamid = int.Parse(_table.Rows[i]["Teamid"] + "");
                    _TeamModel.TeamName = _table.Rows[i]["TeamName"] + "";
                    _TeamModel.Desciption = _table.Rows[i]["Desciption"] + "";
                    _TeamModel.SeqID = _table.Rows[i]["SeqID"] + "";
                    arr_Group.Add(_TeamModel);
                }
            }
            return arr_Group;
        }
        /// <summary>
        /// 返回病人列表
        /// </summary>
        /// <param name="str_msg"></param>
        /// <returns></returns>
        public List<PatientModel> GetPatientModels(out string str_msg)
        {
            List<PatientModel> arr_Patient = new List<PatientModel>();

            DataSet _DataSet = new DataSet();
            if (_con.ConInit(out str_msg) && _con.SelPatInformation(out str_msg, out _DataSet, string.Empty))
            {
                DataTable _table = _DataSet.Tables[0];
                for (int i = 0; i < _table.Rows.Count; i++)
                {
                    PatientModel _patientModel = new PatientModel();
                    _patientModel.PatId = int.Parse(_table.Rows[i]["PatId"] + "");
                    _patientModel.PatName = _table.Rows[i]["PatName"] + "";
                    _patientModel.PatAge = int.Parse(_table.Rows[i]["PatAge"] + "");
                    _patientModel.PatBrithday = _table.Rows[i]["PatBrithday"] + "";
                    _patientModel.IsRead = int.Parse(_table.Rows[i]["IsRead"] + "");
                    _patientModel.PatIdCardNo = _table.Rows[i]["PatIdCardNo"] + "";
                    _patientModel.PatOutCardNo = _table.Rows[i]["PatOutCardNo"] + "";
                    _patientModel.PatSex = _table.Rows[i]["PatSex"] + "";
                    _patientModel.PaymentDate = _table.Rows[i]["PaymentDate"] + "";
                    _patientModel.Remark = _table.Rows[i]["Remark"] + "";
                    _patientModel.SendDeptId = _table.Rows[i]["SendDeptId"] + "";
                    _patientModel.SendDeptName = _table.Rows[i]["SendDeptName"] + "";
                    _patientModel.TelphoneNo = _table.Rows[i]["TelphoneNo"] + "";
                    arr_Patient.Add(_patientModel);
                }
            }
            return arr_Patient;
        }
        /// <summary>
        /// 返回检查打印列表
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="str_GroupId"></param>
        /// <param name="str_SchedulDate"></param>
        /// <param name="str_SchedulTime"></param>
        /// <returns></returns>
        public List<SchedulPrint> GetSchedulPrint(out string str_msg, string str_GroupId, string str_SchedulDate, string str_SchedulTime)
        {
            List<SchedulPrint> arr_schedulPrints = new List<SchedulPrint>();
            DataSet _DataSet = new DataSet();
            string[] arr_condition = { str_SchedulDate, str_SchedulTime, str_GroupId };
            if (_con.ConInit(out str_msg) && _con.SelSchedul(out str_msg, ref _DataSet, arr_condition))
            {
                DataTable _table = _DataSet.Tables[0];
                for (int i = 0; i < _table.Rows.Count; i++)
                {
                    SchedulPrint _schedulPrint = new SchedulPrint();
                 
                    _schedulPrint.GroupName = _table.Rows[i]["groupname"] + "";
                    _schedulPrint.GroupDescption = _table.Rows[i]["groupdescption"] + "";
                    _schedulPrint.TeamName = _table.Rows[i]["teamname"] + "";
                    _schedulPrint.TeamDescption = _table.Rows[i]["teamdescption"] + "";
                    _schedulPrint.MacName = _table.Rows[i]["macname"] + "";
                    _schedulPrint.MacDescption = _table.Rows[i]["macdescption"] + "";
                    _schedulPrint.SchedulDate = _table.Rows[i]["SchedulDate"] + "";
                    _schedulPrint.SchedulTime = _table.Rows[i]["SchedulTime"] + "";
                    _schedulPrint.DialyzerName = _table.Rows[i]["dialyzerName"] + "";
                    _schedulPrint.RouteName = _table.Rows[i]["routeName"] + "";
                    _schedulPrint.AnticoagulantName = _table.Rows[i]["anticoagulantName"] + "";
                    _schedulPrint.Remark = _table.Rows[i]["remark"] + "";
                    _schedulPrint.SigninDate = _table.Rows[i]["signinDate"] + "";
                    _schedulPrint.SignInSeq = int.Parse("0" + _table.Rows[i]["SignInSeq"] );
                    _schedulPrint.PatName = _table.Rows[i]["patName"] + "";
                    _schedulPrint.PatIdCardNo = _table.Rows[i]["patIdCardNo"] + "";
                    _schedulPrint.MainId = _table.Rows[i]["mainid"] + "";
                    _schedulPrint.PatWeightBefore = int.Parse("0" + _table.Rows[i]["patWeightBefore"]);
                    _schedulPrint.PatWeightAfter = int.Parse("0" + _table.Rows[i]["patWeightAfter"]);

                    arr_schedulPrints.Add(_schedulPrint);
                }
            }
            return arr_schedulPrints;
        }
        /// <summary>
        /// 返回检查Columns列表
        /// </summary>
        /// <param name="str_msg"></param>
        /// <returns></returns>
        public List<SchedulColumns> GetSchedulColumns(out string str_msg,int int_colType)
        {
            List<SchedulColumns> arr_SchedulColumns = new List<SchedulColumns>();

            DataSet _DataSet = new DataSet();
            if (_con.ConInit(out str_msg) && _con.SelSchedulColumn(out str_msg, out _DataSet, int_colType))
            {
                DataTable _table = _DataSet.Tables[0];
                for (int i = 0; i < _table.Rows.Count; i++)
                {
                    SchedulColumns _schedulColumns = new SchedulColumns();
                    _schedulColumns.colMainId = int.Parse(_table.Rows[i]["colMainId"] + "");
                    _schedulColumns.colName = _table.Rows[i]["colName"] + "";
                    _schedulColumns.colType = int.Parse(_table.Rows[i]["colType"] + "");
                    _schedulColumns.desciption = _table.Rows[i]["desciption"] + "";
                    _schedulColumns.colDetailId = int.Parse(_table.Rows[i]["colDetailId"] + "");
                    _schedulColumns.colDetailName = _table.Rows[i]["colDetailName"] + "";
                    arr_SchedulColumns.Add(_schedulColumns);
                }
            }
            return arr_SchedulColumns;
        }
        /// <summary>
        /// 返回检查项目列表
        /// </summary>
        /// <param name="str_msg"></param>
        /// <returns></returns>
        public List<SchedulColumns> GetSchedul1Columns(out string str_msg)
        {
            List<SchedulColumns> arr_SchedulColumns = new List<SchedulColumns>();

            DataSet _DataSet = new DataSet();
            if (_con.ConInit(out str_msg) && _con.SelSchedulColumn(out str_msg, out _DataSet, 1))
            {
                DataTable _table = _DataSet.Tables[0];
                for (int i = 0; i < _table.Rows.Count; i++)
                {
                    SchedulColumns _schedulColumns = new SchedulColumns();
                    _schedulColumns.colMainId = int.Parse(_table.Rows[i]["colMainId"] + "");
                    _schedulColumns.colName = _table.Rows[i]["colName"] + "";
                    _schedulColumns.colType = int.Parse(_table.Rows[i]["colType"] + "");
                    _schedulColumns.desciption = _table.Rows[i]["desciption"] + "";
                    _schedulColumns.colDetailId = int.Parse(_table.Rows[i]["colDetailId"] + "");
                    _schedulColumns.colDetailName = _table.Rows[i]["colDetailName"] + "";
                    arr_SchedulColumns.Add(_schedulColumns);
                }
            }
            return arr_SchedulColumns;
        }
        /// <summary>
        /// 返回时间段列表
        /// </summary>
        /// <param name="str_msg"></param>
        /// <returns></returns>
        public List<SplitDateModel> GetSplitDateModels(out string str_msg)
        {
            List<SplitDateModel> arr_splitDateModels = new List<SplitDateModel>();
            DataSet _DataSet = new DataSet();
            if (_con.ConInit(out str_msg) && _con.SelDateTimeSplit(out str_msg, out _DataSet))
            {
                DataTable _table = _DataSet.Tables[0];
                for (int i = 0; i < _table.Rows.Count; i++)
                {
                    SplitDateModel _splitDateModel = new SplitDateModel();
                    _splitDateModel.SplitId = int.Parse(_table.Rows[i]["SplitId"] + "");
                    _splitDateModel.SplitName = _table.Rows[i]["SplitName"] + "";
                    _splitDateModel.SplitDesciption = _table.Rows[i]["SplitDesciption"] + "";
                    _splitDateModel.BegintTime = _table.Rows[i]["BegintTime"] + "";
                    _splitDateModel.EndTime = _table.Rows[i]["EndTime"] + "";
                    arr_splitDateModels.Add(_splitDateModel);
                }
            }
            return arr_splitDateModels;
        }
        /// <summary>
        /// 返回当前时间检查排队列表
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="str_GroupId"></param>
        /// <param name="str_TeamId"></param>
        /// <returns></returns>
        public List<CurrentSchedulModel> GetCurrentSchedulModels(out string str_msg,string str_GroupId, string str_TeamId)
        {
            List<CurrentSchedulModel> arr_currentSchedulModels = new List<CurrentSchedulModel>();
            DataSet _DataSet = new DataSet();
            if(_con.ConInit(out str_msg) && _con.SelGroupDetail(out str_msg,out _DataSet))
            {
                DataTable _table = _DataSet.Tables[0];
                for (int i = 0; i < _table.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(str_GroupId) && str_GroupId == _table.Rows[i]["GroupId"] + "" && ((string.IsNullOrEmpty(str_TeamId) || (!string.IsNullOrEmpty(str_TeamId) && str_TeamId == _table.Rows[i]["teamid"] + ""))))
                    {
                        CurrentSchedulModel _currentSchedulModel = new CurrentSchedulModel();
                        _currentSchedulModel.macid = int.Parse(_table.Rows[i]["macid"] + "");
                        _currentSchedulModel.macname = _table.Rows[i]["macname"] + "";
                        _currentSchedulModel.GroupDescption = _table.Rows[i]["GroupDescption"] + "";
                        _currentSchedulModel.teamDescption = _table.Rows[i]["teamDescption"] + "";
                        arr_currentSchedulModels.Add(_currentSchedulModel);
                    }
                }
            }

            _DataSet = new DataSet();
            if (!string.IsNullOrEmpty(str_GroupId))
            {
                List<string> arr_condition = new List<string>();
                arr_condition.Add(str_GroupId);
                if (!string.IsNullOrEmpty(str_TeamId))
                {
                    arr_condition.Add(str_TeamId);
                }
                if (_con.SelViewCurrentSchedul(out str_msg, out _DataSet, arr_condition.ToArray()))
                {
                    DataTable _table = _DataSet.Tables[0];
                    for (int i = 0; i < _table.Rows.Count; i++)
                    {
                        for (int j = 0; j < arr_currentSchedulModels.Count(); j++)
                        {
                            if (arr_currentSchedulModels[j].macid == int.Parse(_table.Rows[i]["macid"] + ""))
                            {
                                arr_currentSchedulModels[j].PatName = _table.Rows[i]["PatName"] + "";
                                arr_currentSchedulModels[j].signInDate = _table.Rows[i]["signInDate"] + "";
                                arr_currentSchedulModels[j].SignInSeq = int.Parse("0" +_table.Rows[i]["SignInSeq"]);
                                break;
                            }
                        }
                    }
                }
            }
            else
                str_msg = "parameter is null!";
            return arr_currentSchedulModels;
        }

        /// <summary>
        /// 返回时间段
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="str_time"></param>
        /// <returns></returns>
        public string GetDateSplit(out string str_msg,string str_time)
        {
            DataSet _DataResult = new DataSet();
            if (_con.ConInit(out str_msg) && _con.SelGetDateTimeSplit(out str_msg, out _DataResult, str_time))
            {
                if (_DataResult != null && _DataResult.Tables[0].Rows.Count > 0)
                {
                    return _DataResult.Tables[0].Rows[0][0] + "";
                }
                else
                    return string.Empty;
            }
            else
                return string.Empty;
        }
        /// <summary>
        /// 返回排序号最大值
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="str_teamid"></param>
        /// <returns></returns>
        public int GetMaxCurrenSeq(out string str_msg, string str_teamid)
        {
            DataSet _DataResult = new DataSet();
            if (_con.ConInit(out str_msg) && _con.SelMaxCurrentSeq(out str_msg, out _DataResult, new string[] { str_teamid }))
            {
                if (_DataResult != null && _DataResult.Tables[0].Rows.Count > 0)
                {
                    string str_seq = _DataResult.Tables[0].Rows[0][0] + "";
                    return (string.IsNullOrEmpty(str_seq) ? 1 : int.Parse(str_seq) + 1);
                }
                else
                    return 1;
            }
            else
                return 1;
        }


        /// <summary>
        /// 返回字段类型
        /// </summary>
        /// <param name="str_msg"></param>
        /// <returns></returns>
        public List<SchedulColumnTypeModel> GetSchedulColumnType(out string str_msg)
        {
            List<SchedulColumnTypeModel> arr_schedulColumnTypeModels = new List<SchedulColumnTypeModel>();

            DataSet _DataSet = new DataSet();
            if (_con.ConInit(out str_msg) && _con.SelSchedulColumnType(out str_msg, out _DataSet))
            {
                DataTable _table = _DataSet.Tables[0];
                for (int i = 0; i < _table.Rows.Count; i++)
                {
                    SchedulColumnTypeModel _schedulColumns = new SchedulColumnTypeModel();
                    _schedulColumns.TypeSign = _table.Rows[i]["typeSign"] + "";
                    _schedulColumns.TypeName = _table.Rows[i]["typeName"] + "";
                    arr_schedulColumnTypeModels.Add(_schedulColumns);
                }
            }
            return arr_schedulColumnTypeModels;
        }

        /// <summary>
        /// 返回列标题
        /// </summary>
        /// <param name="str_msg"></param>
        /// <returns></returns>
        public List<SchedulColumnMainModel> GetSchedulColumnMain(out string str_msg)
        {
            List<SchedulColumnMainModel> arr_schedulColumnMainModels = new List<SchedulColumnMainModel>();
            DataSet _DataSet = new DataSet();
            if(_con.ConInit(out str_msg)&&_con.SelSchedulColumnMain(out str_msg,out _DataSet))
            {
                DataTable _table = _DataSet.Tables[0];
                for(int i=0;i<_table.Rows.Count;i++)
                {
                    SchedulColumnMainModel _schedulColumnMainModel = new SchedulColumnMainModel();
                    _schedulColumnMainModel.ColMainId = int.Parse(_table.Rows[i]["colMainId"] + "");
                    _schedulColumnMainModel.ColName = _table.Rows[i]["colName"] + "";
                    _schedulColumnMainModel.ColType = int.Parse(_table.Rows[i]["colType"] + "");
                    _schedulColumnMainModel.Desciption = _table.Rows[i]["desciption"] + "";
                    _schedulColumnMainModel.Status = "0";
                    arr_schedulColumnMainModels.Add(_schedulColumnMainModel);
                }
            }
            return arr_schedulColumnMainModels;
        }

        /// <summary>
        /// 返回可选值
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="str_colMainId"></param>
        /// <returns></returns>
        public List<SchedulColumnDetailModel> GetSchedulColumnDetail(out string str_msg, string str_colMainId)
        {
            List<SchedulColumnDetailModel> _schedulColumnDetailModels = new List<SchedulColumnDetailModel>();
            DataSet _DataSet = new DataSet();
            if (_con.ConInit(out str_msg) && !string.IsNullOrEmpty(str_colMainId) && _con.SelSchedulColumnDetail(out str_msg, out _DataSet, str_colMainId))
            {
                DataTable _table = _DataSet.Tables[0];
                for (int i = 0; i < _table.Rows.Count; i++)
                {
                    SchedulColumnDetailModel _schedulColumnDetailModel = new SchedulColumnDetailModel();
                    _schedulColumnDetailModel.ColDetailId = int.Parse(_table.Rows[i]["colDetailId"] + "");
                    _schedulColumnDetailModel.ColDetailName = _table.Rows[i]["colDetailName"] + "";
                    _schedulColumnDetailModel.ColMainId = int.Parse(_table.Rows[i]["colMainId"] + "");
                    _schedulColumnDetailModel.Desciption = _table.Rows[i]["desciption"] + "";
                    _schedulColumnDetailModel.Status = "0";
                    _schedulColumnDetailModels.Add(_schedulColumnDetailModel);

                }
            }
            return _schedulColumnDetailModels;
        }
    }
}