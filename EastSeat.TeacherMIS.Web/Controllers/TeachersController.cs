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
using System.Globalization;
using System.Runtime.Versioning;
using System.Threading;

namespace EastSeat.TeacherMIS.Web.Controllers
{
    [Authorize(Roles = "Admin,Supervisor,HumanResource,DataEntrant")]
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ITeacherFile _teacherFileService;

        public TeachersController(ApplicationDbContext databaseContext, ITeacherFile teacherFileService)
        {
            _db = databaseContext;
            _teacherFileService = teacherFileService;
        }

        public async Task<IActionResult> Index(string search, int? page)
        {
            if (search != null)
            {
                page = 1;
            }

            if (string.IsNullOrWhiteSpace(search))
            {
                return View(null);
            }

            var model = _db.Teachers
                .Where(t => t.Fullname.ToLower().Contains(search.ToLower()) || t.UtsFileNumber.ToLower().Contains(search.ToLower()) || t.IppsNumber.Contains(search.ToLower()))
                .Select(t => new TeacherViewModel{
                    TeacherId = t.TeacherId,
                    ConfirmationEscMinute = t.ConfirmationEscMinute,
                    PositionId = t.PositionId??Guid.Empty,
                    GradeId = t.GradeId??Guid.Empty,
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
                    SchoolName = t.School.Name,
                    UtsFileNumber = t.UtsFileNumber
                });

            int pageSize = 10;            
            return View(await PaginatedList<TeacherViewModel>.CreateAsync(model.AsNoTracking(), page ?? 1, pageSize));
        }

