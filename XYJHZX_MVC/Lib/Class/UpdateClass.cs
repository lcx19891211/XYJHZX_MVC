using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XYJHZX_MVC.Lib.Class
{
    public class UpdateClass
    {
        public string DbName
        { get; set; }
        public List<string> arr_conditions
        { get; set; }
        public List<string> arr_values
        { get; set; }
        public List<string> arr_orgConditions
        { get; set; }
        public List<string> arr_orgValues
        { get; set; }


        public bool UpdateOpr(out string str_msg)
        {
            try
            {
                if (arr_conditions.Count != arr_values.Count || arr_orgConditions.Count != arr_orgValues.Count)
                {
                    str_msg = "Array Count Is Not Equal";
                    return false;
                }
                string str_check = DbName;
                foreach (string strTmp in arr_conditions)
                {
                    str_check += ";" + strTmp;
                }
                foreach (string strTmp in arr_orgConditions)
                {
                    str_check += ";" + strTmp;
                }
                foreach (string strTmp in arr_orgValues)
                {
                    str_check += ";" + strTmp;
                }
                foreach (string strTmp in arr_values)
                {
                    str_check += ";" + strTmp;
                }
                if (!str_check.ProcessSqlStr())
                {
                    str_msg = "Word Forbidden";
                    return false;
                }
                string str_sql = "update " + DbName + " set ";
                string str_SqlSetValues = "";
                for (int i = 0; i < arr_conditions.Count; i++)
                {
                    if (i > 0)
                        str_SqlSetValues += " , ";
                    str_SqlSetValues += arr_conditions[i] + " = " + arr_values[i] ;
                }
                str_sql += str_SqlSetValues + " where ";
                str_SqlSetValues = "";
                for (int i = 0; i < arr_orgValues.Count; i++)
                {
                    if (i > 0)
                        str_SqlSetValues += " and ";
                    str_SqlSetValues += arr_orgConditions[i] + " = "  + arr_orgValues[i] ;
                }
                str_sql += str_SqlSetValues ;
                str_msg = str_sql;
                str_sql = null;
                return true;
            }
            catch (Exception ex)
            {
                str_msg = ex.Message;
                return false;
            }
        }
    }
}