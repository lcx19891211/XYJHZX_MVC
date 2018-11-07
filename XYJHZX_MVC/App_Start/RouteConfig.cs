using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace XYJHZX_MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);


            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");
            routes.IgnoreRoute("{resource}.html/{*pathInfo}");
            ///检查排队路由
            routes.MapRoute(
                name: "SchedulRoute", // 路由名称
                url: "{controller}/{action}/{id}", // 带有参数的 URL
                defaults: new { controller = "Schedul", action = "SchedulConfig", id = 1 } // 参数默认值
            );
            ///病人路由
            routes.MapRoute(
                name: "PatientRoute", // 路由名称
                url: "{controller}/{action}/{Patientid}", // 带有参数的 URL
                defaults: new { controller = "Patient", action = "PatientManage", Patientid = 1 } // 参数默认值
            );
            ///当前检查排队路由
            routes.MapRoute(
                name: "CurrentSchedulRoute", // 路由名称
                url: "{controller}/{action}/{groupid}/{teamid}/", // 带有参数的 URL
                defaults: new { controller = "CurrentSchedul", action = "ViewForTeam", groupid = 1, teamid = "x" } // 参数默认值
            );
            ///检查项目路由
            routes.MapRoute(
                name: "SchedulItemRoute", // 路由名称
                url: "{controller}/{action}/", // 带有参数的 URL
                defaults: new { controller = "SchedulItem", action = "Config" } // 参数默认值
            );
            ///机器分组路由
            routes.MapRoute(
                name: "MacGroup", // 路由名称
                url: "{controller}/{action}/{groupid}/{teamid}/", // 带有参数的 URL
                defaults: new { controller = "MacGroup", action = "Config", groupid = 1, teamid = 1 } // 参数默认值
            );
        }
    }
}
