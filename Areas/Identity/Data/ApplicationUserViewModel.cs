using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication4.Areas.Identity.Data
{
    public class ApplicationUserViewModel
    {
        public ApplicationUser User { get; set; }
        public SelectListItem Item { get; set; }
    }
}
