using System;
using System.ComponentModel.DataAnnotations;


namespace XYJHZX_MVC.Models
{
    public class PatientModel
    {
        [Required]
        [Display(Name = "病人标识号")]
        public Int64 PatId { get; set; }
        [Required]
        [Display(Name = "病人姓名")]
        public string PatName { get; set; }
        [Required]
        [Display(Name = "病人性别")]
        public string PatSex { get; set; }
        [Required]
        [Display(Name = "病人出生日期")]
        public string PatBrithday { get; set; }
        [Required]
        [Display(Name = "病人年龄")]
        public int PatAge { get; set; }
        [Required]
        [Display(Name = "病人身份证")]
        public string PatIdCardNo { get; set; }
        [Required]
        [Display(Name = "诊疗卡号")]
        public string PatOutCardNo { get; set; }
        [Required]
        [Display(Name = "开单科室ID")]
        public string SendDeptId { get; set; }
        [Required]
        [Display(Name = "开单科室名称")]
        public string SendDeptName { get; set; }
        [Required]
        [Display(Name = "病人联系电话")]
        public string TelphoneNo { get; set; }
        [Required]
        [Display(Name = "病人缴费日期")]
        public string PaymentDate { get; set; }
        [Required]
        [Display(Name = "备注")]
        public string Remark { get; set; }
        [Required]
        [Display(Name = "病人登记标识")]
        public int IsRead { get; set; }
    }
}