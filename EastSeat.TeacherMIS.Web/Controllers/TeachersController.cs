using System.Linq;
using EastSeat.TeacherMIS.Web.Data;
using EastSeat.TeacherMIS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EastSeat.TeacherMIS.Web.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TeachersController(ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
        }

        public IActionResult Index(string search, int? page)
        {
            var model = _db.Teachers
                .Select(t => new TeacherViewModel{
                    TeacherId = t.TeacherId,
                    ConfirmationEscMinute = t.ConfirmationEscMinute,
                    CurrentPosition = t.CurrentPosition,
                    CurrentPositionAppMinute = t.CurrentPositionAppMinute,
                    CurrentPositionPostingDate = t.CurrentPositionPostingDate,
                    DateOfBirth = t.DateOfBirth,
                    FirstAppEscMinute = t.FirstAppEscMinute,
                    FirstProbationAppDate = t.FirstProbationAppDate,
                    Fullname = t.Fullname,
                    Gender = t.Gender,
                    IppsNumber = t.IppsNumber,
                    ProbationAppDesignation = t.ProbationAppDesignation,
                    RegistrationNumber = t.RegistrationNumber,
                    RowVersion = t.RowVersion,
                    SchoolId = t.SchoolId,
                    UtsFileNumber = t.UtsFileNumber
                }).ToList();
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(TeacherViewModel formData)
        {
            return View();
        }

        public IActionResult File(string id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult File(TeacherViewModel formData)
        {
            return View();
        }

        public IActionResult School(string id, string search, int? page)
        {
            return View();
        }

        public IActionResult Subject(string id, string search, int? page)
        {
            return View();
        }
    }
}