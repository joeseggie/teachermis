using System;
using System.ComponentModel.DataAnnotations;

namespace EastSeat.TeacherMIS.Web.Models.ViewModels
{
    public class SubjectCategoryViewModel
    {
        [Required]
        public Guid SubjectCategoryId { get; set; }
        [Required]
        public string Description { get; set; }
        public string Stub { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString="{0:0}")]
        public decimal Salary { get; set; }
        public byte[] RowVersion { get; set; }
    }
}