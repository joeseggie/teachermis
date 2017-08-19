using System;
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
        public byte[] RowVersion { get; set; }
    }
}