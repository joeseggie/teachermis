using System;
using System.ComponentModel.DataAnnotations;

namespace EastSeat.TeacherMIS.Web.Models.ViewModels
{
    public class SchoolViewModel
    {
        [Required()]
        public Guid SchoolId { get; set; }
        [Required]
        [Display(Name = "School Name")]
        public string Name { get; set; }
        public byte[] RowVersion { get; set; }
    }
}