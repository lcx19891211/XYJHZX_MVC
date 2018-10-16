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
        IDataCon _con = new DataCon();

        public List<SchedulShowModel> SchedulInitTable(out string str_msg, string str_GroupId, string str_SchedulDate, string str_SchedulTime)
        {
            List<SchedulShowModel> arr_schedulShow = new List<SchedulShowModel>();
            DataSet _DataCollect = new DataSet();
            if (_con.ConInit(out str_msg) && _con.SelGroupDetail(out str_msg, out _DataCollect))
            {
                DataTable _table = _DataCollect.Tables[0];
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
                _DataCollect = null;
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
                    for(int i=0;i<_table.Rows.Count;i++)
                    {
                        Int64 str_macid = Int64.Parse(_table.Rows[i]["macid"] + "");
                        foreach(SchedulShowModel _schedulShowModel in arr_schedulShow)
                        {
                            if(_schedulShowModel.macid == str_macid)
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

        public List<GroupModel> GetGroupModel(out string str_msg)
        {
            List<GroupModel> arr_Group = new List<GroupModel>();
            DataSet _DataCollect = new DataSet();
            if (_con.ConInit(out str_msg) && _con.SelGroup(out str_msg, ref _DataCollect))
            {
                DataTable _table = _DataCollect.Tables[0];
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

        public List<TeamModel> GetTeamModel(out string str_msg)
        {
            List<TeamModel> arr_Group = new List<TeamModel>();
            DataSet _DataCollect = new DataSet();
            if (_con.ConInit(out str_msg) && _con.SelTeam(out str_msg, ref _DataCollect))
            {
                DataTable _table = _DataCollect.Tables[0];
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

        public List<PatientModel> GetPatientModels(out string str_msg)
        {
            List<PatientModel> arr_Patient = new List<PatientModel>();

            DataSet _DataCollect = new DataSet();
            if (_con.ConInit(out str_msg) && _con.SelPatInformation(out str_msg, out _DataCollect, string.Empty))
            {
                DataTable _table = _DataCollect.Tables[0];
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
        
        public List<SchedulColumns> GetSchedul2Columns(out string str_msg)
        {
            List<SchedulColumns> arr_SchedulColumns = new List<SchedulColumns>();

            DataSet _DataCollect = new DataSet();
            if (_con.ConInit(out str_msg) && _con.SelSchedulColumn(out str_msg, out _DataCollect, 2))
            {
                DataTable _table = _DataCollect.Tables[0];
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

        public List<SchedulColumns> GetSchedul1Columns(out string str_msg)
        {
            List<SchedulColumns> arr_SchedulColumns = new List<SchedulColumns>();

            DataSet _DataCollect = new DataSet();
            if (_con.ConInit(out str_msg) && _con.SelSchedulColumn(out str_msg, out _DataCollect, 1))
            {
                DataTable _table = _DataCollect.Tables[0];
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

        public List<SplitDateModel> GetSplitDateModels(out string str_msg)
        {
            List<SplitDateModel> arr_splitDateModels = new List<SplitDateModel>();
            DataSet _DataCollect = new DataSet();
            if (_con.ConInit(out str_msg) && _con.SelDateTimeSplit(out str_msg, out _DataCollect))
            {
                DataTable _table = _DataCollect.Tables[0];
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

        public List<CurrentSchedulModel> GetCurrentSchedulModels(out string str_msg,string str_GroupId, string str_TeamId)
        {
            List<CurrentSchedulModel> arr_currentSchedulModels = new List<CurrentSchedulModel>();
            DataSet _DataCollect = new DataSet();
            if(_con.ConInit(out str_msg) && _con.SelGroupDetail(out str_msg,out _DataCollect))
            {
                DataTable _table = _DataCollect.Tables[0];
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

            _DataCollect = new DataSet();
            if (!string.IsNullOrEmpty(str_GroupId))
            {
                List<string> arr_condition = new List<string>();
                arr_condition.Add(str_GroupId);
                if (!string.IsNullOrEmpty(str_TeamId))
                {
                    arr_condition.Add(str_TeamId);
                }
                if (_con.SelViewCurrentSchedul(out str_msg, out _DataCollect, arr_condition.ToArray()))
                {
                    DataTable _table = _DataCollect.Tables[0];
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
    }
}