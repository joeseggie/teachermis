using System;

namespace EastSeat.TeacherMIS.Web.Models.ViewModels
{
    public class SubjectTaughtViewModel
    {
        public string Subject { get; set; }
        public Guid SubjectId { get; set; }
        public string SubjectCategory { get; set; }
        public Guid SubjectCategoryId { get; set; }
        public string SubjectCategoryStub { get; set; }
    }
}