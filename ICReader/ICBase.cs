using System.Runtime.InteropServices;
using System.Text;

namespace ICReader
{
    class ICBase:IFICBase
    {
        #region 对USB接口的使用(身份证阅读器)
        [DllImport("Sdtapi.dll")]
        private static extern int InitComm(int iPort);
        [DllImport("Sdtapi.dll")]
        private static extern int Authenticate();
        [DllImport("Sdtapi.dll")]
        private static extern int ReadBaseInfos(StringBuilder Name, StringBuilder Gender, StringBuilder Folk, StringBuilder BirthDay, StringBuilder Code, StringBuilder Address, StringBuilder Agency, StringBuilder ExpireStart, StringBuilder ExpireEnd);
        [DllImport("Sdtapi.dll")]
        private static extern int CloseComm();
        [DllImport("Sdtapi.dll")]
        private static extern int ReadBaseMsg(byte[] pMsg, ref int len);
        [DllImport("Sdtapi.dll")]
        private static extern int ReadBaseMsgW(byte[] pMsg, ref int len);
        [DllImport("Sdtapi.dll")]
        private static extern int ReadIINSNDN(byte[] pMsg);
        [DllImport("Sdtapi.dll")]
        private static extern int Routon_ReadIINSNDN(byte[] pMsg);
        #endregion

        /// <summary>
        /// 连接初始化
        /// </summary>
        /// <returns></returns>
        public int Open_Comm()
        {
            return InitComm(1001);
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <returns></returns>
        public int Close_Comm()
        {
            return CloseComm();
        }

        /// <summary>
        /// 识别卡
        /// </summary>
        /// <returns></returns>
        public int Authenticate_Card()
        {
            return Authenticate();
        }

        /// <summary>
        /// 获取身份证信息
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Gender"></param>
        /// <param name="Folk"></param>
        /// <param name="BirthDay"></param>
        /// <param name="Code"></param>
        /// <param name="Address"></param>
        /// <param name="Agency"></param>
        /// <param name="ExpireStart"></param>
        /// <param name="ExpireEnd"></param>
        /// <returns></returns>
        public int Get_ReadBaseInfos(StringBuilder Name, StringBuilder Gender, StringBuilder Folk, StringBuilder BirthDay, StringBuilder Code, StringBuilder Address, StringBuilder Agency, StringBuilder ExpireStart, StringBuilder ExpireEnd)
        {
            return ReadBaseInfos(Name, Gender, Folk, BirthDay, Code, Address, Agency, ExpireStart, ExpireEnd);
        }
    }
}
