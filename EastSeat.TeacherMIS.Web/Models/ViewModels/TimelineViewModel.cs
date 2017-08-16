using System;

namespace EastSeat.TeacherMIS.Web.Models.ViewModels
{
    public class TimelineViewModel
    {
        public Guid TeacherId { get; set; }
        public string Teacher { get; set; }
        public DateTime RecordDate { get; set; }
        public string Details { get; set; }
    }
}