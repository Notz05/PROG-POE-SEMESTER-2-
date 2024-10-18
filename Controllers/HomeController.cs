using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class HomeController : Controller
    {
        // Index Action
        public IActionResult Index()
        {
            // Determine if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Get the user's role
                var userRole = User.FindFirstValue(ClaimTypes.Role);

                // Redirect based on role
                if (userRole == "Admin")
                {
                    return RedirectToAction("AdminDashboard");
                }
                else if (userRole == "Lecturer")
                {
                    return RedirectToAction("LecturerDashboard");
                }
            }

            // If not authenticated or no role, show a generic home page
            return View();
        }

        // Admin Dashboard Action
        [Authorize(Roles = "Admin")] // Only allow Admins
        public IActionResult AdminDashboard()
        {
            
            return View();
        }

        // Lecturer Dashboard Action
        [Authorize(Roles = "Lecturer")] // Only allow Lecturers
        public IActionResult LecturerDashboard()
        {
            
            return View();
        }

        // Privacy Action
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
