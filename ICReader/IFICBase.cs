using System.Runtime.InteropServices;
using System.Text;

namespace ICReader
{
    /// <summary>
    /// ActiveX安全接口
    /// </summary>
    [Guid("3d8b7761-3fbe-449d-9fe5-bd9ad1628a1f"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IObjectSafety
    {
        void GetInterfacceSafyOptions(System.Int32 riid, out System.Int32 pdwSupportedOptions, out System.Int32 pdwEnabledOptions);
        void SetInterfaceSafetyOptions(System.Int32 riid, System.Int32 dwOptionsSetMask, System.Int32 dwEnabledOptions);
    }

    public interface IFICBase
    {
        int Open_Comm();

        int Close_Comm();

        int Authenticate_Card();

        int Get_ReadBaseInfos(StringBuilder Name, StringBuilder Gender, StringBuilder Folk, StringBuilder BirthDay, StringBuilder Code, StringBuilder Address, StringBuilder Agency, StringBuilder ExpireStart, StringBuilder ExpireEnd);
    }
}
