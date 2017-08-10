using System;

namespace EastSeat.TeacherMIS.Web.Models
{
    public class SubjectTaught
    {
        public Guid SubjectTaughtId { get; set; }
        public Guid TeacherId { get; set; }
        public Guid SubjectId { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual Subject Subject { get; set; }
    }
}