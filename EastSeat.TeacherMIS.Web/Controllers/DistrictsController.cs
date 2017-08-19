using System;
using System.Linq;
using System.Threading.Tasks;
using EastSeat.TeacherMIS.Web.Data;
using EastSeat.TeacherMIS.Web.Models;
using EastSeat.TeacherMIS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EastSeat.TeacherMIS.Web.Controllers
{
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
            if(ModelState.IsValid)
            {
                var newDistrictAdded = await _db.Districts.AddAsync(new District{
                    Name = formData.Name
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
                        RowVersion = d.RowVersion
                    })
                    .SingleOrDefaultAsync(d => d.DistrictId == districtId);

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

                    _db.Update(districtForUpdate);
                    await _db.SaveChangesAsync();

                    return RedirectToAction("Details", new{ id = formData });
                }
            }

            return View(formData);
        }
    }
}