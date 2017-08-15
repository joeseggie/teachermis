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
        [Required]
        [Display(Name = "Subject Category")]
        public Guid SubjectCategoryId { get; set; }
        public string SubjectCategoryStub { get; set; }
        public byte[] RowVersion { get; set; }

        [Display(Name = "Subject Category")]
        public string SubjectCategory { get; set; }
    }
}