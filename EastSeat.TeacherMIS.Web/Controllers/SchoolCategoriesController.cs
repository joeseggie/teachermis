using System.Linq;
using System.Threading.Tasks;
using EastSeat.TeacherMIS.Web.Data;
using EastSeat.TeacherMIS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EastSeat.TeacherMIS.Web.Controllers
{
    public class SchoolCategoriesController : Controller
    {
        private readonly ILogger<SchoolCategoriesController> _logger;
        private readonly ApplicationDbContext _db;

        public SchoolCategoriesController(ApplicationDbContext databaseContext, ILogger<SchoolCategoriesController> logger)
        {
            _logger = logger;
            _db = databaseContext;
        }

        public async Task<IActionResult> Index()
        {
            var model = _db.SchoolCategories.Select(c => new SchoolCategoryViewModel{
                SchoolCategoryId = c.SchoolCategoryId,
                Description = c.Description
            });

            return View(await model.ToListAsync());
        }
    }
}