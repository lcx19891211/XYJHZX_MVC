using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XYJHZX_MVC.Lib.Class
{
    public class InsertClass
    {
        public string DbName
        { get; set; }
        public List<string> arr_conditions
        { get; set; }
        public List<string> arr_values
        { get; set; }


        public bool InsertOpr(out string str_msg)
        {
            try
            {
                string str_check = DbName;
                string str_conditions = "";
                string str_values = "";
                if (str_conditions != null)
                {
                    if (arr_conditions.Count != arr_values.Count)
                    {
                        str_msg = "Array Count Is Not Equal";
                        return false;
                    }
                    foreach (string strTmp in arr_conditions)
                    {
                        str_check += ";" + strTmp;
                        str_conditions += strTmp + ",";
                    }
                    str_conditions = "(" + str_conditions.Remove(str_conditions.Count() - 1) + ")";
                }
                foreach (string strTmp in arr_values)
                {
                    str_check += ";" + strTmp;
                    str_values += strTmp + ",";
                }
                str_values = "(" + str_values.Remove(str_values.Count() - 1) + ")";
                if (!str_check.ProcessSqlStr())
                {
                    str_msg = "Word Forbidden";
                    return false;
                }
                string str_sql = "insert into " + DbName + str_conditions + " values " + str_values;
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