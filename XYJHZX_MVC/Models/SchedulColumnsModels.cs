using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XYJHZX_MVC.Models
{
    /// <summary>
    /// 列标题
    /// </summary>
    public class SchedulColumnMainModel
    {
        [Required]
        [Display(Name = "列标题主键")]
        public int ColMainId { get; set; }

        [Required]
        [Display(Name = "列标题名称")]
        public string ColName { get; set; }

        [Required]
        [Display(Name = "类型标识")]
        public string Desciption { get; set; }

        [Required]
        [Display(Name = "标题分类")]
        public int ColType { get; set; }

        [Required]
        [Display(Name ="修改状态")]
        public string Status { get; set; }
    }

    /// <summary>
    /// 可选值
    /// </summary>
    public class SchedulColumnDetailModel
    {
        [Required]
        [Display(Name ="可选值主键")]
        public int ColDetailId { get; set; }

        [Required]
        [Display(Name ="列标题主键")]
        public int ColMainId { get; set; }

        [Required]
        [Display(Name ="可选值名称")]
        public string ColDetailName { get; set; }

        [Required]
        [Display(Name = "可选值描述")]
        public string Desciption { get; set; }

        [Required]
        [Display(Name = "修改状态")]
        public string Status { get; set; }
    }

    /// <summary>
    /// 列类型
    /// </summary>
    public class SchedulColumnTypeModel
    {
        [Required]
        [Display(Name = "类型标识")]
        public string TypeSign { get; set; }

        [Required]
        [Display(Name ="类型名称")]
        public string TypeName { get; set; }
    }
}