using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using EastSeat.TeacherMIS.Web.Data;
using EastSeat.TeacherMIS.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EastSeat.TeacherMIS.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Accessing landing page");
            var model = new DashboardViewModel();

            var teachersQuery = _db.SubjectsTaught
                .Include(t => t.Subject)
                    .ThenInclude(s => s.SubjectCategory)
                .Select(q => new {
                    Category = q.Subject.SubjectCategory.Description
                });

            var scienceTeachers = await teachersQuery.CountAsync(q => q.Category.ToLower() == "sciences");
            var artsTeachers = await teachersQuery.CountAsync(q => q.Category.ToLower() == "arts");

            model.TeachersScienceVersusArts = $"{scienceTeachers},{artsTeachers}";

            var teachersCount = await _db.Teachers.CountAsync(t => (t.DateOfBirth.AddYears(50) - DateTime.Now.AddDays(180)).Days >= 180);
            var retiringTeachers = await _db.Teachers.CountAsync(t => (t.DateOfBirth.AddYears(50) - DateTime.Now.AddDays(180)).Days < 180);

            model.TeachersRetiringVersusNonRetiring = $"{teachersCount},{retiringTeachers}";

            var totalAllocatedWage = await _db.Districts.SumAsync(d => d.WageAllocation);
            var hiringCost = await _db.SubjectsTaught.SumAsync(s => s.Subject.SubjectCategory.Salary);

            model.AllocatedWagesVersusHiringCost = $"{totalAllocatedWage},{hiringCost}";

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
