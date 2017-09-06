using System;
using System.Linq;

namespace EastSeat.TeacherMIS.Web.Models
{
    public class Position
    {
        public Guid PositionId { get; set; }
        public string Name { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual IQueryable<Teacher> Teachers { get; set; }
    }
}