using System;
using System.Linq;

namespace EastSeat.TeacherMIS.Web.Models
{
    public class Subject
    {
        public Guid SubjectId { get; set; }
        public string Description { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual IQueryable<SubjectTaught> SubjectsTaught { get; set; }
    }
}