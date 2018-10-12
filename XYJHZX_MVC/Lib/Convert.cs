using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Reflection;

namespace XYJHZX_MVC.Lib
{
    public class Convert<T> where T : new()
    {
        public static IList<T> ConvertToList(DataTable _dt)
        {

            IList<T> arr_result = new List<T>();// 定义返回集合
            Type _type = typeof(T); // 获得此模型的类型
            string str_tmpName = ""; //DataTable列名
            foreach (DataRow _dr in _dt.Rows)
            {
                T _toType = new T(); //转换类型
                PropertyInfo[] arr_propertys = _toType.GetType().GetProperties();// 获得此模型的公共属性
                //复制行所有属性到新行
                foreach (PropertyInfo _pi in arr_propertys)
                {
                    str_tmpName = _pi.Name;
                    if (_dt.Columns.Contains(str_tmpName))
                    {
                        if (!_pi.CanWrite) continue;
                        object obj_value = _dr[str_tmpName];
                        if (obj_value != DBNull.Value)
                            _pi.SetValue(_toType, obj_value, null);
                    }
                }
                arr_result.Add(_toType);
            }
            return arr_result;
        }


    }
}