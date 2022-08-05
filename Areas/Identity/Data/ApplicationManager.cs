using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Owin;

namespace WebApplication4.Areas.Identity.Data
{
    public class ApplicationManager : IApplicationUserManager
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationManager(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;           // this.userManager = userManager;
        }

        public async Task DeleteUsersAsync(ApplicationUserViewModel model)
        {
            var users = new List<ApplicationUser>();
            foreach (var id in model.Id)
            {
                var user = await userManager.FindByIdAsync(id);
                if (user is not null)
                {
                    await userManager.DeleteAsync(user);
                    await userManager.ResetAuthenticatorKeyAsync(user);
                }
            }
        }

        public IEnumerable<ApplicationUser> Get() => dbContext.Users;

        public ApplicationUser Get(string id)
        {
            return Get().FirstOrDefault(user => user.Id.Equals(id, StringComparison.Ordinal));
        }

        public async Task SetLockValueAsync(bool lockValue, ApplicationUserViewModel model)
        {
            var applicationUsers = Get();
            foreach (var id in model.Id)
            {
                var user = applicationUsers.FirstOrDefault(item => item.Id.Equals(id));
                user.IsBlocked = lockValue;
                await userManager.ResetAuthenticatorKeyAsync(user);
            }
            dbContext.SaveChanges();
        }

        public void SetOnlineStatus(ApplicationUserViewModel model)
        {
            foreach (var id in model.Id)
            {
                var user = Get(id);
                if (user is not null)
                    user.IsOnline = false;
            }
        }

        public static string GetStatusMessage(bool status) => status ? "Да" : "Нет";
    }
}
