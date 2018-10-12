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
}