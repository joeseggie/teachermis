using System;
using Microsoft.EntityFrameworkCore;

namespace EastSeat.TeacherMIS.Web.Models
{
    public class Headmaster
    {
        public Guid HeadmasterId { get; set; }
        public Guid? TeacherId { get; set; }
        public Guid SchoolId { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual School School { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}