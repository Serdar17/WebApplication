using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Owin.Security;
using System.Diagnostics;
using WebApplication4.Areas.Identity.Data;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signIn)
        {
            _logger = logger;
            dbContext = context;
            this.userManager = userManager;
            signInManager = signIn;
        }
        public IActionResult Index()
        {
            var users = dbContext.Users.ToList();
            var user = users.FirstOrDefault(item => item.Email.Equals(User.Identity.Name));
            if (user is not null && user.IsBlocked)
                signInManager.SignOutAsync();
            if (User.Identity.IsAuthenticated && user is not null)
                return View(users);
            if (user is null)
                signInManager.SignOutAsync();
            return View(users);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Block(ApplicationUserViewModel model)
        {
            if (model.IsValid())
            {
                await SetOnlineStatusAsync(model);
                SetLockValue(true, model);
            }
            return new JsonResult(Ok());
        }

        [HttpPost]
        public IActionResult Unblock(ApplicationUserViewModel model)
        {
            if (model.IsValid())
                SetLockValue(false, model);
            return new JsonResult(Ok());
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = dbContext.Users.ToList();
            return new JsonResult(Ok(users));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ApplicationUserViewModel model)
        {
            if (model.IsValid())
            {
                await DeleteUsersAsync(model);
            }
            return new JsonResult(Ok());
        }

        private async Task DeleteUsersAsync(ApplicationUserViewModel model)
        {
            foreach (var id in model.Id)
            {
                var user = await userManager.FindByIdAsync(id);
                if (user is not null)
                {
                    await userManager.DeleteAsync(user);
                    if (User.Identity.Name.Equals(user.Email))
                        await SingOutUser();
                    await userManager.ResetAuthenticatorKeyAsync(user);
                }
            }
        }

        private async Task SingOutUser()
        {
            if (User.Identity.IsAuthenticated)
                await signInManager.SignOutAsync();
        }

        private async Task SetOnlineStatusAsync(ApplicationUserViewModel model)
        {
            foreach (var id in model.Id)
            {
                var user = await userManager.FindByIdAsync(id);
                if (user is not null)
                {
                    user.IsOnline = false;
                    if (User.Identity.Name.Equals(user.Email))
                        await SingOutUser();
                }
            }
            //var user = dbContext.Users.FirstOrDefault(item => item.Email.Equals(User.Identity.Name));
            //if (user is not null)
            //    user.IsOnline = false;
        }

        private void SetLockValue(bool isBlocked, ApplicationUserViewModel model)
        {
            var applicationUsers = dbContext.Users;
            foreach (var id in model.Id)
            {
                var user = applicationUsers.FirstOrDefault(item => item.Id.Equals(id));
                user.IsBlocked = isBlocked;
            }
            dbContext.SaveChanges();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}