using System;
using System.ComponentModel.DataAnnotations;

namespace EastSeat.TeacherMIS.Web.Models.ViewModels
{
    public class TeacherViewModel
    {
        [Required()]
        [Display(Name="Teacher Id")]
        public Guid TeacherId { get; set; }
        [Required()]
        [Display(Name="Fullname")]
        public string Fullname { get; set; }
        [Required()]
        [Display(Name="Sex")]
        public string Gender { get; set; }
        [Required()]
        [Display(Name="UTS Number")]
        public string UtsFileNumber { get; set; }
        [Required()]
        [Display(Name="Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required()]
        [Display(Name="Registration Number")]
        public string RegistrationNumber { get; set; }
        [Required()]
        [Display(Name="IPPS Number")]
        public string IppsNumber { get; set; }
        [Required()]
        [Display(Name="Date of First Appointment Starting with Probation")]
        public DateTime FirstProbationAppDate { get; set; }
        [Required()]
        [Display(Name="Position When Appointed on Probation")]
        public string ProbationAppDesignation { get; set; }
        [Required()]
        [Display(Name="Education Service Commission Minute of First Appointment")]
        public string FirstAppEscMinute { get; set; }
        [Required()]
        [Display(Name="Education Service Commission Minute of Appointment")]
        public string ConfirmationEscMinute { get; set; }
        [Required()]
        [Display(Name="Current Position")]
        public string CurrentPosition { get; set; }
        [Required()]
        [Display(Name="Education Service Commission Minute of Current Position")]
        public string CurrentPositionAppMinute { get; set; }
        [Required()]
        [Display(Name="School")]
        public Guid SchoolId { get; set; }
        [Required()]
        [Display(Name="Date of Posting to Current Position")]
        public DateTime CurrentPositionPostingDate { get; set; }
        public byte[] RowVersion { get; set; }
    }
}