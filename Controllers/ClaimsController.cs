using Microsoft.AspNetCore.Mvc;
using ContractMonthlyClaimSystem.Models;
using System.Collections.Generic;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class ClaimsController : Controller
    {
        // Action to list all claims
        // public IActionResult Index()
        // {
        //     List<Claim> claims = GetClaims(); // Placeholder method to fetch claims
        //     return View(claims);
        // }

        // Action to show the form for submitting a new claim
        // public IActionResult Create()
        // {
        //     return View();
        // }

        // Action to handle the submission of a new claim
        // [HttpPost]
        // public IActionResult Create(Claim claim)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         // Save the claim (this is where backend logic will go)
        //         return RedirectToAction("Index");
        //     }
        //     return View(claim);
        // }

        // Action to show the details of a specific claim
        // public IActionResult Details(int id)
        // {
        //     Claim claim = GetClaimById(id); // Placeholder method to fetch claim by ID
        //     return View(claim);
        // }

        // Action to show the verification and approval interface
        // public IActionResult Verify(int id)
        // {
        //     Claim claim = GetClaimById(id); // Placeholder method to fetch claim by ID
        //     return View(claim);
        // }

        // Placeholder methods to simulate data fetching
        private List<Claim> GetClaims()
        {
            // Sample data for demonstration
            return new List<Claim>
            {
                new Claim { Id = 1, LecturerName = "Tariq Gundu", ClaimDate = DateTime.Now, Amount = 1000, Status = "Pending" },
                new Claim { Id = 2, LecturerName = "Kirren Masha", ClaimDate = DateTime.Now.AddDays(-6), Amount = 1500, Status = "Approved" },
                new Claim { Id = 3, LecturerName = "Notzy Masha", ClaimDate = DateTime.Now.AddDays(-9), Amount = 21500, Status = "Approved" }

            };
        }

        private Claim GetClaimById(int id)
        {
            return GetClaims().Find(c => c.Id == id);
        }
    }
}
