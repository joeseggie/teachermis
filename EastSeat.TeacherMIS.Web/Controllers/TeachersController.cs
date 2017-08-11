using System;
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
            if (!string.IsNullOrWhiteSpace(id))
            {
                Guid teacherId;

                if (Guid.TryParse(id, out teacherId))
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
                        })
                        .SingleOrDefault(t => t.TeacherId == teacherId);
                    return View(model);
                }
            }
            return View("TeacherNotFound");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult File(TeacherViewModel formData)
        {
            return View();
        }

        public IActionResult School(string id, string search, int? page)
        {
            if(!string.IsNullOrWhiteSpace(id))
            {
                Guid schoolId;

                if (Guid.TryParse(id, out schoolId))
                {
                    var model = _db.Teachers
                        .Where(t => t.SchoolId == schoolId)
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
            }
            return View("SchoolNotFound");
        }

        public IActionResult Subject(string id, string search, int? page)
        {
            if(!string.IsNullOrWhiteSpace(id))
            {
                Guid subjectId;

                if (Guid.TryParse(id, out subjectId))
                {
                    var model = _db.SubjectsTaught
                        .Where(t => t.SubjectId == subjectId)
                        .Select(t => new TeacherViewModel{
                            TeacherId                  = t.Teacher.TeacherId,
                            ConfirmationEscMinute      = t.Teacher.ConfirmationEscMinute,
                            CurrentPosition            = t.Teacher.CurrentPosition,
                            CurrentPositionAppMinute   = t.Teacher.CurrentPositionAppMinute,
                            CurrentPositionPostingDate = t.Teacher.CurrentPositionPostingDate,
                            DateOfBirth                = t.Teacher.DateOfBirth,
                            FirstAppEscMinute          = t.Teacher.FirstAppEscMinute,
                            FirstProbationAppDate      = t.Teacher.FirstProbationAppDate,
                            Fullname                   = t.Teacher.Fullname,
                            Gender                     = t.Teacher.Gender,
                            IppsNumber                 = t.Teacher.IppsNumber,
                            ProbationAppDesignation    = t.Teacher.ProbationAppDesignation,
                            RegistrationNumber         = t.Teacher.RegistrationNumber,
                            RowVersion                 = t.Teacher.RowVersion,
                            SchoolId                   = t.Teacher.SchoolId,
                            UtsFileNumber              = t.Teacher.UtsFileNumber
                        }).ToList();

                    return View(model);
                }
            }
            return View("SubjectNotFound");
        }
    }
}