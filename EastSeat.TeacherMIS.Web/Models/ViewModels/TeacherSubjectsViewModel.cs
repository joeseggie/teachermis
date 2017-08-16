using System;
using System.Collections.Generic;

namespace EastSeat.TeacherMIS.Web.Models.ViewModels
{
    public class TeacherSubjectsViewModel
    {
        public Guid TeacherId { get; set; }
        public string Teacher { get; set; }
        public IEnumerable<SubjectTaughtViewModel> SubjectsTaught { get; set; }
    }
}