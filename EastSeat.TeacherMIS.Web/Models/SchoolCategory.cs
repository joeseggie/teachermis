using System;
using System.Linq;

namespace EastSeat.TeacherMIS.Web.Models
{
    public class SchoolCategory
    {
        public Guid SchoolCategoryId { get; set; }
        public string Description { get; set; }
        public byte[] RowVersion { get; set; }
        
        public virtual IQueryable<School> Schools { get; set; }
    }
}