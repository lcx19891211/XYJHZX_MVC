using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using XYJHZX_MVC.Lib.Class;

namespace XYJHZX_MVC.Lib
{
    public class DataCon : IDataCon
    {
        private string str_password = ""; //服务器连接密码
        private string str_svrpath = ""; //服务器地址
        private string str_sqlCon = "";//连接字符串
        private SQLiteConnection _sqlCon;//Sqlite连接实例
        private XmlOper _XmlConfig = new XmlOper();//Xml设置实例
        private IXmlOper IXmlConfig;//Xml设置接口

        #region 连接初始化
        /// <summary>
        /// 连接初始化
        /// </summary>
        /// <param name="str_msg">返回信息</param>
        /// <param name="str_password">数据库密码</param>
        /// <returns></returns>
        private bool ConInit(out string str_msg, string str_password)
        {
            try
            {
                IXmlConfig = _XmlConfig;//获取Xml设置
                List<string> arr_strConfig = new List<string>();

                if (IXmlConfig.GetDataForXml(out str_svrpath, ref arr_strConfig, "Connect1"))
                {
                    if (arr_strConfig[0].ToUpper().Trim() == "SQLITE")
                    {
                        string str_con = "Data Source=" + str_svrpath + arr_strConfig[1] + ";Version=3;";
                        if (!string.IsNullOrEmpty(str_password))
                        {
                            str_con += "Password = " + str_password + "; ";
                            str_password = null;
                        }
                        str_sqlCon = Crypt.Encrypt(str_con);
                        
                        _sqlCon = new SQLiteConnection(str_con);
                        str_msg = str_con;
                        str_con = null;
                        return true;
                    }
                    else
                    {
                        str_msg = "SqlType Is Missing";
                        return false;
                    }
                }
                else
                {
                    str_msg = str_svrpath;
                    return false;
                }
            }
            catch (Exception ex)
            {
                str_msg = ex.Message;
                return false;
            }
        }
        public bool ConInit(out string str_msg)
        {
            try
            {
                IXmlConfig = _XmlConfig;
                List<string> arr_strConfig = new List<string>();

                if (IXmlConfig.GetDataForXml(out str_svrpath, ref arr_strConfig, "Connect1"))
                {
                    if (arr_strConfig[0].ToUpper().Trim() == "SQLITE")
                    {
                        string str_con = "Data Source=" + str_svrpath + arr_strConfig[1] + ";";
                        if (arr_strConfig.Count > 3 && !string.IsNullOrEmpty(arr_strConfig[3]))
                        {
                            str_password = Crypt.StringToMD5Hash(Crypt.Decrypt(arr_strConfig[3]));
                            str_con += "Password = " + str_password + "; ";
                            str_password = null;
                        }
                        str_sqlCon = Crypt.Encrypt(str_con);
                        _sqlCon = new SQLiteConnection(str_con);
                        str_msg = str_con;
                        str_con = null;
                        return true;
                    }
                    else
                    {
                        str_msg = "SqlType Is Missing";
                        return false;
                    }
                }
                else
                {
                    str_msg = str_svrpath;
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

        #region Sqlite数据库设置
        /// <summary>
        /// 修改数据库密码
        /// </summary>
        /// <param name="str_msg">返回信息</param>
        /// <param name="str_orgpassword">原始密码</param>
        /// <param name="str_newpassword">新密码</param>
        /// <returns></returns>
        public bool SetDBPwd(out string str_msg, string str_orgpassword, string str_newpassword)
        {
            try
            {
                _sqlCon.Open();
                str_msg = "";
                if (ConInit(out str_msg, str_orgpassword))
                {
                    string str_cryptpwd = "";
                    if (!string.IsNullOrEmpty(str_newpassword))
                    {

                        IXmlConfig.SetDataToXml(out str_msg, Crypt.Encrypt(str_newpassword), "Connect1");
                        str_cryptpwd = Crypt.StringToMD5Hash(str_newpassword);
                        str_newpassword = null;
                    }
                    else
                        IXmlConfig.SetDataToXml(out str_msg, string.Empty, "Connect1");
                    _sqlCon.ChangePassword(str_cryptpwd);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                str_msg = ex.Message;
                return false;
            }
            finally
            {
                _sqlCon.Close();
            }
        }
        #endregion

        #region ======select======
        /// <summary>
        /// 查询分组
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <returns></returns>
        public bool SelGroup(out string str_msg, ref DataSet _ResultData)
        {
            string str_sql = "select * from t_bse_Group where status = 1 order by SeqID ";
            _ResultData = new DataSet();
            return SelectOpr(out str_msg, str_sql, ref _ResultData);
        }
        /// <summary>
        /// 查询分区
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <returns></returns>
        public bool SelTeam(out string str_msg, ref DataSet _ResultData)
        {
            string str_sql = "select * from t_bse_Team where status = 1 order by SeqID ";
            _ResultData = new DataSet();
            return SelectOpr(out str_msg, str_sql, ref _ResultData);
        }
        /// <summary>
        /// 查询机位
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <returns></returns>
        public bool SelMachine(out string str_msg, ref DataSet _ResultData)
        {
            string str_sql = "select * from t_bse_Machine where status = 1 order by SeqID ";
            _ResultData = new DataSet();
            return SelectOpr(out str_msg, str_sql, ref _ResultData);
        }
        /// <summary>
        /// 关联分组编号查询分区
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <param name="str_GroupId"></param>
        /// <returns></returns>
        public bool SelGroupTeam(out string str_msg,ref DataSet _ResultData,string str_GroupId)
        {
            if (str_GroupId.ProcessSqlStr())
            {
                string str_sql = @"select a.GroupId,c.TeamId,c.TeamName,c.Desciption,c.Seqid
                                   from t_bse_Group a 
                             inner join t_bse_TeamWithGroup b
                                     on a.Groupid = b.Groupid
                             inner join t_bse_team c
                                     on b.teamid = c.teamid
                                    and c.status = 1 
                                  where a.status = 1 and a.GroupId = " + str_GroupId +
                              " order by c.SeqID ";
                _ResultData = new DataSet();
                return SelectOpr(out str_msg, str_sql, ref _ResultData);
            }
            else
            {
                str_msg = "Word Forbidden";
                return false;
            }
        }
        /// <summary>
        /// 关联分区编号查询机位
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <param name="str_TeamId"></param>
        /// <returns></returns>
        public bool SelTeamMachine(out string str_msg, ref DataSet _ResultData, string str_TeamId)
        {
            if (str_TeamId.ProcessSqlStr())
            {
                string str_sql = @"select a.TeamId,c.MacId,c.MacName,c.Desciption,c.Seqid
                                   from t_bse_Team a 
                             inner join t_bse_MachineWithTeam b
                                     on a.teamid = b.teamid
                             inner join t_bse_Machine c
                                     on b.macid = c.macid
                                    and c.status = 1 
                                  where a.status = 1 and a.TeamId = " + str_TeamId +
                              " order by c.SeqID ";
                _ResultData = new DataSet();
                return SelectOpr(out str_msg, str_sql, ref _ResultData);
            }
            else
            {
                str_msg = "Word Forbidden";
                return false;
            }
        }

        /// <summary>
        /// 查询检查
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public bool SelSchedul(out string str_msg,ref DataSet _ResultData,string[] condition)
        {
            string str_sql = string.Format(@"select t.Groupname          groupname,
                                                    t.groupdescption     groupdescption,
                                                    t.teamname           teamname,
                                                    t.teamdescption      teamdescption,
                                                    t.macname            macname,
                                                    t.macdescption       macdescption,
                                                    a.patWeightBefore    patWeightBefore,
                                                    a.patWeightAfter     patWeightAfter,
                                                    a.SchedulDate        SchedulDate,
                                                    a.SchedulTime        SchedulTime,
                                                    a.dialyzerName       dialyzerName,
                                                    a.routeName          routeName,
                                                    a.anticoagulantName  anticoagulantName,
                                                    a.remark             remark,
                                                    a.signinDate         signinDate,
                                                    a.SignInSeq          SignInSeq,
                                                    b.patName            patName,
                                                    b.patIdCardNo        patIdCardNo 
                                                    from v_groupdetail t 
                                                    left join t_pro_schedulMain a 
                                                    on t.macid = a.macid
                                                    and a.SchedulDate = '{0}'
                                                    and a.SchedulTime = '{1}'
                                                    and a.status = 1
                                                    left join t_pro_patInformation b 
                                                    on a.patid = b.patId
                                                    where t.groupid = {2}
                                                    order by t.groupseq,t.teamseq,t.macseq", condition[0], condition[1], condition[2]);
            return SelectOpr(out str_msg, str_sql, ref _ResultData);
        }

        /// <summary>
        /// 查询登记项目可选项
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <returns></returns>
        public bool SelSchedulColumnDetail(out string str_msg, out DataSet _ResultData, string str_colMainId)
        {
            string str_sql = "select * from t_bse_SchedulColumnDetail where status = 1";
            if(!string.IsNullOrEmpty(str_colMainId))
            {
                string str_condition = string.Format(" and colMainId = {0} ", str_colMainId.Trim());
                str_sql += str_condition;
            }
            _ResultData = new DataSet();
            return SelectOpr(out str_msg, str_sql, ref _ResultData);
        }
        /// <summary>
        /// 查询所有登记项目名称
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <returns></returns>
        public bool SelSchedulColumnMain(out string str_msg, out DataSet _ResultData)
        {
            string str_sql = "select * from t_bse_SchedulColumnMain where status = 1";
            _ResultData = new DataSet();
            return SelectOpr(out str_msg, str_sql, ref _ResultData);
        }
        /// <summary>
        /// 查询所有可选序列登记项目名称
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <returns></returns>
        public bool SelSchedulColumnMainWithList(out string str_msg, out DataSet _ResultData)
        {
            string str_sql = "select * from t_bse_SchedulColumnMain where desciption = 'List' and status = 1";
            _ResultData = new DataSet();
            return SelectOpr(out str_msg, str_sql, ref _ResultData);
        }
        /// <summary>
        /// 查询病人信息
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <returns></returns>
        public bool SelPatInformation(out string str_msg, out DataSet _ResultData, string str_condition)
        {
            string str_sql = "select * from t_pro_PatInformation where status = 1 and isRead = 0 ";
            if (!string.IsNullOrEmpty(str_condition) && str_condition.ProcessSqlStr())
            {
                str_condition = string.Format(" and (patName like '%{0}%' or patIdCardNo like '%{0}%' or patOutCardNo like '%{0}%' )", str_condition);
                str_sql += str_condition;
            }
            str_sql += " order by patName ";
            _ResultData = new DataSet();
            return SelectOpr(out str_msg, str_sql, ref _ResultData);
        }
        /// <summary>
        /// 检查病人是否存在
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <param name="str_condition"></param>
        /// <returns></returns>
        public bool CheckPatIsRead(out string str_msg, string[] arr_condition)
        {
            DataSet _ResultData = new DataSet();
            string str_sql = "select * from t_pro_PatInformation where status = 1 and isRead = 0 ";
            if (!string.IsNullOrEmpty(arr_condition[0] + arr_condition[1]) && arr_condition[0].ProcessSqlStr() && arr_condition[1].ProcessSqlStr())
            {
                string str_condition = string.Format(" and (patName = '{0}' or patIdCardNo = '{1}')", arr_condition[0], arr_condition[1]);
                str_sql += str_condition;
            }
            else
            {
                str_msg = "条件不能为空！";
                return false;
            }
            _ResultData = new DataSet();
            if (SelectOpr(out str_msg, str_sql, ref _ResultData) && _ResultData.Tables[0].Rows.Count <= 0)
                return true;
            else
            {
                str_msg = "病人已存在！";
                return false;
            }
        }
        /// <summary>
        /// 查询字段类型
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <returns></returns>
        public bool SelSchedulColumnType(out string str_msg, out DataSet _ResultData)
        {
            string str_sql = "select * from t_bse_SchedulColumnType";
            _ResultData = new DataSet();
            return SelectOpr(out str_msg, str_sql, ref _ResultData);
        }
        /// <summary>
        /// 查询时间段
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <returns></returns>
        public bool SelDateTimeSplit(out string str_msg, out DataSet _ResultData)
        {
            string str_sql = "select * from t_bse_DateTimeSplit Where Status = 1 order by SeqId";
            _ResultData = new DataSet();
            return SelectOpr(out str_msg, str_sql, ref _ResultData);
        }

        /// <summary>
        /// 返回特定时间段
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <returns></returns>
        public bool SelGetDateTimeSplit(out string str_msg, out DataSet _ResultData, string str_Time)
        {
            string str_sql = string.Format(@"select SplitName from t_bse_DateTimeSplit Where Status = 1 And BegintTime < strftime('%H:%M:%S','{0}') And EndTime >= strftime('%H:%M:%S','{0}')  order by SeqId", str_Time);
            _ResultData = new DataSet();
            return SelectOpr(out str_msg, str_sql, ref _ResultData);
        }

        /// <summary>
        /// 查询分组明细
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <returns></returns>
        public bool SelGroupDetail(out string str_msg, out DataSet _ResultData)
        {
            string str_sql = "select * from V_GroupDetail";
            _ResultData = new DataSet();
            return SelectOpr(out str_msg, str_sql, ref _ResultData);
        }

        /// <summary>
        /// 查询项目明细
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <returns></returns>
        public bool SelSchedulColumn(out string str_msg, out DataSet _ResultData, int int_type)
        {
            string str_sql = "select * from V_SchedulColumn";
            if(int_type != 0)
            {
                str_sql += " where colType = " + int_type;
            }
            _ResultData = new DataSet();
            return SelectOpr(out str_msg, str_sql, ref _ResultData);
        }

        /// <summary>
        /// 查询检查安排
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <param name="arr_condition"></param>
        /// <returns></returns>
        public bool SelViewSchedulMain(out string str_msg, out DataSet _ResultData, string[] arr_condition)
        {
            string str_sql = "select * from V_SchedulMain";
            if (!string.IsNullOrEmpty(arr_condition[0] + arr_condition[1]) && arr_condition[0].ProcessSqlStr() && arr_condition[1].ProcessSqlStr())
            {
                string str_condition = string.Format(" Where (Groupid = {0} and SchedulDate = '{1}' and SchedulTime = '{2}')", arr_condition[0], arr_condition[1], arr_condition[2]);
                str_sql += str_condition;
            }
            _ResultData = new DataSet();
            return SelectOpr(out str_msg, str_sql, ref _ResultData);
        }
        /// <summary>
        /// 查询当前时间检查
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <param name="arr_condition"></param>
        /// <returns></returns>
        public bool SelViewCurrentSchedul(out string str_msg, out DataSet _ResultData, string[] arr_condition)
        {
            string str_sql = "select * from V_CurrentSchedul";
            if (arr_condition.Count() > 0 && !string.IsNullOrEmpty(arr_condition[0]) && arr_condition[0].ProcessSqlStr())
            {
                string str_condition = string.Format(" Where Groupid = {0} ", arr_condition[0]);
                str_sql += str_condition;
                if(arr_condition.Count() > 1 && !string.IsNullOrEmpty(arr_condition[1]) && arr_condition[1].ProcessSqlStr())
                {
                    str_condition = string.Format(" And teamid = {0} ", arr_condition[1]);
                    str_sql += str_condition;
                }
            }
            _ResultData = new DataSet();
            return SelectOpr(out str_msg, str_sql, ref _ResultData);
        }

        /// <summary>
        /// 根据身份证号获取检查主键
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <param name="arr_condition"></param>
        /// <returns></returns>
        public bool SelMainIDCurrentSchedulForIDCard(out string str_msg, out DataSet _ResultData, string[] arr_condition)
        {
            string str_sql = "select mainid,teamid from V_CurrentSchedul";
            if (arr_condition.Count() > 0 && !string.IsNullOrEmpty(arr_condition[0]) && arr_condition[0].ProcessSqlStr() && !string.IsNullOrEmpty(arr_condition[1]) && arr_condition[1].ProcessSqlStr())
            {
                string str_condition = string.Format(" Where signinseq is null and patIdCardNo = {0} and groupid = {1}", arr_condition[0], arr_condition[1]);
                str_sql += str_condition;
            }
            _ResultData = new DataSet();
            return SelectOpr(out str_msg, str_sql, ref _ResultData);
        }
        /// <summary>
        /// 获取分区最大签到值
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <param name="arr_condition"></param>
        /// <returns></returns>
        public bool SelMaxCurrentSeq(out string str_msg,out DataSet _ResultData,string[] arr_condition)
        {
            string str_sql = "select max(signinseq) seq from V_CurrentSchedul";
            if (arr_condition.Count() > 0 && !string.IsNullOrEmpty(arr_condition[0]) && arr_condition[0].ProcessSqlStr())
            {
                string str_condition = string.Format(" Where teamid = {0} ", arr_condition[0]);
                str_sql += str_condition;
            }
            _ResultData = new DataSet();
            return SelectOpr(out str_msg, str_sql, ref _ResultData);
        }
        #endregion

        #region======update======
        /// <summary>
        /// 更新分组
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr_values"></param>
        /// <param name="str_orgid"></param>
        /// <returns></returns>
        public bool UpdateGroup(out string str_msg, List<string[]> arr2_values, string[] str_orgid)
        {
            string[] arr_condition = { "groupname", "desciption", "SeqID" };
            return UpdateBseDir(out str_msg, arr2_values, str_orgid, "t_bse_Group", arr_condition, "groupid");
        }
        /// <summary>
        /// 更新机位
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr_values"></param>
        /// <param name="str_orgid"></param>
        /// <returns></returns>
        public bool UpdateMachine(out string str_msg, List<string[]> arr2_values, string[] str_orgid)
        {
            string[] arr_condition = { "macname", "desciption", "SeqID" };
            return UpdateBseDir(out str_msg, arr2_values, str_orgid, "t_bse_Machine", arr_condition, "macid");
        }
        /// <summary>
        /// 更新分区
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr_values"></param>
        /// <param name="str_orgid"></param>
        /// <returns></returns>
        public bool UpdateTeam(out string str_msg, List<string[]> arr2_values, string[] str_orgid)
        {
            string[] arr_condition = { "teamname", "desciption", "SeqID" };
            return UpdateBseDir(out str_msg, arr2_values, str_orgid, "t_bse_Team", arr_condition, "teamid");
        }


        public bool UpdatePatInformation(out string str_msg, List<string[]> arr2_values, string[] str_orgid)
        {
            string[] arr_condition = { "patName", "patSex", "patBrithday", "patAge", "patIdCardNo", "sendDeptId", "sendDeptName", "patOutCardNo", "TelphoneNo", "paymentDate", "remark" };
            return UpdateBseDir(out str_msg, arr2_values, str_orgid, "t_pro_PatInformation", arr_condition, "patId");
        }

        /// <summary>
        /// 更新登记项目
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr_values"></param>
        /// <param name="str_orgid"></param>
        /// <returns></returns>
        public bool UpdateSchedulColumnMain(out string str_msg, List<string[]> arr2_values, string[] str_orgid)
        {
            string[] arr_condition = { "colName", "desciption", "coltype" };
            return UpdateBseDir(out str_msg, arr2_values, str_orgid, "t_bse_SchedulColumnMain", arr_condition, "colMainId");
        }

        /// <summary>
        /// 更新登记项目可选项
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr_values"></param>
        /// <param name="str_orgid"></param>
        /// <returns></returns>
        public bool UpdateSchedulColumnDetail(out string str_msg, List<string[]> arr2_values, string[] str_orgid)
        {
            string[] arr_condition = { "colDetailName", "desciption" };
            return UpdateBseDir(out str_msg, arr2_values, str_orgid, "t_bse_SchedulColumnDetail", arr_condition, "colDetailId");
        }

        /// <summary>
        /// 更新检查操作项目
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <param name="str_orgid"></param>
        /// <returns></returns>
        public bool UpdateSchedulDetail(out string str_msg, List<string[]> arr2_values, string[] str_orgid)
        { 
            string[] arr_condition = { "mainid", "maccolid", "maccolval" };
            return UpdateBseDir(out str_msg, arr2_values, str_orgid, "t_pro_SchedulDetail", arr_condition, "detailid");
        }
        /// <summary>
        /// 更新检查主索
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <param name="str_orgid"></param>
        /// <returns></returns>
        public bool UpdateSchedulMain(out string str_msg, List<string[]> arr2_values, string[] str_orgid)
        {
            string[] arr_condition = { "SchedulDate", "SchedulTime", "patid",  "teamid", "groupid", "macid", "dialyzerName", "routeName", "anticoagulantName", "remark", "lastDate"};
            return UpdateBseDir(out str_msg, arr2_values, str_orgid, "t_pro_SchedulMain", arr_condition, "mainid");
        }
        /// <summary>
        /// 检查病人是否存在
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <param name="str_condition"></param>
        /// <returns></returns>
        public bool UpadatePatIsRead(out string str_msg, string[] arr_patId)
        {
            string[] arr_condition = { "isRead" };
            List<string[]> arr2_values = new List<string[]>();
            for (int i = 0; i < arr_patId.Count(); i++)
            {
                arr2_values.Add(new string[] { "1" });
            }
            return UpdateBseDir(out str_msg, arr2_values, arr_patId, "t_pro_PatInformation", arr_condition, "patId");
        }
        /// <summary>
        /// 更新检查签到时间
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <param name="str_orgid"></param>
        /// <returns></returns>
        public bool UpdateSchedulSigninDate(out string str_msg, List<string[]> arr2_values, string[] str_orgid)
        {
            string[] arr_condition = { "signinDate" , "signinseq" };
            return UpdateBseDir(out str_msg, arr2_values, str_orgid, "t_pro_SchedulMain", arr_condition, "mainid");
        }
        /// <summary>
        /// 更新体重
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <param name="str_orgid"></param>
        /// <returns></returns>
        public bool UpdateSchedulPatWeight(out string str_msg, List<string[]> arr2_values, string[] str_orgid)
        {
            string[] arr_condition = { "patWeightBefore", "patWeightAfter" };
            return UpdateBseDir(out str_msg, arr2_values, str_orgid, "t_pro_SchedulMain", arr_condition, "mainid");
        }

        /// <summary>
        /// 删除机位
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <param name="str_orgid"></param>
        /// <returns></returns>
        public bool DropMachine(out string str_msg, string str_status, string[] str_orgid)
        {
            string[] arr_condition = { "status" };
            string[] arr_values = { str_status };
            List<string[]> arr2_values = new List<string[]>();
            for (int i = 0; i < str_orgid.Count(); i++)
                arr2_values.Add(arr_values);
            return UpdateBseDir(out str_msg, arr2_values, str_orgid, "t_bse_Machine", arr_condition, "macid");
        }
        /// <summary>
        /// 删除分区
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <param name="str_orgid"></param>
        /// <returns></returns>
        public bool DropTeam(out string str_msg, string str_status, string[] str_orgid)
        {
            string[] arr_condition = { "status" };
            string[] arr_values = { str_status };
            List<string[]> arr2_values = new List<string[]>();
            for (int i = 0; i < str_orgid.Count(); i++)
                arr2_values.Add(arr_values);
            return UpdateBseDir(out str_msg, arr2_values, str_orgid, "t_bse_Team", arr_condition, "teamid");
        }
        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <param name="str_orgid"></param>
        /// <returns></returns>
        public bool DropGroup(out string str_msg, string str_status, string[] str_orgid)
        {
            string[] arr_condition = { "status" };
            string[] arr_values = { str_status };
            List<string[]> arr2_values = new List<string[]>();
            for (int i = 0; i < str_orgid.Count(); i++)
                arr2_values.Add(arr_values);
            return UpdateBseDir(out str_msg, arr2_values.ToList(), str_orgid, "t_bse_Group", arr_condition, "Groupid");
        }
        /// <summary>
        /// 删除登记项目可选项
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <param name="str_orgid"></param>
        /// <returns></returns>
        public bool DropSchedulColumnDetail(out string str_msg, string str_status, string[] str_orgid)
        {
            string[] arr_condition = { "status" };
            string[] arr_values = { str_status };
            List<string[]> arr2_values = new List<string[]>();
            for (int i = 0; i < str_orgid.Count(); i++)
                arr2_values.Add(arr_values);
            return UpdateBseDir(out str_msg, arr2_values, str_orgid, "t_bse_SchedulColumnDetail", arr_condition, "colDetailId");
        }
        /// <summary>
        /// 删除登记项目
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <param name="str_orgid"></param>
        /// <returns></returns>
        public bool DropSchedulColumnMain(out string str_msg, string str_status, string[] str_orgid)
        {
            string[] arr_condition = { "status" };
            string[] arr_values = { str_status };
            List<string[]> arr2_values = new List<string[]>();
            for (int i = 0; i < str_orgid.Count(); i++)
                arr2_values.Add(arr_values);
            return UpdateBseDir(out str_msg, arr2_values, str_orgid, "t_bse_SchedulColumnMain", arr_condition, "colMainId");
        }
        /// <summary>
        /// 删除病人信息
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <param name="str_orgid"></param>
        /// <returns></returns>
        public bool DropPatInformation(out string str_msg, List<string[]> arr2_values, string[] str_orgid)
        {
            string[] arr_condition = { "status" };
            return UpdateBseDir(out str_msg, arr2_values, str_orgid, "t_pro_PatInformation", arr_condition, "patId");
        }
        /// <summary>
        /// 删除检查主索
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <param name="str_orgid"></param>
        /// <returns></returns>
        public bool DropSchedulMain(out string str_msg, List<string[]> arr2_values, string[] str_orgid)
        {
            string[] arr_condition = { "status" };
            return UpdateBseDir(out str_msg, arr2_values, str_orgid, "t_pro_SchedulMain", arr_condition, "mainid");
        }


        /// <summary>
        /// 批量执行更新Sql
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <param name="arr_orgid"></param>
        /// <param name="str_DbName"></param>
        /// <param name="str_conditionName"></param>
        /// <param name="str_orgconditionname"></param>
        /// <returns></returns>
        private bool UpdateBseDir(out string str_msg, List<string[]> arr2_values, string[] arr_orgid, string str_DbName, string[] arr_conditionName, string str_orgconditionname)
        {
            try
            {
                if (arr2_values[0].Length != arr_conditionName.Length && arr2_values.Count() != arr_orgid.Count())
                {
                    str_msg = " Array Count Is Not equal";
                    return false;
                }
                List<string> arr_strSql = new List<string>();
                for (int i = 0; i < arr2_values.Count; i++)
                {

                    UpdateClass _updateValues = new UpdateClass();
                    _updateValues.DbName = str_DbName;
                    string[] arr_orgcondition = { str_orgconditionname };
                    string[] arr_orgvalues = { arr_orgid[i] };
                    _updateValues.arr_orgConditions = arr_orgcondition.ToList();
                    _updateValues.arr_conditions = arr_conditionName.ToList();
                    _updateValues.arr_orgValues = arr_orgvalues.ToList();
                    _updateValues.arr_values = arr2_values[i].ToList();
                    if (_updateValues.UpdateOpr(out str_msg))
                        arr_strSql.Add(str_msg);
                    else
                        return false;
                }
                return ExecOpr(out str_msg, arr_strSql);
            }
            catch (Exception ex)
            {
                str_msg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 获取透析缴费病人（未实现）
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="_ResultData"></param>
        /// <returns></returns>
        public bool SelPayPatient(out string str_msg, out DataSet _ResultData)
        {
            str_msg = "";
            _ResultData = null;
            return false;
        }
        #endregion

        #region ======insert======
        /// <summary>
        /// 插入分组
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <returns></returns>
        public bool InsertGroup(out string str_msg, List<string[]> arr2_values)
        {
            string[] arr_condition = { "Groupname", "desciption", "seqid" };
            return InsertBseDir(out str_msg, arr2_values, "t_bse_Group", arr_condition.ToList());
        }
        /// <summary>
        /// 插入机位
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <returns></returns>
        public bool InsertMachine(out string str_msg, List<string[]> arr2_values, ref List<string> str_keyId)
        {
            string[] arr_condition = { "macname", "desciption", "seqid" };
            return InsertBseDir(out str_msg, arr2_values, "t_bse_Machine", arr_condition.ToList(), ref str_keyId);
        }
        /// <summary>
        /// 插入分组
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <returns></returns>
        public bool InsertTeam(out string str_msg, List<string[]> arr2_values, ref List<string> str_keyId)
        {
            string[] arr_condition = { "teamname", "desciption", "seqid" };
            return InsertBseDir(out str_msg, arr2_values, "t_bse_Team", arr_condition.ToList(), ref str_keyId);
        }
        /// <summary>
        /// 插入登记项目名称
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <returns></returns>
        public bool InsertSchedulColumnMain(out string str_msg, List<string[]> arr2_values)
        {
            string[] arr_condition = { "colName", "desciption", "coltype" };
            return InsertBseDir(out str_msg, arr2_values, "t_bse_SchedulColumnMain", arr_condition.ToList());
        }
        /// <summary>
        /// 插入登记项目可选项
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <returns></returns>
        public bool InsertSchedulColumnDetail(out string str_msg, List<string[]> arr2_values)
        {
            string[] arr_condition = { "colMainId", "colDetailName", "desciption" };
            return InsertBseDir(out str_msg, arr2_values, "t_bse_SchedulColumnDetail", arr_condition.ToList());
        }
        /// <summary>
        /// 插入机位所在分区
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <returns></returns>
        public bool InsertMashineWithTeam(out string str_msg, List<string[]> arr2_values)
        {
            string[] arr_condition = { "macid", "teamid" };
            return InsertBseDir(out str_msg, arr2_values, "t_bse_MachineWithTeam", arr_condition.ToList());
        }
        /// <summary>
        /// 插入分区所在组
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <returns></returns>
        public bool InsertTeamWithGroup(out string str_msg, List<string[]> arr2_values)
        {
            string[] arr_condition = { "teamid", "groupid" };
            return InsertBseDir(out str_msg, arr2_values, "t_bse_TeamWithGroup", arr_condition.ToList());
        }
        /// <summary>
        /// 插入检查项目明细
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <returns></returns>
        public bool InsertSchedulDetail(out string str_msg,List<string[]>arr2_values)
        {
            string[] arr_condition = { "mainid", "maccolid", "maccolval" };
            return InsertBseDir(out str_msg, arr2_values, "t_pro_SchedulDetail", arr_condition.ToList());
        }
        /// <summary>
        /// 插入检查主索
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <returns></returns>
        public bool InsertSchedulMain(out string str_msg, List<string[]> arr2_values)
        {
            string[] arr_condition = { "SchedulDate", "SchedulTime", "patid", "teamid", "groupid", "macid", "dialyzerName", "routeName", "anticoagulantName", "remark", "lastDate" };
            return InsertBseDir(out str_msg, arr2_values, "t_pro_SchedulMain", arr_condition.ToList());
        }
        /// <summary>
        /// 插入缴费病人信息
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <returns></returns>
        public bool InsertPatInformation(out string str_msg, List<string[]> arr2_values)
        {
            string[] arr_condition = { "patName", "patSex", "patBrithday", "patAge", "patIdCardNo", "sendDeptId", "sendDeptName", "patOutCardNo", "TelphoneNo", "paymentDate", "remark" };
            return InsertBseDir(out str_msg, arr2_values, "t_pro_PatInformation", arr_condition.ToList());
        }
        
        /// <summary>
        /// 批量执行插入Sql
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr2_values"></param>
        /// <param name="str_DbName"></param>
        /// <param name="arr_conditionName"></param>
        /// <returns></returns>
        private bool InsertBseDir(out string str_msg, List<string[]> arr2_values, string str_DbName, List<string> arr_conditionName)
        {
            try
            {
                List<string> arr_strSql = new List<string>();
                foreach (string[] arr_Tmp in arr2_values)
                {
                    InsertClass _insertValues = new InsertClass();
                    _insertValues.DbName = str_DbName;
                    _insertValues.arr_conditions = arr_conditionName;
                    _insertValues.arr_values = arr_Tmp.ToList();
                    if (_insertValues.InsertOpr(out str_msg))
                    {
                        arr_strSql.Add(str_msg);
                    }
                    else
                    {
                        return false;
                    }
                }
                return ExecOpr(out str_msg, arr_strSql);

            }
            catch (Exception ex)
            {
                str_msg = ex.Message;
                return false;
            }
        }

        private bool InsertBseDir(out string str_msg,List<string[]> arr2_values,string str_DbName, List<string> arr_conditionName,ref List<string> str_keyid)
        {
            try
            {
                foreach (string[] arr_Tmp in arr2_values)
                {
                    InsertClass _insertValues = new InsertClass();
                    _insertValues.DbName = str_DbName;
                    _insertValues.arr_conditions = arr_conditionName;
                    _insertValues.arr_values = arr_Tmp.ToList();
                    if (_insertValues.InsertOpr(out str_msg))
                    {
                        try
                        {
                            _sqlCon.Open();
                            DataSet _DataTmp = new DataSet();
                            string str_cmd = str_msg + ";";
                            str_cmd += "SELECT Seq from SQLITE_SEQUENCE where name = '" + str_DbName + "';";
                            SQLiteDataAdapter _adp = new SQLiteDataAdapter(str_cmd, _sqlCon);
                            str_msg = _adp.Fill(_DataTmp).ToString();
                            if (_DataTmp != null && _DataTmp.Tables.Count > 0)
                            {
                                str_keyid.Add(_DataTmp.Tables[0].Rows[0][0] + "");
                            }
                            str_cmd = null;
                        }
                        catch (Exception ex)
                        {
                            str_msg = ex.Message;
                            return false;
                        }
                        finally
                        {
                            _sqlCon.Close();
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                str_msg = str_keyid.Count().ToString() ;
                return true;
            }
            catch (Exception ex)
            {
                str_msg = ex.Message;
                return false;
            }
        }

        #endregion

        #region======delete======
        /// <summary>
        /// 删除机位所在分区
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr_pkid"></param>
        /// <returns></returns>
        public bool DeleteMachineWithTeam(out string str_msg, string[] arr_pkid)
        {
            DeleteClass _deleteValues = new DeleteClass();
            List<string> arr_strSql = new List<string>();
            foreach (string str_tmp in arr_pkid)
            {
                string[] arr_orgvalues = { str_tmp };
                string[] arr_orgconditions = { "Macid" };

                _deleteValues.DbName = "t_bse_MachineWithTeam";
                _deleteValues.arr_orgvalues = arr_orgvalues.ToList();
                _deleteValues.arr_orgconditions = arr_orgconditions.ToList();
                if (_deleteValues.DeleteOpr(out str_msg))
                    arr_strSql.Add(str_msg);
                else
                    return false;
            }
            return ExecOpr(out str_msg, arr_strSql);
        }
        /// <summary>
        /// 删除分区所在分组
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr_pkid"></param>
        /// <returns></returns>
        public bool DeleteTeamWithGroup(out string str_msg, string[] arr_pkid)
        {
            DeleteClass _deleteValues = new DeleteClass();
            List<string> arr_strSql = new List<string>();
            foreach (string str_tmp in arr_pkid)
            {
                string[] arr_orgvalues = { str_tmp };
                string[] arr_orgconditions = { "teamid" };

                _deleteValues.DbName = "t_bse_TeamWithGroup";
                _deleteValues.arr_orgvalues = arr_orgvalues.ToList();
                _deleteValues.arr_orgconditions = arr_orgconditions.ToList();
                if (_deleteValues.DeleteOpr(out str_msg))
                    arr_strSql.Add(str_msg);
                else
                    return false;
            }
            return ExecOpr(out str_msg, arr_strSql);
        }
        #endregion

        #region sql原子操作

        /// <summary>
        /// 单条查询操作
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="str_sql"></param>
        /// <param name="_resultData"></param>
        /// <returns></returns>
        private bool SelectOpr(out string str_msg, string str_sql, ref DataSet _resultData)
        {
            try
            {
                _sqlCon.Open();
                SQLiteDataAdapter _adp = new SQLiteDataAdapter(str_sql, _sqlCon);
                str_msg = _adp.Fill(_resultData).ToString();
                str_sql = null;
                return true;
            }
            catch (Exception ex)
            {
                str_msg = ex.Message;
                return false;
            }
            finally
            {
                _sqlCon.Close();
            }
        }
        /// <summary>
        /// Sql执行
        /// </summary>
        /// <param name="str_msg"></param>
        /// <param name="arr_strSql"></param>
        /// <returns></returns>
        private bool ExecOpr(out string str_msg, List<string> arr_strSql)
        {
            try
            {
                _sqlCon.Open();
                SQLiteCommand _sqlCmd = _sqlCon.CreateCommand();
                string str_cmd = "";
                for (int i = 0; i < arr_strSql.Count; i++)
                {
                    str_cmd += arr_strSql[i] + ";";
                }
                _sqlCmd.CommandText = str_cmd;
                str_msg = _sqlCmd.ExecuteNonQuery().ToString();
                arr_strSql = null;
                return true;
            }
            catch (Exception ex)
            {
                str_msg = ex.Message;
                return false;
            }
            finally
            {
                _sqlCon.Close();
            }
        }

        #endregion
    }

}