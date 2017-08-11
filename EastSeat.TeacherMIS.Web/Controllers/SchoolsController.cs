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
    public class SchoolsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SchoolsController(ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
        }

        public async Task<IActionResult> IndexAsync(string search, int? page)
        {
            if (search != null)
            {
                page = 1;
            }

            var model = _db.Schools
                .Select(s => new SchoolViewModel{
                    SchoolId = s.SchoolId,
                    Name = s.Name,
                    RowVersion = s.RowVersion
                });

            int pageSize = 10;            
            return View(await PaginatedList<SchoolViewModel>.CreateAsync(model.AsNoTracking(), page ?? 1, pageSize));
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(SchoolViewModel formData)
        {
            if (ModelState.IsValid)
            {
                _db.Schools.Add(new School{
                    Name = formData.Name
                });
                _db.SaveChanges();

                return RedirectToAction("index");
            }

            return View(formData);
        }

        public IActionResult School(string id)
        {
            if(!string.IsNullOrWhiteSpace(id))
            {
                Guid schoolId;
                if (Guid.TryParse(id, out schoolId))
                {
                    var model = _db.Schools
                        .Select(s => new SchoolViewModel{
                            Name = s.Name,
                            SchoolId = s.SchoolId,
                            RowVersion = s.RowVersion
                        })
                        .SingleOrDefault(s => s.SchoolId == schoolId);

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
                var schoolForUpdate = _db.Schools.Find(formData.SchoolId);
                if (schoolForUpdate != null)
                {
                    schoolForUpdate.Name = formData.Name;
                    _db.Update(schoolForUpdate);

                    _db.SaveChanges();
                }

                ModelState.AddModelError("","School doesn't exist or record is invalid");
            }

            return View(formData);
        }
    }
}