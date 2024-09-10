using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class HomeController : Controller
    {
       

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
