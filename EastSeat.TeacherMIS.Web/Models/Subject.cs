using System;
using System.Linq;
using System.Collections.Generic;

namespace EastSeat.TeacherMIS.Web.Models
{
    public class Subject
    {
        public Guid SubjectId { get; set; }
        public string Description { get; set; }
        public Guid? SubjectCategoryId { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual IEnumerable<SubjectTaught> SubjectsTaught { get; set; }
        public virtual SubjectCategory SubjectCategory { get; set; }
    }
}
