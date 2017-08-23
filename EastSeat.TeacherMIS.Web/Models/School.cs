using System;
using System.Linq;
using System.Collections.Generic;

namespace EastSeat.TeacherMIS.Web.Models
{
    public class School
    {
        public Guid SchoolId { get; set; }
        public string Name { get; set; }
        public Guid? DistrictId { get; set; }
        public Guid? SchoolCategoryId { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual IEnumerable<Teacher> Teachers { get; set; }
        public virtual IEnumerable<Headmaster> Headmasters { get; set; }
        public virtual District District { get; set; }
        public virtual SchoolCategory SchoolCategory { get; set; }
    }
}