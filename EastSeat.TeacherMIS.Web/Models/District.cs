using System;
using System.Linq;
using System.Collections.Generic;

namespace EastSeat.TeacherMIS.Web.Models
{
    public class District
    {
        public Guid DistrictId { get; set; }
        public string Name { get; set; }
        public decimal? WageAllocation { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual IEnumerable<School> Schools { get; set; }
    }
}