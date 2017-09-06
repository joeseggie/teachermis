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
using System.Text.RegularExpressions;

namespace EastSeat.TeacherMIS.Web.Controllers
{
    [Authorize(Roles = "Admin,Supervisor,HumanResource")]
    public class SubjectCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SubjectCategoryController(ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
        }

        public async Task<IActionResult> Index()
        {
            var model = _db.SubjectCategories
                .Select(c => new SubjectCategoryViewModel{
                    SubjectCategoryId = c.SubjectCategoryId,
                    Description = c.Description,
                    Stub = c.Stub,
                    Salary = c.Salary??0,
                    RowVersion = c.RowVersion
                });

            return View(await model.ToListAsync());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(SubjectCategoryViewModel formData)
        {
            if (ModelState.IsValid)
            {
                string stub = Regex.Replace(formData.Description,@"[^A-Za-z0-9\s]", "-").ToLower();
                var newSubjectCategory  = await _db.AddAsync(new SubjectCategory{
                    Description = formData.Description,
                    Salary = formData.Salary,
                    Stub = stub
                });
                var subjectCategoriesAddedCount = await _db.SaveChangesAsync();

                TempData["Message"] = $"{subjectCategoriesAddedCount} subject category added successfully.";

                return RedirectToAction("category", routeValues: new { id = newSubjectCategory.Entity.Stub});
            }

            return View(formData);
        }

        public async Task<IActionResult> Category(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var model = await _db.SubjectCategories
                    .Select(c => new SubjectCategoryViewModel{
                        SubjectCategoryId = c.SubjectCategoryId,
                        Description = c.Description,
                        Stub = c.Stub,
                        Salary = c.Salary??0,
                        RowVersion = c.RowVersion
                    })
                    .SingleOrDefaultAsync(c => c.Stub == id);

                if (model != null)
                {
                    return View(model);
                }
            }

            return View("SubjectCategoryNotFound");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Category(SubjectCategoryViewModel formData)
        {
            if (ModelState.IsValid)
            {
                var subjectCategoryToUpdate = await _db.SubjectCategories.SingleOrDefaultAsync(c => c.SubjectCategoryId == formData.SubjectCategoryId);

                if (subjectCategoryToUpdate != null)
                {
                    subjectCategoryToUpdate.Description = formData.Description;
                    subjectCategoryToUpdate.Stub = Regex.Replace(formData.Description,@"[^A-Za-z0-9\s]", "-").ToLower();
                    subjectCategoryToUpdate.Salary = formData.Salary;

                    _db.Update(subjectCategoryToUpdate);
                    var subjectCategoryUpdatedCount = await _db.SaveChangesAsync();

                    TempData["Message"] = $"{subjectCategoryUpdatedCount} subject category updated successfully.";

                    return RedirectToAction("category", routeValues: new{ id = subjectCategoryToUpdate.Stub });
                }

                ModelState.AddModelError("", "Subject category doesn't exist or is invalid");
            }

            return View(formData);
        }
    }    
}