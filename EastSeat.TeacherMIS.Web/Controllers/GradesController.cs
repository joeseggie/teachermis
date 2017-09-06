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
    [Authorize(Roles="Admin,HumanResource,Supervisor")]
    public class GradesController : Controller
    {
        private readonly ILogger<GradesController> _logger;
        private readonly ApplicationDbContext _db;

        public GradesController(ILogger<GradesController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _db = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = _db.Grades.Select(g => new GradeViewModel{
                GradeId = g.GradeId,
                Name = g.Name
            });
            return View(await model.ToListAsync());
        }

        public IActionResult Add()
        {
            return View();            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(GradeViewModel formData)
        {
            if (ModelState.IsValid)
            {
                await _db.Grades.AddAsync(new Grade{
                    Name = formData.Name
                });
                await _db.SaveChangesAsync();

                return RedirectToAction("index");
            }

            return View(formData);
        }
    }
}