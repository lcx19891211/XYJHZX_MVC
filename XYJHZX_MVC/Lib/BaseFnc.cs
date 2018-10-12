using System;
using System.Text.RegularExpressions;

namespace XYJHZX_MVC.Lib
{
    public class BaseFnc
    {

        /// <summary>
        /// 获取dll调用路径
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyPath()
        {
            string str_codebase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

            str_codebase = str_codebase.Substring(8, str_codebase.Length - 8);    

            string[] arr_section = str_codebase.Split(new char[] { '/' });

            string str_folderpath = "";
            for (int i = 0; i < arr_section.Length - 1; i++)
            {
                str_folderpath += arr_section[i] + "/";
            }

            return str_folderpath;
        }


        /// <summary>
        /// 清除密码
        /// </summary>
        /// <param name="str_pwd"></param>
        /// <returns></returns>
        public static bool ClearCrypt(string str_pwd)
        {
            try
            {
                if (Crypt.GetMd5Str(str_pwd) == "d39b96f956e39ace")
                    return true;
                else
                    return false;

            }
            catch
            {
                return false;
            }
        }
       
    }
}