using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EastSeat.TeacherMIS.Web.Models.ViewModels
{
    public class SchoolViewModel
    {
        [Required()]
        public Guid SchoolId { get; set; }
        [Required]
        [Display(Name = "School Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name="District")]
        public Guid DistrictId { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual IEnumerable<SelectListItem> DistrictsSelectList { get; set; }
    }
}