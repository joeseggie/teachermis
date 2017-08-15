using System;
using System.Linq;
using System.Threading.Tasks;
using EastSeat.TeacherMIS.Web.Data;
using EastSeat.TeacherMIS.Web.Helpers;
using EastSeat.TeacherMIS.Web.Models;
using EastSeat.TeacherMIS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EastSeat.TeacherMIS.Web.Controllers
{
    [Authorize]
    public class SubjectsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SubjectsController(ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
        }

        public async Task<IActionResult> Index(string search, int? page)
        {
            if (search != null)
            {
                page = 1;
            }

            if(string.IsNullOrWhiteSpace(search))
            {
                return View(null);
            }

            var model = _db.Subjects
                .Where(s => s.Description.ToLower().Contains(search.ToLower()))
                .Select(s => new SubjectViewModel{
                    SubjectId = s.SubjectId,
                    Description = s.Description,
                    SubjectCategoryId = s.SubjectCategoryId == null ? Guid.Empty : Guid.Parse(s.SubjectCategoryId.ToString()),
                    SubjectCategory = s.SubjectCategory == null ? string.Empty : s.SubjectCategory.Description,
                    SubjectCategoryStub = s.SubjectCategory.Stub,
                    RowVersion = s.RowVersion
                });

            ViewData["search"] = search;
            int pageSize = 10;            
            return View(await PaginatedList<SubjectViewModel>.CreateAsync(model.AsNoTracking(), page ?? 1, pageSize));
        }

        public async Task<IActionResult> New()
        {
            await LoadSubjectCategoriesSelectItemListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(SubjectViewModel formData)
        {
            if (ModelState.IsValid)
            {
                var resultEntry = await _db.Subjects.AddAsync(new Subject{
                    Description = formData.Description,
                    SubjectCategoryId = formData.SubjectCategoryId
                });
                await _db.SaveChangesAsync();

                return RedirectToAction("details", routeValues: new { id = resultEntry.Entity.SubjectId });
            }

            await LoadSubjectCategoriesSelectItemListAsync();
            return View(formData);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                Guid subjectId;
                if(Guid.TryParse(id, out subjectId))
                {
                    var model = await _db.Subjects
                        .Select(s => new SubjectViewModel{
                            SubjectId = s.SubjectId,
                            Description = s.Description,
                            SubjectCategoryId = s.SubjectCategoryId == null ? Guid.Empty : Guid.Parse(s.SubjectCategoryId.ToString()),
                            SubjectCategory = s.SubjectCategory == null ? string.Empty : s.SubjectCategory.Description,
                            SubjectCategoryStub = s.SubjectCategory.Stub,
                            RowVersion = s.RowVersion
                        })
                        .SingleOrDefaultAsync(s => s.SubjectId == subjectId);

                    await LoadSubjectCategoriesSelectItemListAsync();
                    return View(model);
                }
            }

            return View("SubjectNotFound");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(SubjectViewModel formData)
        {
            if(ModelState.IsValid)
            {
                var subjectForUpdate = _db.Subjects.SingleOrDefault(s => s.SubjectId == formData.SubjectId);
                if (subjectForUpdate != null)
                {
                    subjectForUpdate.Description = formData.Description;
                    subjectForUpdate.SubjectCategoryId = formData.SubjectCategoryId;

                    _db.Update(subjectForUpdate);

                    await _db.SaveChangesAsync();

                    TempData["Message"] = "Subject details updated successfully.";

                    return RedirectToAction("details", routeValues: new { id = subjectForUpdate.SubjectId });
                }

                ModelState.AddModelError("","Subject doesn't exist or record is invalid");
            }

            await LoadSubjectCategoriesSelectItemListAsync();
            return View(formData);
        }

        private async Task LoadSubjectCategoriesSelectItemListAsync()
        {
            var subjectCategoryList = _db.SubjectCategories.Select(c => new SelectListItem{
                Value = c.SubjectCategoryId.ToString(),
                Text = c.Description
            });

            ViewData["SubjectCategories"] = await subjectCategoryList.ToListAsync();
        }
    }
}