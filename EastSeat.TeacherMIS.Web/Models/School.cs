using System;
using System.Linq;

namespace EastSeat.TeacherMIS.Web.Models
{
    public class School
    {
        public Guid SchoolId { get; set; }
        public string Name { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual IQueryable<Teacher> Teachers { get; set; }
        public virtual IQueryable<Headmaster> Headmasters { get; set; }
    }
}