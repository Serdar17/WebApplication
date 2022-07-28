using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication4.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public DateTime CreateDateTime { get; set; } = DateTime.Now;
    public DateTime LastLoginDateTime { get; set; }
    public bool IsOnline { get; set; }
    public bool IsBlocked { get; set; }
}