        public IActionResult Register()
        {
            ViewData["Schools"] = _db.Schools
                .Select(l => new SelectListItem{
                    Value = l.SchoolId.ToString(),
                    Text = l.Name
                });
            ViewData["Positions"] = _db.Positions.Select(p => new SelectListItem{
                Value = p.PositionId.ToString(),
                Text = p.Name
            });
            ViewData["Grades"] = _db.Grades.Select(g => new SelectListItem{
                Value = g.GradeId.ToString(),
                Text = g.Name
            });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(TeacherViewModel formData)
        {
            if(ModelState.IsValid)
            {
                var newTeacherEntry = _db.Teachers.Add(new Teacher{
                    PositionId            = formData.PositionId,
                    GradeId            = formData.GradeId,
                    ConfirmationEscMinute      = formData.ConfirmationEscMinute,
                    CurrentPositionAppMinute   = formData.CurrentPositionAppMinute,
                    CurrentPositionPostingDate = formData.CurrentPositionPostingDate,
                    DateOfBirth                = formData.DateOfBirth,
                    FirstAppEscMinute          = formData.FirstAppEscMinute,
                    FirstProbationAppDate      = formData.FirstProbationAppDate,
                    Fullname                   = formData.Fullname,
                    Gender                     = formData.Gender,
                    IppsNumber                 = formData.IppsNumber,
                    ProbationAppDesignation    = formData.ProbationAppDesignation,
                    RegistrationNumber         = formData.RegistrationNumber,
                    RowVersion                 = formData.RowVersion,
                    SchoolId                   = formData.SchoolId,
                    UtsFileNumber              = formData.UtsFileNumber
                });

                var teacherId = newTeacherEntry.Entity.TeacherId;

                _db.SaveChanges();

                TempData["Message"] = "Teacher registered successfully";

                return RedirectToAction("file", routeValues: new{ id = teacherId.ToString() });
            }

            ViewData["Schools"] = _db.Schools
                .Select(l => new SelectListItem{
                    Value = l.SchoolId.ToString(),
                    Text = l.Name
                });
            ViewData["Positions"] = _db.Positions.Select(p => new SelectListItem{
                Value = p.PositionId.ToString(),
                Text = p.Name
            });
            ViewData["Grades"] = _db.Grades.Select(g => new SelectListItem{
                Value = g.GradeId.ToString(),
                Text = g.Name
            });
            return View(formData);
        }

        public async Task<IActionResult> File(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                Guid teacherId;

                if (Guid.TryParse(id, out teacherId))
                {
                    var model = await _db.Teachers
                        .Select(t => new TeacherViewModel{
                            TeacherId = t.TeacherId,
                            ConfirmationEscMinute = t.ConfirmationEscMinute,
                            PositionId = t.PositionId??Guid.Empty,
                            GradeId = t.GradeId??Guid.Empty,
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
                        .SingleOrDefaultAsync(t => t.TeacherId == teacherId);

                    ViewData["SchoolsSelectList"] = _db.Schools.Select(s => new SelectListItem{
                        Value = s.SchoolId.ToString(),
                        Text = s.Name
                    });
                    ViewData["Positions"] = _db.Positions.Select(p => new SelectListItem{
                        Value = p.PositionId.ToString(),
                        Text = p.Name
                    });
                    ViewData["Grades"] = _db.Grades.Select(g => new SelectListItem{
                        Value = g.GradeId.ToString(),
                        Text = g.Name
                    });
                    
                    await _teacherFileService.LogAccess(User.Identity.Name, model.TeacherId);
                    return View(model);
                }
            }
            return View("TeacherNotFound");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> File(TeacherViewModel formData)
        {
            if (ModelState.IsValid)
            {
                var teacherForUpdate = _db.Teachers.SingleOrDefault(t => t.TeacherId == formData.TeacherId);
                if (teacherForUpdate == null)
                {
                    ModelState.AddModelError("","Teacher record doesn't exist or is invalid");
                }
                else
                {
                    teacherForUpdate.PositionId            = formData.PositionId            ;
                    teacherForUpdate.GradeId = formData.GradeId;
                    teacherForUpdate.ConfirmationEscMinute      = formData.ConfirmationEscMinute      ;
                    teacherForUpdate.CurrentPositionAppMinute   = formData.CurrentPositionAppMinute   ;
                    teacherForUpdate.CurrentPositionPostingDate = formData.CurrentPositionPostingDate ;
                    teacherForUpdate.DateOfBirth                = formData.DateOfBirth                ;
                    teacherForUpdate.FirstAppEscMinute          = formData.FirstAppEscMinute          ;
                    teacherForUpdate.FirstProbationAppDate      = formData.FirstProbationAppDate      ;
                    teacherForUpdate.Fullname                   = formData.Fullname                   ;
                    teacherForUpdate.Gender                     = formData.Gender                     ;
                    teacherForUpdate.IppsNumber                 = formData.IppsNumber                 ;
                    teacherForUpdate.ProbationAppDesignation    = formData.ProbationAppDesignation    ;
                    teacherForUpdate.RegistrationNumber         = formData.RegistrationNumber         ;
                    teacherForUpdate.RowVersion                 = formData.RowVersion                 ;
                    teacherForUpdate.SchoolId                   = formData.SchoolId                   ;
                    teacherForUpdate.UtsFileNumber              = formData.UtsFileNumber              ;
                    teacherForUpdate.RowVersion                 = formData.RowVersion;
                    teacherForUpdate.TeacherId                  = formData.TeacherId;

                    _db.Update(teacherForUpdate);

                    await _db.SaveChangesAsync();
                }
            }

            ViewData["SchoolsSelectList"] = _db.Schools.Select(s => new SelectListItem{
                Value = s.SchoolId.ToString(),
                Text = s.Name
            });
            ViewData["Positions"] = _db.Positions.Select(p => new SelectListItem{
                Value = p.PositionId.ToString(),
                Text = p.Name
            });
            ViewData["Grades"] = _db.Grades.Select(g => new SelectListItem{
                Value = g.GradeId.ToString(),
                Text = g.Name
            });

            return View(formData);
        }

        public async Task<IActionResult> School(string id, string search, int? page)
        {
            if(!string.IsNullOrWhiteSpace(id))
            {
                Guid schoolId;

                if (Guid.TryParse(id, out schoolId))
                {
                    if(search != null)
                    {
                        page = 1;
                    }

                    var model = _db.Teachers
                        .Where(t => t.SchoolId == schoolId)
                        .Select(t => new TeacherViewModel{
                            TeacherId = t.TeacherId,
                            ConfirmationEscMinute = t.ConfirmationEscMinute,
                            PositionId = t.PositionId??Guid.Empty,
                            GradeId = t.GradeId??Guid.Empty,
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
                        });
                    
                    int pageSize = 10;
                    return View(await PaginatedList<TeacherViewModel>.CreateAsync(model.AsNoTracking(), page ?? 1, pageSize));
                }
            }
            return View("SchoolNotFound");
        }

        public async Task<IActionResult> Subject(string id, string search, int? page)
        {
            if(!string.IsNullOrWhiteSpace(id))
            {
                Guid subjectId;

                if (Guid.TryParse(id, out subjectId))
                {
                    if(search != null)
                    {
                        page = 1;
                    }

                    var model = _db.SubjectsTaught
                        .Where(t => t.SubjectId == subjectId)
                        .Select(t => new TeacherViewModel{
                            TeacherId                  = t.Teacher.TeacherId,
                            ConfirmationEscMinute      = t.Teacher.ConfirmationEscMinute,
                            PositionId            = t.Teacher.PositionId??Guid.Empty,
                            GradeId = t.Teacher.GradeId??Guid.Empty,
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
                        });
                    
                    int pageSize = 10;
                    return View(await PaginatedList<TeacherViewModel>.CreateAsync(model.AsNoTracking(), page ?? 1, pageSize));
                }
            }
            return View("SubjectNotFound");
        }
    }
}