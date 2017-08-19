using System;
using System.Collections.Generic;
using System.Linq;
using EastSeat.TeacherMIS.Web.Models;

namespace EastSeat.TeacherMIS.Web.Models
{
    public class SubjectCategory
    {
        public Guid SubjectCategoryId { get; set; }
        public string Description { get; set; }
        public string Stub { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual IQueryable<Subject> Subjects { get; set; }
    }
}