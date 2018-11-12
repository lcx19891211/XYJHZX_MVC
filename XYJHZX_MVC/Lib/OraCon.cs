using System;
using System.Collections.Generic;
using XYJHZX_MVC.Lib.Class;
using ClientGetDateSvr;
using System.Data;

namespace XYJHZX_MVC.Lib
{

    public class OraCon : IDataCon
    {
        SvrInterface sif = new SvrInterface();
        IOAGetData proxy = null;
        private IXmlOper IXmlConfig = new XmlOper();//Xml设置接口
        private string str_OraType = "Oracle";
        string url = "http://200.10.6.250:9999/Servers.svc";

        #region 连接初始化
        /// <summary>
        /// 初始化连接
        /// </summary>
        /// <param name="str_msg"></param>
        /// <returns></returns>
        public bool ConInit(out string str_msg)
        {
            try
            {
                List<string> arr_strConfig = new List<string>();
                if (IXmlConfig.GetDataForXml(out str_msg, ref arr_strConfig, "Connect2"))
                {
                    if (arr_strConfig.Count > 1 && !string.IsNullOrEmpty(arr_strConfig[1]))
                    {
                        url = arr_strConfig[1].Trim();
                    }
                    if (arr_strConfig.Count > 0 && !string.IsNullOrEmpty(arr_strConfig[0]))
                    {
                        str_OraType = arr_strConfig[0].Trim();
                    }
                    sif.url = url;
                    str_msg = sif.CreateNewConn(ref proxy);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                str_msg = ex.Message;
                return false;
            }
        }
        #endregion
        
        /// <summary>
        /// 获取透析缴费病人
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <returns></returns>
        public bool SelPayPatient(out string str_msg, out DataSet _ResultData)
        {
            _ResultData = new DataSet();
            string str_sql = "select distinct 卡号,日期,性别,病人姓名,开方科室,身份证,电话,出生日期 from MP.XCK_XYTX t";
            str_msg = proxy.GetDates(str_sql, ref _ResultData, ref str_OraType);
            if (_ResultData != null && _ResultData.Tables.Count > 0)
                return true;
            else
                return false;
        }

        #region 未实现接口函数
        public bool SetDBPwd(out string str_msg, string str_orgpassword, string str_newpassword) { str_msg = ""; return true; }

        public bool SelGroup(out string str_msg, ref DataSet _ResultData) { str_msg = ""; return true; }

        public bool SelTeam(out string str_msg, ref DataSet _ResultData) { str_msg = ""; return true; }

        public bool SelMachine(out string str_msg, ref DataSet _ResultData) { str_msg = ""; return true; }

        public bool SelGroupTeam(out string str_msg, ref DataSet _ResultData, string str_GroupId) { str_msg = ""; return true; }

        public bool SelTeamMachine(out string str_msg, ref DataSet _ResultData, string str_TeamId) { str_msg = ""; return true; }

        public bool SelSchedul(out string str_msg, ref DataSet _ResultData, string[] condition) { str_msg = ""; return true; }

        public bool SelSchedulWithMainId(out string str_msg, ref DataSet _ResultData, string str_mainid) { str_msg = ""; return true; }

        public bool SelSchedulColumnDetail(out string str_msg, out DataSet _ResultData, string str_colMainId) { str_msg = ""; _ResultData = null; return true; }

        public bool SelSchedulColumnMain(out string str_msg, out DataSet _ResultData) { str_msg = ""; _ResultData = null; return true; }

        public bool SelSchedulColumnMainWithList(out string str_msg, out DataSet _ResultData) { str_msg = ""; _ResultData = null; return true; }

        public bool SelPatInformation(out string str_msg, out DataSet _ResultData, string str_condition) { str_msg = ""; _ResultData = null; return true; }

        public bool CheckPatIsRead(out string str_msg, string[] arr_condition) { str_msg = ""; return true; }

        public bool SelSchedulColumnType(out string str_msg, out DataSet _ResultData) { str_msg = ""; _ResultData = null; return true; }

        public bool SelDateTimeSplit(out string str_msg, out DataSet _ResultData) { str_msg = ""; _ResultData = null; return true; }

        public bool SelGetDateTimeSplit(out string str_msg, out DataSet _ResultData, string str_Time) { str_msg = ""; _ResultData = null; return true; }

        public bool SelGroupDetail(out string str_msg, out DataSet _ResultData) { str_msg = ""; _ResultData = null; return true; }

        public bool SelSchedulColumn(out string str_msg, out DataSet _ResultData, int int_type) { str_msg = ""; _ResultData = null; return true; }

        public bool SelViewSchedulMain(out string str_msg, out DataSet _ResultData, string[] arr_condition) { str_msg = ""; _ResultData = null; return true; }

        public bool SelViewCurrentSchedul(out string str_msg, out DataSet _ResultData, string[] arr_condition) { str_msg = ""; _ResultData = null; return true; }

        public bool SelMainIDCurrentSchedulForIDCard(out string str_msg, out DataSet _ResultData, string[] arr_condition) { str_msg = ""; _ResultData = null; return true; }

        public bool SelMaxCurrentSeq(out string str_msg, out DataSet _ResultData, string[] arr_condition) { str_msg = ""; _ResultData = null; return true; }

        public bool SelMaxSchedulSeq(out string str_msg, out DataSet _ResultData, string[] arr_condition) { str_msg = ""; _ResultData = null; return true; }

        public bool UpdateGroup(out string str_msg, List<string[]> arr2_values, string[] str_orgid) { str_msg = ""; return true; }

        public bool UpdateMachine(out string str_msg, List<string[]> arr2_values, string[] str_orgid) { str_msg = ""; return true; }

        public bool UpdateTeam(out string str_msg, List<string[]> arr2_values, string[] str_orgid) { str_msg = ""; return true; }

        public bool UpdateSchedulColumnMain(out string str_msg, List<string[]> arr2_values, string[] str_orgid) { str_msg = ""; return true; }

        public bool UpdateSchedulColumnDetail(out string str_msg, List<string[]> arr2_values, string[] str_orgid) { str_msg = ""; return true; }

        public bool UpdateSchedulDetail(out string str_msg, List<string[]> arr2_values, string[] str_orgid) { str_msg = ""; return true; }

        public bool UpdateSchedulMain(out string str_msg, List<string[]> arr2_values, string[] str_orgid) { str_msg = ""; return true; }

        public bool UpadatePatIsRead(out string str_msg, string[] arr_patId) { str_msg = ""; return true; }

        public bool UpdateSchedulSigninDate(out string str_msg, List<string[]> arr2_values, string[] str_orgid) { str_msg = ""; return true; }

        public bool UpdateSchedulPatWeight(out string str_msg, List<string[]> arr2_values, string[] str_orgid) { str_msg = ""; return true; }

        public bool DropMachine(out string str_msg, string str_status, string[] str_orgid) { str_msg = ""; return true; }

        public bool DropTeam(out string str_msg, string str_status, string[] str_orgid) { str_msg = ""; return true; }

        public bool DropGroup(out string str_msg, string str_status, string[] str_orgid) { str_msg = ""; return true; }

        public bool UpdatePatInformation(out string str_msg, List<string[]> arr2_values, string[] str_orgid) { str_msg = ""; return true; }

        public bool DropSchedulColumnDetail(out string str_msg, string str_status, string[] str_orgid) { str_msg = ""; return true; }

        public bool DropSchedulColumnMain(out string str_msg, string str_status, string[] str_orgid) { str_msg = ""; return true; }

        public bool DropPatInformation(out string str_msg, List<string[]> arr2_values, string[] str_orgid) { str_msg = ""; return true; }

        public bool DropSchedulMain(out string str_msg, List<string[]> arr2_values, string[] str_orgid) { str_msg = ""; return true; }

        public bool InsertGroup(out string str_msg, List<string[]> arr2_values) { str_msg = ""; return true; }

        public bool InsertMachine(out string str_msg, List<string[]> arr2_values, ref List<string> str_keyId) { str_msg = ""; return true; }

        public bool InsertTeam(out string str_msg, List<string[]> arr2_values, ref List<string> str_keyId) { str_msg = ""; return true; }

        public bool InsertSchedulColumnMain(out string str_msg, List<string[]> arr2_values) { str_msg = ""; return true; }

        public bool InsertSchedulColumnDetail(out string str_msg, List<string[]> arr2_values) { str_msg = ""; return true; }

        public bool InsertMashineWithTeam(out string str_msg, List<string[]> arr2_values) { str_msg = ""; return true; }

        public bool InsertTeamWithGroup(out string str_msg, List<string[]> arr2_values) { str_msg = ""; return true; }

        public bool InsertSchedulDetail(out string str_msg, List<string[]> arr2_values) { str_msg = ""; return true; }

        public bool InsertSchedulMain(out string str_msg, List<string[]> arr2_values) { str_msg = ""; return true; }

        public bool InsertPatInformation(out string str_msg, List<string[]> arr2_values) { str_msg = ""; return true; }

        public bool DeleteMachineWithTeam(out string str_msg, string[] arr_pkid) { str_msg = ""; return true; }

        public bool DeleteTeamWithGroup(out string str_msg, string[] arr_pkid) { str_msg = ""; return true; }
        #endregion
    }
}