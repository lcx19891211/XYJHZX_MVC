using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace XYJHZX_MVC.Lib
{
    interface IXmlOper
    {
        bool GetDataForXml(out string str_msg, ref List<string> str_result, string str_con);

        bool SetDataToXml(out string str_msg, string str_pwd, string str_con);
    }
}
