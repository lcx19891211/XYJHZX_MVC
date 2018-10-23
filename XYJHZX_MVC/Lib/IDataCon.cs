using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using XYJHZX_MVC.Lib.Class;

namespace XYJHZX_MVC.Lib
{
    public interface IDataCon
    {
        bool ConInit(out string str_msg);

        bool SetDBPwd(out string str_msg, string str_orgpassword, string str_newpassword);

        bool SelGroup(out string str_msg, ref DataSet _ResultData);

        bool SelTeam(out string str_msg, ref DataSet _ResultData);

        bool SelMachine(out string str_msg, ref DataSet _ResultData);

        bool SelGroupTeam(out string str_msg, ref DataSet _ResultData, string str_GroupId);

        bool SelTeamMachine(out string str_msg, ref DataSet _ResultData, string str_TeamId);

        bool SelSchedul(out string str_msg, ref DataSet _ResultData, string[] condition);

        bool SelSchedulColumnDetail(out string str_msg, out DataSet _ResultData);

        bool SelSchedulColumnMain(out string str_msg, out DataSet _ResultData);

        bool SelSchedulColumnMainWithList(out string str_msg, out DataSet _ResultData);

        bool SelPatInformation(out string str_msg, out DataSet _ResultData, string str_condition);

        bool CheckPatIsRead(out string str_msg, string[] arr_condition);

        bool SelSchedulColumnType(out string str_msg, out DataSet _ResultData);

        bool SelDateTimeSplit(out string str_msg, out DataSet _ResultData);

        bool SelGetDateTimeSplit(out string str_msg, out DataSet _ResultData, string str_Time);
        
        bool SelGroupDetail(out string str_msg, out DataSet _ResultData);

        bool SelSchedulColumn(out string str_msg, out DataSet _ResultData, int int_type);

        bool SelViewSchedulMain(out string str_msg, out DataSet _ResultData, string[] arr_condition);

        bool SelViewCurrentSchedul(out string str_msg, out DataSet _ResultData, string[] arr_condition);

        bool SelMainIDCurrentSchedulForIDCard(out string str_msg, out DataSet _ResultData, string[] arr_condition);

        bool SelMaxCurrentSeq(out string str_msg, out DataSet _ResultData, string[] arr_condition);

        bool SelPayPatient(out string str_msg, out DataSet _ResultData);

        bool UpdateGroup(out string str_msg, List<string[]> arr2_values, string[] str_orgid);

        bool UpdateMachine(out string str_msg, List<string[]> arr2_values, string[] str_orgid);

        bool UpdateTeam(out string str_msg, List<string[]> arr2_values, string[] str_orgid);

        bool UpdateSchedulColumnMain(out string str_msg, List<string[]> arr2_values, string[] str_orgid);

        bool UpdateSchedulColumnDetail(out string str_msg, List<string[]> arr2_values, string[] str_orgid);

        bool UpdateSchedulDetail(out string str_msg, List<string[]> arr2_values, string[] str_orgid);

        bool UpdateSchedulMain(out string str_msg, List<string[]> arr2_values, string[] str_orgid);

        bool UpadatePatIsRead(out string str_msg, string[] arr_patId);

        bool UpdateSchedulSigninDate(out string str_msg, List<string[]> arr2_values, string[] str_orgid);

        bool UpdateSchedulPatWeight(out string str_msg, List<string[]> arr2_values, string[] str_orgid);

        bool DropMachine(out string str_msg, string str_status, string[] str_orgid);

        bool DropTeam(out string str_msg, string str_status, string[] str_orgid);

        bool DropGroup(out string str_msg, string str_status, string[] str_orgid);

        bool UpdatePatInformation(out string str_msg, List<string[]> arr2_values, string[] str_orgid);

        bool DropSchedulColumnDetail(out string str_msg, string str_status, string[] str_orgid);

        bool DropSchedulColumnMain(out string str_msg, string str_status, string[] str_orgid);

        bool DropPatInformation(out string str_msg, List<string[]> arr2_values, string[] str_orgid);

        bool DropSchedulMain(out string str_msg, List<string[]> arr2_values, string[] str_orgid);

        bool InsertGroup(out string str_msg, List<string[]> arr2_values);

        bool InsertMachine(out string str_msg, List<string[]> arr2_values, ref List<string> str_keyId);

        bool InsertTeam(out string str_msg, List<string[]> arr2_values, ref List<string> str_keyId);

        bool InsertSchedulColumnMain(out string str_msg, List<string[]> arr2_values);

        bool InsertSchedulColumnDetail(out string str_msg, List<string[]> arr2_values);

        bool InsertMashineWithTeam(out string str_msg, List<string[]> arr2_values);

        bool InsertTeamWithGroup(out string str_msg, List<string[]> arr2_values);

        bool InsertSchedulDetail(out string str_msg, List<string[]> arr2_values);

        bool InsertSchedulMain(out string str_msg, List<string[]> arr2_values);

        bool InsertPatInformation(out string str_msg, List<string[]> arr2_values);

        bool DeleteMachineWithTeam(out string str_msg, string[] arr_pkid);

        bool DeleteTeamWithGroup(out string str_msg, string[] arr_pkid);

    }
}
