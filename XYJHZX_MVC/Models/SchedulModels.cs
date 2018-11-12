using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XYJHZX_MVC.Models
{
    public class SchedulMainModel
    {
        [Required]
        [Display(Name = "检查主索ID")]
        public Int64 mainid { get; set; }
        [Required]
        [Display(Name = "检查日期")]
        public string SchedulDate { get; set; }
        [Required]
        [Display(Name = "检查时间")]
        public string SchedulTime { get; set; }
        [Required]
        [Display(Name = "病人ID")]
        public Int64 patid { get; set; }
        [Required]
        [Display(Name = "检查前体重")]
        public float patWeightBefore { get; set; }
        [Required]
        [Display(Name = "检查后体重")]
        public float patWeightAfter { get; set; }
        [Required]
        [Display(Name = "分区ID")]
        public Int64 teamid { get; set; }
        [Required]
        [Display(Name = "分组ID")]
        public Int64 groupid { get; set; }
        [Required]
        [Display(Name = "机器ID")]
        public Int64 macid { get; set; }
        [Required]
        [Display(Name = "透析器名称")]
        public string dialyzerName { get; set; }
        [Required]
        [Display(Name = "路径名称")]
        public string routeName { get; set; }
        [Required]
        [Display(Name = "抗凝血剂名称")]
        public string anticoagulantName { get; set; }
        [Required]
        [Display(Name = "备注")]
        public string remark { get; set; }
        [Required]
        [Display(Name = "病人登记标识")]
        public string  signinDate { get; set; }
        [Required]
        [Display(Name = "最后修改日期")]
        public string lastDate { get; set; }
        [Required]
        [Display(Name = "检查修改标识")]
        public int status { get; set; }
    }

    public class SchedulShowModel
    {
        [Required]
        [Display(Name = "检查主索ID")]
        public Int64 mainid { get; set; }
        [Required]
        [Display(Name = "检查日期")]
        public string SchedulDate { get; set; }
        [Required]
        [Display(Name = "检查时间")]
        public string SchedulTime { get; set; }
        [Required]
        [Display(Name = "病人ID")]
        public Int64 patid { get; set; }
        [Required]
        [Display(Name = "病人姓名")]
        public string patName { get; set; }
        [Required]
        [Display(Name = "机器ID")]
        public Int64 macid { get; set; }
        [Required]
        [Display(Name = "机器名称")]
        public string macname { get; set; }
        [Required]
        [Display(Name = "透析器名称")]
        public string dialyzerName { get; set; }
        [Required]
        [Display(Name = "路径名称")]
        public string routeName { get; set; }
        [Required]
        [Display(Name = "抗凝血剂名称")]
        public string anticoagulantName { get; set; }
        [Required]
        [Display(Name = "备注")]
        public string remark { get; set; }
        [Required]
        [Display(Name = "检查修改标识")]
        public int status { get; set; }
    }

    public class SchedulColumns
    {
        [Required]
        [Display(Name = "选项主键")]
        public Int64 colMainId { get; set; }
        [Required]
        [Display(Name = "选项标题")]
        public string colName { get; set; }
        [Required]
        [Display(Name = "选项数据类型")]
        public string desciption { get; set; }
        [Required]
        [Display(Name = "选项必选类型")]
        public Int32 colType { get; set; }
        [Required]
        [Display(Name = "选项明细主键")]
        public Int64 colDetailId { get; set; }
        [Required]
        [Display(Name = "选项明细名称")]
        public string colDetailName { get; set; }
    }

    public class SchedulPrint
    {
        [Required]
        [Display(Name = "分组名称")]
        public string GroupName{ get; set; }
        [Required]
        [Display(Name = "主键")]
        public string MainId { get; set; }
        [Required]
        [Display(Name = "分组描述")]
        public string GroupDescption{ get; set; }
        [Required]
        [Display(Name = "分区名称")]
        public string TeamName{ get; set; }
        [Required]
        [Display(Name = "分组描述")]
        public string TeamDescption{ get; set; }
        [Required]
        [Display(Name = "机器名称")]
        public string MacName{ get; set; }
        [Required]
        [Display(Name = "机器描述")]
        public string MacDescption{ get; set; }
        [Required]
        [Display(Name = "检查日期")]
        public string SchedulDate{ get; set; }
        [Required]
        [Display(Name = "检查时间")]
        public string SchedulTime{ get; set; }
        [Required]
        [Display(Name = "透析器")]
        public string DialyzerName{ get; set; }
        [Required]
        [Display(Name = "路径")]
        public string RouteName{ get; set; }
        [Required]
        [Display(Name = "抗凝剂")]
        public string AnticoagulantName{ get; set; }
        [Required]
        [Display(Name = "备注")]
        public string Remark{ get; set; }
        [Required]
        [Display(Name = "签到时间")]
        public string SigninDate{ get; set; }
        [Required]
        [Display(Name = "签到顺序")]
        public int SignInSeq{ get; set; }
        [Required]
        [Display(Name = "病人姓名")]
        public string PatName{ get; set; }
        [Required]
        [Display(Name = "病人身份证号")]
        public string PatIdCardNo { get; set; }
        [Required]
        [Display(Name = "检查前体重")]
        public int PatWeightBefore { get; set; }
        [Required]
        [Display(Name = "检查后体重")]
        public int PatWeightAfter { get; set; }
    }
}