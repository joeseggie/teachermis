using System;
using System.Linq;
using System.Threading.Tasks;
using EastSeat.TeacherMIS.Web.Data;
using EastSeat.TeacherMIS.Web.Helpers;
using EastSeat.TeacherMIS.Web.Models;
using EastSeat.TeacherMIS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EastSeat.TeacherMIS.Web.Controllers
{
    public class SubjectTaughtController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SubjectTaughtController(ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Guid newsubject, Guid TeacherId)
        {
            if (!_db.SubjectsTaught.Any(s => s.SubjectId == newsubject && s.TeacherId == TeacherId))
            {
                var newSubjectTaught = await _db.SubjectsTaught.AddAsync(new SubjectTaught{
                    SubjectId = newsubject,
                    TeacherId = TeacherId
                });
                await _db.SaveChangesAsync();

                TempData["Message"] = "Subject added successfully";

                return RedirectToAction("teacher", routeValues: new { controller = "subjects", id = TeacherId });
            }
            else
            {
                TempData["Message"] = "Subject already exists";

                return RedirectToAction("teacher", routeValues: new { controller = "subjects", id = TeacherId });
            }
        }
    }
}