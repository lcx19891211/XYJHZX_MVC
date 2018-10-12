using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XYJHZX_MVC.Lib.Class
{
    public class DeleteClass
    {
        public string DbName
        { get; set; }
        public List<string> arr_orgconditions
        { get; set; }
        public List<string> arr_orgvalues
        { get; set; }

        public bool DeleteOpr(out string str_msg)
        {
            try
            {
                if (arr_orgconditions.Count != arr_orgvalues.Count)
                {
                    str_msg = "Array Count Is Not Equal";
                    return false;
                }
                string str_check = DbName;
                string str_values = "";
                foreach (string strTmp in arr_orgconditions)
                {
                    str_check += ";" + strTmp;
                }
                foreach (string strTmp in arr_orgvalues)
                {
                    str_check += ";" + strTmp;
                }
                if (!str_check.ProcessSqlStr())
                {
                    str_msg = "Word Forbidden";
                    return false;
                }
                for(int i=0;i<arr_orgconditions.Count;i++)
                {
                    str_values += arr_orgconditions[i] + " = '" + arr_orgvalues[i] + "' and";
                }

                string str_sql = "delete from " + DbName + " where " + str_values.Remove(str_values.Count() - 4, 4);
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