using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Authentication.Controllers
{
    public class HomeController : Controller
    {
        private  UserManager<IdentityUser> _userManager { get; }
        private SignInManager<IdentityUser> _signInManager { get; }

        public HomeController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(String username, String password)
        {
            return await SignIn(username, password);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(String username, String password)
        {
            IdentityUser user = new IdentityUser 
            { 
                UserName = username,
                Email = ""
            };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return await SignIn(username, password);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        private async Task<IActionResult> SignIn(String username, String password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}
