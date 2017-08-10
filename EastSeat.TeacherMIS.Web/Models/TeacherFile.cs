using System;

namespace EastSeat.TeacherMIS.Web.Models
{
    public class TeacherFile
    {
        public Guid TeacherFileId { get; set; }
        public Guid TeacherId { get; set; }
        public DateTime RecordDate { get; set; }
        public string Details { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}