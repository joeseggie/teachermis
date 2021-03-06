using System;
using System.Linq;
using System.Collections.Generic;

namespace EastSeat.TeacherMIS.Web.Models
{
    public class SchoolCategory
    {
        public Guid SchoolCategoryId { get; set; }
        public string Description { get; set; }
        public byte[] RowVersion { get; set; }
        
        public virtual IEnumerable<School> Schools { get; set; }
    }
}