using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XYJHZX_MVC.Models
{
    public class CurrentSchedulModel
    {
        [Required]
        [Display(Name = "机位ID")]
        public Int64 macid { get; set; }
        [Required]
        [Display(Name = "机位名称")]
        public string macname { get; set; }
        [Required]
        [Display(Name = "病人姓名")]
        public string PatName { get; set; }
        [Required]
        [Display(Name = "签到时间")]
        public string signInDate { get; set; }
        [Required]
        [Display(Name = "排队号")]
        public int SignInSeq { get; set; }
        [Required]
        [Display(Name = "分区名称")]
        public string teamDescption { get; set; }
        [Required]
        [Display(Name = "分组名称")]
        public string GroupDescption { get; set; }
    }
}