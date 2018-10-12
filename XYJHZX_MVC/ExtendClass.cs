using System;
using System.Text.RegularExpressions;

namespace XYJHZX_MVC
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExtendClass
    {

        /// <summary>
        /// sql字符串加引号
        /// </summary>
        /// <param name="str_value"></param>
        /// <returns></returns>
        public static string ConvertSqlCondition(this string str_input)
        {
            return "'" + str_input + "'";
        }

        /// <summary>
        /// 日期转换
        /// </summary>
        /// <param name="str_date"></param>
        /// <returns></returns>
        public static DateTime? ConvertToDate(this string str_input)
        {
            if (string.IsNullOrEmpty(str_input))
                return null;
            else
                return DateTime.Parse(str_input);
        }


        /// <summary>
        /// 是否数字类型
        /// </summary>
        /// <param name="str">检查字符串</param>
        /// <returns>返回判断</returns>
        public static bool IsNumber(this string str_input)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

            return !objNotNumberPattern.IsMatch(str_input) &&
                   !objTwoDotPattern.IsMatch(str_input) &&
                   !objTwoMinusPattern.IsMatch(str_input) &&
                   objNumberPattern.IsMatch(str_input);
        }
        
        /// <summary>
        /// sql防注入
        /// </summary>
        /// <param name="Str">检查字符串</param>
        /// <returns>返回判断</returns>
        public static bool ProcessSqlStr(this string str_input)
        {
            string SqlStr = " and | exec | insert | select | delete | update | count | * | chr | mid | master | truncate | char | declare ";
            bool ReturnValue = true;
            try
            {
                if (str_input != "")
                {
                    string[] anySqlStr = SqlStr.Split('|');
                    foreach (string ss in anySqlStr)
                    {
                        if (str_input.IndexOf(ss) >= 0)
                            ReturnValue = false;
                    }
                }
            }
            catch (Exception ex)
            {
                string str_msg = ex.Message;
                ReturnValue = false;
            }
            return ReturnValue;
        }
    }
}