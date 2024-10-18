using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



   
    public class LecturerController : Controller
    {
        // Lecturer actions

        // Action for submitting a claim
        public IActionResult SubmitClaim()
        {
            return View();
        }

        // Action for tracking claims
        public IActionResult TrackClaims()
        {
            return View();
        }
    }

