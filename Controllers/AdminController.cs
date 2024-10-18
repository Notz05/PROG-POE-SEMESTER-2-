using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


public class AdminController : Controller
{
    // Admin actions

    // View all claims
    public IActionResult ViewAllClaims()
    {
        return View();
    }

    // Approve or reject claims
    public IActionResult ApproveRejectClaims()
    {
        return View();
    }
}
