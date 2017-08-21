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
    public class SchoolsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SchoolsController(ApplicationDbContext databaseContext)
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

            ViewData["search"] = search;

            var model = _db.Schools
                .Where(s => s.Name.ToLower().Contains(search.ToLower()))
                .Select(s => new SchoolViewModel{
                    SchoolId = s.SchoolId,
                    Name = s.Name,
                    RowVersion = s.RowVersion
                });

            int pageSize = 10;            
            return View(await PaginatedList<SchoolViewModel>.CreateAsync(model.AsNoTracking(), page ?? 1, pageSize));
        }

        public async Task<IActionResult> Register()
        {
            var model = new SchoolViewModel();
            var districtsSelectList = _db.Districts.Select(d => new SelectListItem{
                Value = d.DistrictId.ToString(),
                Text = d.Name
            });

            model.DistrictsSelectList = await districtsSelectList.ToListAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(SchoolViewModel formData)
        {
            if (ModelState.IsValid)
            {
                var registeredSchoolEntry = _db.Schools.Add(new School{
                    Name = formData.Name,
                    DistrictId = formData.DistrictId
                });
                _db.SaveChanges();

                return RedirectToAction("school", routeValues: new { id = registeredSchoolEntry.Entity.SchoolId });
            }

            return View(formData);
        }

        public async Task<IActionResult> School(string id)
        {
            if(!string.IsNullOrWhiteSpace(id))
            {
                Guid schoolId;
                if (Guid.TryParse(id, out schoolId))
                {
                    var model = await _db.Schools
                        .Select(s => new SchoolViewModel{
                            Name = s.Name,
                            SchoolId = s.SchoolId,
                            DistrictId = s.DistrictId == null ? Guid.Empty : Guid.Parse(s.DistrictId.ToString()),
                            RowVersion = s.RowVersion
                        })
                        .SingleOrDefaultAsync(s => s.SchoolId == schoolId);
                    ViewData["DistrictsSelectList"] = await _db.Districts.Select(d => new SelectListItem{
                        Text = d.Name,
                        Value = d.DistrictId.ToString()
                    }).ToListAsync();

                    return View(model);
                }
            }

            return View("SchoolNotFound");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult School(SchoolViewModel formData)
        {
            if(ModelState.IsValid)
            {
                var schoolForUpdate = _db.Schools.SingleOrDefault(s => s.SchoolId == formData.SchoolId);
                if (schoolForUpdate != null)
                {
                    schoolForUpdate.Name = formData.Name;
                    schoolForUpdate.DistrictId = formData.DistrictId;
                    _db.Update(schoolForUpdate);

                    _db.SaveChanges();

                    TempData["Message"] = "Changes saved successfully";

                    return RedirectToAction("school", routeValues: new { id = schoolForUpdate.SchoolId });
                }
                
                ModelState.AddModelError("","School doesn't exist or record is invalid");
            }

            return View(formData);
        }
    }
}