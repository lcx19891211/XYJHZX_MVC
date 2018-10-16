using System;
using System.ComponentModel.DataAnnotations;


namespace XYJHZX_MVC.Models
{
    public class SplitDateModel
    {
        [Required]
        [Display(Name = "时间分区ID")]
        public Int64 SplitId { get; set; }
        [Required]
        [Display(Name = "时间分区名称")]
        public string SplitName { get; set; }
        [Required]
        [Display(Name = "时间分区描述")]
        public string SplitDesciption { get; set; }
        [Required]
        [Display(Name = "时间分区开始时间")]
        public string BegintTime { get; set; }
        [Required]
        [Display(Name = "时间分区结束时间")]
        public string EndTime { get; set; }
    }
}