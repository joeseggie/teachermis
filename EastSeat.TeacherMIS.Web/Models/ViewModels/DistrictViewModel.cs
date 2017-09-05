using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EastSeat.TeacherMIS.Web.Models.ViewModels
{
    public class DistrictViewModel
    {
        [Required]
        public Guid DistrictId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public decimal WageAllocation { get; set; }

        [Required]
        public byte[] RowVersion { get; set; }
        public IEnumerable<SchoolViewModel> Schools { get; set; }
    }
}