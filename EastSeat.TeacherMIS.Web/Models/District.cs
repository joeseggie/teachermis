using System;
using System.Linq;

namespace EastSeat.TeacherMIS.Web.Models
{
    public class District
    {
        public Guid DistrictId { get; set; }
        public string Name { get; set; }
        public decimal? WageAllocation { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual IQueryable<School> Schools { get; set; }
    }
}