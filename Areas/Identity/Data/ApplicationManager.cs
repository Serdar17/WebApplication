using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Owin;

namespace WebApplication4.Areas.Identity.Data
{
    public class ApplicationManager /*: UserManager<ApplicationUser>*/
    {
        //public ApplicationManager(IUserStore<ApplicationUser> store) : base(store)
        //{
        //}
        //public static ApplicationManager Create(IdentityFactoryOptions<ApplicationManager> options,
        //IOwinContext context)
        //{
        //    var db = context.Get<ApplicationDbContext>();
        //    ApplicationManager manager = new ApplicationManager(new UserStore<ApplicationUser>(db));
        //    return manager;
        //}
    }
}
