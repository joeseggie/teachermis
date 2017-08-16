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
    public class TimelineController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TimelineController(ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
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

            return View(await model.ToListAsync());
        }
    }
}