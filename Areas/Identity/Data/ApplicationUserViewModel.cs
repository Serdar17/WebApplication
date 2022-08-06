using System.Web.Mvc;

namespace WebApplication4.Areas.Identity.Data
{
    public class ApplicationUserViewModel
    {
        public string[] Id { get; set; }
        
        public SelectListItem[] Item  { get; set; }

        public bool IsValid() => Id is not null && Id.Length > 0;

        public static string GetStatusMessage(bool status) => status ? "Да" : "Нет";
    }
}
