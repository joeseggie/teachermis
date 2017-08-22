using System;

namespace EastSeat.TeacherMIS.Web.Models.ViewModels
{
    public class RetirementViewModel
    {
        public Guid TeacherId { get; set; }
        public string Teacher { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string School { get; set; }
        public int DaysToRetirement { get; set; }
        public Guid SchoolId { get; set; }
    }
}
