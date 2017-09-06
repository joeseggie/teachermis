using System;
using System.Linq;
using System.Threading.Tasks;
using EastSeat.TeacherMIS.Web.Data;
using EastSeat.TeacherMIS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EastSeat.TeacherMIS.Web.Controllers
{
    [Authorize(Roles = "Admin,Supervisor,HumanResource,DataEntrant")]
    public class TeacherReportsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TeacherReportsController(ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
        }

        public async Task<IActionResult> Retiring(double id)
        {
            var model = _db.Teachers
                .Where(t => (t.DateOfBirth.AddYears(50) - DateTime.Now.AddDays(id)).Days < 180)
                .Select(t => new RetirementViewModel{
                    TeacherId = t.TeacherId,
                    Teacher = t.Fullname,
                    DateOfBirth = t.DateOfBirth,
                    School = t.School.Name,
                    DaysToRetirement = (t.DateOfBirth.AddYears(50).Subtract(DateTime.Now.AddDays(id))).Days,
                    SchoolId = t.SchoolId
                });

            return View(await model.ToListAsync());
        }
    }
}
