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
    [Authorize]
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(TeacherViewModel formData)
        {
            if(ModelState.IsValid)
            {
                var newTeacherEntry = _db.Teachers.Add(new Teacher{
                    CurrentPosition            = formData.CurrentPosition,
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

                var recordDetails = $"Teacher file opened.~~"+
                                "Teacher details:~"+
                                $"Fullname: {formData.Fullname}~"+
                                $"Teacher Id: {teacherId}~"+
                                $"Current Position: {formData.CurrentPosition}~"+
                                $"Education Service Commission Minute of Appointment: {formData.ConfirmationEscMinute}~"+
                                $"CurrentPositionAppMinute: {formData.CurrentPositionAppMinute}~"+
                                $"Date of Posting to Current Position: {formData.CurrentPositionPostingDate.ToString("dd, MMMM yyyy")}~"+
                                $"Date of Birth: {formData.DateOfBirth.ToString("dd, MMMM yyyy")}~"+
                                $"Education Service Commission Minute of First Appointment: {formData.FirstAppEscMinute}~"+
                                $"Date of First Appointment Starting with Probation: {formData.FirstProbationAppDate.ToString("dd, MMMM yyyy")}~"+
                                $"Sex: {formData.Gender}~"+
                                $"IPPS Number: {formData.IppsNumber}~"+
                                $"Position When Appointed on Probation: {formData.ProbationAppDesignation}~"+
                                $"Registration Number: {formData.RegistrationNumber}~"+
                                $"School Id: {formData.SchoolId}~"+
                                $"UTS File Number: {formData.UtsFileNumber}~";

                // _teacherFileService.LogRegistration(User.Identity.Name, teacherId, recordDetails);

                TempData["Message"] = "Teacher registered successfully";

                return RedirectToAction("file", routeValues: new{ id = teacherId.ToString() });
            }

            ViewData["Schools"] = _db.Schools
                .Select(l => new SelectListItem{
                    Value = l.SchoolId.ToString(),
                    Text = l.Name
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
                        .SingleOrDefaultAsync(t => t.TeacherId == teacherId);
                    
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
                    teacherForUpdate.CurrentPosition            = formData.CurrentPosition            ;
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

                    // _db.TeacherFiles.Add(new TeacherFile{
                    //     TeacherId = teacherForUpdate.TeacherId,
                    //     RecordDate = DateTime.Now,
                    //     Details = $"Teacher file opened.~~"+
                    //                 "Teacher details:~"+
                    //                 $"Fullname: {formData.Fullname}~"+
                    //                 $"Teacher Id: {teacherForUpdate.TeacherId}~"+
                    //                 $"Current Position: {formData.CurrentPosition}~"+
                    //                 $"Education Service Commission Minute of Appointment: {formData.ConfirmationEscMinute}~"+
                    //                 $"CurrentPositionAppMinute: {formData.CurrentPositionAppMinute}~"+
                    //                 $"Date of Posting to Current Position: {formData.CurrentPositionPostingDate.ToString("dd, MMMM yyyy")}~"+
                    //                 $"Date of Birth: {formData.DateOfBirth.ToString("dd, MMMM yyyy")}~"+
                    //                 $"Education Service Commission Minute of First Appointment: {formData.FirstAppEscMinute}~"+
                    //                 $"Date of First Appointment Starting with Probation: {formData.FirstProbationAppDate.ToString("dd, MMMM yyyy")}~"+
                    //                 $"Sex: {formData.Gender}~"+
                    //                 $"IPPS Number: {formData.IppsNumber}~"+
                    //                 $"Position When Appointed on Probation: {formData.ProbationAppDesignation}~"+
                    //                 $"Registration Number: {formData.RegistrationNumber}~"+
                    //                 $"School Id: {formData.SchoolId}~"+
                    //                 $"UTS File Number: {formData.UtsFileNumber}~"
                    // });

                    await _db.SaveChangesAsync();
                    // await _teacherFileService.LogUpdate(User.Identity.Name, teacherForUpdate.TeacherId);
                }
            }

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
                        });
                    
                    int pageSize = 10;
                    return View(await PaginatedList<TeacherViewModel>.CreateAsync(model.AsNoTracking(), page ?? 1, pageSize));
                }
            }
            return View("SubjectNotFound");
        }
    }
}