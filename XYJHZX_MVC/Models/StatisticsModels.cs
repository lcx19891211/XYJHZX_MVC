using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XYJHZX_MVC.Models
{
    public class StatisticsModels
    {
        [Required]
        [Display(Name = "统计主题")]
        public string ColName { get; set; }
        
        [Required]
        [Display(Name = "统计主题主键")]
        public int ColMainId { get; set; }

        [Required]
        [Display(Name = "统计项目")]
        public string ColDetailName { get; set; }

        [Required]
        [Display(Name = "统计项目主键")]
        public int ColDetailId { get; set; }

        [Required]
        [Display(Name = "所属系统")]
        public int ColType { get; set; }
        
        [Required]
        [Display(Name = "统计数量")]
        public int Count { get; set; }
    }
}