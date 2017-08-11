using EastSeat.TeacherMIS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EastSeat.TeacherMIS.Web.Controllers
{
    public class TeachersController : Controller
    {
        public IActionResult Index(string search)
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(TeacherViewModel formData)
        {
            return View();
        }

        public IActionResult File(string id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult File(TeacherViewModel formData)
        {
            return View();
        }

        public IActionResult School(string id, string search, int? page)
        {
            return View();
        }

        public IActionResult Subject(string id, string search, int? page)
        {
            return View();
        }
    }
}