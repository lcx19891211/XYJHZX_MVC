using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ICReader
{
    [Guid("24F0E2B0-04F4-4885-8FFD-1D224559AFE9")]
    public class ICReader : IObjectSafety
    {
        public string ICName = "";//姓名
        public string ICNumber = "";//身份证号
        public string ICAddress = "";//注册地址
        public string ICSex = "";//性别
        public string ICFolk = "";//民族
        public string ICBirthDay = "";//出生日期
        public string ICAgency = "";//签发机关
        public string ICExpireStart = "";//有效期开始时间
        public string ICExpireEnd = "";//有效期结束时间

        public string Message = "";

        IFICBase _IFICBase = new ICBase();
        /// <summary>
        /// 读取身份证信息
        /// </summary>
        private void ICRead()
        {
            StringBuilder Name = new StringBuilder(31);
            StringBuilder Gender = new StringBuilder(3);
            StringBuilder Folk = new StringBuilder(10);
            StringBuilder BirthDay = new StringBuilder(9);
            StringBuilder Code = new StringBuilder(19);
            StringBuilder Address = new StringBuilder(71);
            StringBuilder Agency = new StringBuilder(31);
            StringBuilder ExpireStart = new StringBuilder(9);
            StringBuilder ExpireEnd = new StringBuilder(9);

            int int_OpenRet = _IFICBase.Open_Comm();
            if (int_OpenRet != 1)
            {
                Message = "阅读器连接失败!";
                return;
            }
            int int_ReadRet = _IFICBase.Authenticate_Card();
            if (int_ReadRet != 1)
            {
                Message = "身份证识别失败！";
                return;
            }
            int int_ReadBaseInfoRet = _IFICBase.Get_ReadBaseInfos(Name, Gender, Folk, BirthDay, Code, Address, Agency, ExpireStart, ExpireEnd);
            if (int_ReadBaseInfoRet != 1)
            {
                int int_CloseRet = _IFICBase.Close_Comm();
                return;
            }
            else
            {
                ICName = Name.ToString();
                ICNumber = Code.ToString();
                ICAddress = Address.ToString();
                ICSex = Gender.ToString();
                ICFolk = Folk.ToString();
                ICBirthDay = BirthDay.ToString();
                ICAgency = Agency.ToString();
                ICExpireStart = ExpireStart.ToString();
                ICExpireEnd = ExpireEnd.ToString();
                int int_CloseRet = _IFICBase.Close_Comm();
                return;
            }
        }

        /// <summary>
        /// ActiveX安全接口实现
        /// </summary>
        /// <param name="riid"></param>
        /// <param name="pdwSupportedOptions"></param>
        /// <param name="pdwEnabledOptions"></param>
        public void GetInterfacceSafyOptions(int riid, out int pdwSupportedOptions, out int pdwEnabledOptions)
        {
            pdwSupportedOptions = 1;  //不要修改该代码
            pdwEnabledOptions = 2;    //不要修改该代码
            return;
        }

        public void SetInterfaceSafetyOptions(int riid, int dwOptionsSetMask, int dwEnabledOptions)
        {
            return;
        }
    }
}
