using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace Foundations.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var FoundationClaim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "TheFoundation"),
                new Claim(ClaimTypes.Email, "Omid.Naghizadeh@icloud.com")
            };

            var GoogleClaim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "TheGoogleClaim"),
                new Claim(ClaimTypes.Email, "Omid.Naghizadeh@gmail.com")
            };

            var TheIdentity = new ClaimsIdentity(FoundationClaim, "FoundationClaim");
            var TheGoogleIdentity = new ClaimsIdentity(GoogleClaim, "TheGoogleClaim");


            var UserPrinciple = new ClaimsPrincipal(new[] { TheIdentity, TheGoogleIdentity });

            HttpContext.SignInAsync(UserPrinciple);

            return RedirectToAction("Index");
        }
    }
}
