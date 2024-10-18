using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using ContractMonthlyClaimSystem.Models;
using System.Linq;
using System.Collections.Generic;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display the Login View
        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Claims/Login.cshtml");
        }

        // Handle Login Post
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u =>
                    u.Username == model.Username &&
                    u.Password == model.Password &&
                    u.Role == model.Role);

                if (user != null)
                {
                    // Fully qualify the Claim type to avoid ambiguity
                    var claims = new List<System.Security.Claims.Claim>
                    {
                        new System.Security.Claims.Claim(ClaimTypes.Name, user.Username),
                        new System.Security.Claims.Claim(ClaimTypes.Role, user.Role)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // Redirect to different pages based on the role
                    if (user.Role == "Admin")
                    {
                        return RedirectToAction("AdminDashboard", "Home");
                    }
                    else if (user.Role == "Lecturer")
                    {
                        return RedirectToAction("LecturerDashboard", "Home");
                    }
                }

                ModelState.AddModelError("", "Invalid username, password, or role.");
            }

            return View("~/Views/Claims/Login.cshtml", model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Claims/Register.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Username == model.Username);

                if (existingUser == null)
                {
                    _context.Users.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError("", "Username already taken");
            }

            return View("~/Views/Claims/Register.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
