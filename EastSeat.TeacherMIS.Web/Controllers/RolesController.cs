using Microsoft.AspNetCore.Identity;
using EastSeat.TeacherMIS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using EastSeat.TeacherMIS.Web.Models.ViewModels;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace EastSeat.TeacherMIS.Web.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var model = _roleManager.Roles
                .Select(r => new RoleViewModel{
                    RoleId = Guid.Parse(r.Id),
                    RoleName = r.Name
                });
            return View(model);
        }

        public IActionResult Assign()
        {
            ViewData["Roles"] = _roleManager.Roles
                .Select(r => new SelectListItem{
                    Value = r.Id,
                    Text = r.Name
                });

            ViewData["Users"] = _userManager.Users
                .Select(u => new SelectListItem{
                    Value = u.Id,
                    Text = $"{u.Firstname} {u.Lastname} - ({u.UserName})"
                });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(string role, string user)
        {
            if (string.IsNullOrWhiteSpace(role) || string.IsNullOrWhiteSpace(user))
            {
                ViewData["Roles"] = _roleManager.Roles
                    .Select(r => new SelectListItem{
                        Value = r.Id,
                        Text = r.Name
                    });

                ViewData["Users"] = _userManager.Users
                    .Select(u => new SelectListItem{
                        Value = u.Id,
                        Text = $"{u.Firstname} {u.Lastname} - ({u.UserName})"
                    });
                    
                ModelState.AddModelError("", "Select user and role to be assigned");
                return View();
            }

            var userToBeAssign = await _userManager.FindByIdAsync(user);
            var roleToAssign = (await _roleManager.FindByIdAsync(role)).Name;

            await _userManager.AddToRoleAsync(userToBeAssign, roleToAssign);

            return RedirectToAction("userprofile", new { controller = "account", id = userToBeAssign.UserName });
        }
    }
}