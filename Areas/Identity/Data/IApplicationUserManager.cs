namespace WebApplication4.Areas.Identity.Data
{
    public interface IApplicationUserManager
    {
        IEnumerable<ApplicationUser> Get();

        ApplicationUser Get(string id);

        Task DeleteUsersAsync(ApplicationUserViewModel model);

        Task SetLockValueAsync(bool lockValue, ApplicationUserViewModel model);

        void SetOnlineStatus(ApplicationUserViewModel model);
    }
}
