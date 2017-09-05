using System;
using System.Collections.Generic;

namespace EastSeat.TeacherMIS.Web.Models.ViewModels
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; set; }
    }
}