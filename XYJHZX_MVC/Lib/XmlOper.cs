using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;

namespace XYJHZX_MVC.Lib
{
    public class XmlOper: IXmlOper
    {

        public bool GetDataForXml(out string str_msg, ref List<string> arr_result, string str_con)
        {
            try
            {
                XmlDocument _xmlDoc = new XmlDocument();//xml缓存
                arr_result = new List<string>();
                string str_xmlpath = BaseFnc.GetAssemblyPath() + "Lib/Res/";
                _xmlDoc.Load(str_xmlpath + "Config.xml");
                XmlNode _xn0 = _xmlDoc.SelectSingleNode("Servers");
                XmlNode _xnlist = _xn0.SelectSingleNode(str_con.Trim());
                str_msg = str_xmlpath;
                foreach(XmlNode _xntemp in _xnlist.ChildNodes)
                {
                    arr_result.Add(_xntemp.InnerText);
                }
                _xmlDoc = null;
                return true;
            }
            catch(Exception ex)
            {
                str_msg = ex.Message;
                return false;
            }



        }

        public bool SetDataToXml(out string str_msg,string str_pwd, string str_con)
        {
            try
            {
                XmlDocument _xmlDoc = new XmlDocument();
                string str_xmlpath = BaseFnc.GetAssemblyPath() + "\\Res\\";
                _xmlDoc.Load(str_xmlpath + "Config.xml");
                XmlNode _xn0 = _xmlDoc.SelectSingleNode("Servers");
                XmlNode _xn1 = _xn0.SelectSingleNode("Password");
                if (_xn1 == null)
                {
                    XmlElement _xe = _xmlDoc.CreateElement("Password");
                    _xe.InnerText = str_pwd;
                    _xn0.AppendChild(_xe);
                }
                else
                    _xn1.InnerText = str_pwd;
                str_msg = str_xmlpath;
                _xmlDoc.Save(str_xmlpath + "Config.xml");
                _xmlDoc = null;
                return true;
            }
            catch(Exception ex)
            {
                str_msg = ex.Message;
                return false;
            }
        }
    }
}