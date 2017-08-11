using System;
using System.ComponentModel.DataAnnotations;

namespace EastSeat.TeacherMIS.Web.Models.ViewModels
{
    public class SubjectViewModel
    {
        [Required]
        public Guid SubjectId { get; set; }
        [Required]
        public string Description { get; set; }
        public byte[] RowVersion { get; set; }
    }
}