using System;
using System.Linq;
using System.Collections.Generic;

namespace EastSeat.TeacherMIS.Web.Models
{
    public class Teacher
    {
        public Guid TeacherId { get; set; }
        public string Fullname { get; set; }
        public string Gender { get; set; }
        public string UtsFileNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string RegistrationNumber { get; set; }
        public string IppsNumber { get; set; }
        public DateTime FirstProbationAppDate { get; set; }
        public string ProbationAppDesignation { get; set; }
        public string FirstAppEscMinute { get; set; }
        public string ConfirmationEscMinute { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? GradeId { get; set; }
        public string CurrentPositionAppMinute { get; set; }
        public Guid SchoolId { get; set; }
        public DateTime CurrentPositionPostingDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual School School { get; set; }
        public virtual Headmaster Headmaster { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual Position Position { get; set; }
        public virtual IEnumerable<SubjectTaught> SubjectsTaught { get; set; }
        public virtual IEnumerable<TeacherFile> TeacherFiles { get; set; }
    }
}