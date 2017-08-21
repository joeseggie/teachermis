using System;
using System.ComponentModel.DataAnnotations;

namespace EastSeat.TeacherMIS.Web.Models.ViewModels
{
    public class SchoolCategoryViewModel
    {
        [Required]
        public Guid SchoolCategoryId { get; set; }
        [Required]
        public string Description { get; set; }
        public byte[] RowVersion { get; set; }
    }
}