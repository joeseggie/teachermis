using System;
using System.Linq;

namespace EastSeat.TeacherMIS.Web.Models
{
    public class School
    {
        public Guid SchoolId { get; set; }
        public string Name { get; set; }
        public Guid? DistrictId { get; set; }
        public Guid? SchoolCategoryId { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual IQueryable<Teacher> Teachers { get; set; }
        public virtual IQueryable<Headmaster> Headmasters { get; set; }
        public virtual District District { get; set; }
        public virtual SchoolCategory SchoolCategory { get; set; }
    }
}