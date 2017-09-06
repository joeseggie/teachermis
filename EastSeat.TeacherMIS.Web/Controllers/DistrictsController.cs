using System;
using System.Linq;
using System.Threading.Tasks;
using EastSeat.TeacherMIS.Web.Data;
using EastSeat.TeacherMIS.Web.Models;
using EastSeat.TeacherMIS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EastSeat.TeacherMIS.Web.Controllers
{
    [Authorize(Roles = "Admin,Supervisor,HumanResource")]
    public class DistrictsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<DistrictsController> _logger;

        public DistrictsController(ILogger<DistrictsController> logger, ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string search)
        {
            if(!string.IsNullOrWhiteSpace(search))
            {
                var model = _db.Districts
                .Where(d => d.Name.ToLower().Contains(search.ToLower()))
                .Select(d => new DistrictViewModel{
                    DistrictId = d.DistrictId,
                    Name = d.Name,
                    WageAllocation = d.WageAllocation??0,
                    RowVersion = d.RowVersion
                });

                return View(await model.ToListAsync());
            }
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(DistrictViewModel formData)
        {
            ModelState.Remove("RowVersion");
            if(ModelState.IsValid)
            {
                var newDistrictAdded = await _db.Districts.AddAsync(new District{
                    Name = formData.Name,
                    WageAllocation = formData.WageAllocation
                });

                var savedChangesCount = await _db.SaveChangesAsync();

                return RedirectToAction("details", new{ id = newDistrictAdded.Entity.DistrictId });
            }

            return View(formData);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                Guid districtId;
                if(Guid.TryParse(id, out districtId))
                {
                    var model = await _db.Districts.Select(d => new DistrictViewModel{
                        DistrictId = d.DistrictId,
                        Name = d.Name,
                        WageAllocation = d.WageAllocation??0,
                        RowVersion = d.RowVersion
                    })
                    .SingleOrDefaultAsync(d => d.DistrictId == districtId);
                    model.Schools = _db.Schools
                    .Where(s => s.DistrictId == districtId)
                    .Select(s => new SchoolViewModel{
                        Name = s.Name,
                        SchoolId = s.SchoolId,
                        SchoolTeachers = s.Teachers
                        .Where(t => t.School.DistrictId == districtId)
                        .Select(t => new TeacherViewModel{
                            Fullname = t.Fullname
                        })
                    });
                    return View(model);
                }
            }
            return View("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(DistrictViewModel formData)
        {
            if(ModelState.IsValid)
            {
                var districtForUpdate = await _db.Districts.SingleOrDefaultAsync(d => d.DistrictId == formData.DistrictId);
                if(districtForUpdate == null)
                {
                    ModelState.AddModelError("", "District with Id doesn't exist or is invalid");
                }
                else
                {
                    districtForUpdate.Name = formData.Name;
                    districtForUpdate.WageAllocation = formData.WageAllocation;

                    _db.Update(districtForUpdate);
                    await _db.SaveChangesAsync();

                    TempData["Message"] = "Changes saved successfully";

                    return RedirectToAction("Details", new{ id = districtForUpdate.DistrictId });
                }
            }

            return View(formData);
        }
    }
}