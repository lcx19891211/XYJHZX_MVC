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

        public List<SchedulShowModel> SchedulInitTable(out string str_msg, string str_GroupId)
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
    }
}