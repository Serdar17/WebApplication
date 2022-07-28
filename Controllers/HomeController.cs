using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication4.Areas.Identity.Data;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            dbContext = context;
        }
        public IActionResult Index()
        {
            List<ApplicationUserViewModel> viewModels = new List<ApplicationUserViewModel>();
            IEnumerable<ApplicationUser> users = dbContext.Users;
            foreach (var user in users)
                viewModels.Add(new ApplicationUserViewModel { User = user });
            return View(viewModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Block()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteUser(List<ApplicationUserViewModel> userViewModels)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}