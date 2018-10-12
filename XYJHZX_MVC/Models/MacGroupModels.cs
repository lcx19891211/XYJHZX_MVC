using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XYJHZX_MVC.Models
{
    public class CollectMacGroup
    {
        [Required]
        [Display(Name = "分组集合")]
        List<GroupModel>GroupCollect { get; set; }
        [Required]
        [Display(Name = "分区集合")]
        List<TeamModel> TeamCollect { get; set; }
        [Required]
        [Display(Name = "机器集合")]
        List<MachineModel> MacCollect { get; set; }
        [Required]
        [Display(Name = "区组集合")]
        List<TeamWithGroupModel> TeamWithGroupCollect { get; set; }
        [Required]
        [Display(Name = "机区集合")]
        List<MachineWithTeamModel> MachineWithTeamCollect { get; set; }
    }

    public class GroupModel
    {
        [Required]
        [Display(Name = "分组主键")]
        public int Groupid { get; set; }

        [Required]
        [Display(Name = "分组名称")]
        public string GroupName { get; set; }

        [Required]
        [Display(Name = "分组描述")]
        public string Desciption { get; set; }

        [Required]
        [Display(Name = "分组排序")]
        public string SeqID { get; set; }

        [Required]
        [Display(Name = "状态")]
        public string Status { get; set; }

    }

    public class TeamModel
    {
        [Required]
        [Display(Name = "分区主键")]
        public int Teamid { get; set; }

        [Required]
        [Display(Name = "分区名称")]
        public string TeamName { get; set; }

        [Required]
        [Display(Name = "分区描述")]
        public string Desciption { get; set; }

        [Required]
        [Display(Name = "分区排序")]
        public string SeqID { get; set; }

        [Required]
        [Display(Name = "分组主键")]
        public int Groupid { get; set; }

        [Required]
        [Display(Name = "状态")]
        public string Status { get; set; }
    }

    public class MachineModel
    {
        [Required]
        [Display(Name = "机器主键")]
        public int Macid { get; set; }

        [Required]
        [Display(Name = "机器名称")]
        public string MacName { get; set; }

        [Required]
        [Display(Name = "机器描述")]
        public string Desciption { get; set; }

        [Required]
        [Display(Name = "机器排序")]
        public string SeqID { get; set; }

        [Required]
        [Display(Name = "分区主键")]
        public int Teamid { get; set; }

        [Required]
        [Display(Name = "状态")]
        public string Status { get; set; }
    }

    public class TeamWithGroupModel
    {
        [Required]
        [Display(Name = "主键")]
        public int Pkid { get; set; }

        [Required]
        [Display(Name = "分区主键")]
        public int Teamid { get; set; }

        [Required]
        [Display(Name = "分组主键")]
        public int Groupid { get; set; }
    }

    public class MachineWithTeamModel
    {
        [Required]
        [Display(Name = "主键")]
        public int Pkid { get; set; }

        [Required]
        [Display(Name = "机器主键")]
        public int Macid { get; set; }

        [Required]
        [Display(Name = "分区主键")]
        public int Teamid { get; set; }
    }

    public class MacGroupModel
    {
        [Display(Name = "分组主键")]
        public int Groupid { get; set; }
        [Display(Name = "分组名称")]
        public string GroupName { get; set; }
        [Display(Name = "分组描述")]
        public string GroupDescption { get; set; }
        [Display(Name = "分区主键")]
        public int Teamid { get; set; }
        [Display(Name = "分区名称")]
        public string TeamName { get; set; }
        [Display(Name = "分区描述")]
        public string TeamDescption { get; set; }
        [Display(Name = "机器主键")]
        public int Macid { get; set; }
        [Display(Name = "机器名称")]
        public string MacName { get; set; }
        [Display(Name = "机器描述")]
        public string MacDescption { get; set; }
    }

}