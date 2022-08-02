﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebApplication4.Areas.Identity.Data;

namespace WebApplication4.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly ApplicationDbContext dbContext;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, ApplicationDbContext dbContext)
        {
            _signInManager = signInManager;
            _logger = logger;
            this.dbContext = dbContext; 
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await SetOnlineStatusAsync(User.Identity.Name);
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPage();
            }
        }

        private async Task SetOnlineStatusAsync(string email)
        {
            
            var user = dbContext.Users.Find(email);
            if (user is not null)
                user.IsOnline = false;
            await dbContext.SaveChangesAsync();
        }
    }
}
