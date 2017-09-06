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
using EastSeat.TeacherMIS.Web.Services;

namespace EastSeat.TeacherMIS.Web.Controllers
{
    [Authorize(Roles = "Admin,Supervisor,HumanResource,DataEntrant")]
    public class TimelineController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ITeacherFile _teacherFileService;

        public TimelineController(ApplicationDbContext databaseContext, ITeacherFile teacherFileService)
        {
            _db = databaseContext;
            _teacherFileService = teacherFileService;
        }

        public async Task<IActionResult> Teacher(Guid id)
        {
            var model = _db.TeacherFiles
            .Where(f => f.TeacherId == id)
            .Select(f => new TimelineViewModel{
                TeacherId = f.TeacherId,
                Teacher = f.Teacher.Fullname,
                RecordDate = f.RecordDate,
                Details = f.Details.Replace("~","<br>")
            });

            await _teacherFileService.LogTimelineAccess(User.Identity.Name, id);

            return View(await model.ToListAsync());
        }
    }
}