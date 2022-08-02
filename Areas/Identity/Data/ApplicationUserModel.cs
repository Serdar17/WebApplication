using System.Web.Mvc;

namespace WebApplication4.Areas.Identity.Data
{
    public class ApplicationUserModel
    {
        public string[] Id { get; set; }
        
        public SelectListItem[] Item  { get; set; }
    }
}
