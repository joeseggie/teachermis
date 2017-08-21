using System;
using System.ComponentModel.DataAnnotations;

namespace EastSeat.TeacherMIS.Web.Models.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public Guid RoleId { get; set; }
        [Required]
        [Display(Name="Role Name")]
        public string RoleName { get; set; }
    }
}