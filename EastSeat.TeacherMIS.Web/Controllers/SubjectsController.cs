using System;
using System.Linq;
using System.Threading.Tasks;
using EastSeat.TeacherMIS.Web.Data;
using EastSeat.TeacherMIS.Web.Helpers;
using EastSeat.TeacherMIS.Web.Models;
using EastSeat.TeacherMIS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EastSeat.TeacherMIS.Web.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SubjectsController(ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
        }

        public async Task<IActionResult> IndexAsync(string search, int? page)
        {
            if (search != null)
            {
                page = 1;
            }

            var model = _db.Subjects
                .Select(s => new SubjectViewModel{
                    SubjectId = s.SubjectId,
                    Description = s.Description,
                    RowVersion = s.RowVersion
                });

            int pageSize = 10;            
            return View(await PaginatedList<SubjectViewModel>.CreateAsync(model.AsNoTracking(), page ?? 1, pageSize));
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(SubjectViewModel formData)
        {
            if (ModelState.IsValid)
            {
                _db.Subjects.Add(new Subject{
                    Description = formData.Description
                });
                _db.SaveChanges();

                return RedirectToAction("index");
            }

            return View(formData);
        }

        public IActionResult Details(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                Guid subjectId;
                if(Guid.TryParse(id, out subjectId))
                {
                    var model = _db.Subjects
                        .Select(s => new SubjectViewModel{
                            SubjectId = s.SubjectId,
                            Description = s.Description,
                            RowVersion = s.RowVersion
                        })
                        .SingleOrDefault(s => s.SubjectId == subjectId);

                    return View(model);
                }
            }

            return View("SubjectNotFound");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(SubjectViewModel formData)
        {
            if(ModelState.IsValid)
            {
                var subjectForUpdate = _db.Subjects.Find(formData.SubjectId);
                if (subjectForUpdate != null)
                {
                    subjectForUpdate.Description = formData.Description;
                    _db.Update(subjectForUpdate);

                    _db.SaveChanges();
                }

                ModelState.AddModelError("","Subject doesn't exist or record is invalid");
            }

            return View(formData);
        }
    }
}