using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace ContractMonthlyClaimSystem.Controllers
{
   
    public class ClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Claims
        public async Task<IActionResult> Index(string searchString)
        {
            var claims = _context.Claims.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                claims = claims.Where(c => c.LecturerName.Contains(searchString));
            }

            var viewModel = new ClaimViewModel
            {
                Claims = await claims.ToListAsync(),
                SearchString = searchString,
                SuccessMessage = TempData["SuccessMessage"] as string // Get success message
            };

            return View(viewModel);
        }

        // GET: Claims/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Claims/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Claim claim, IFormFile SupportingDocument)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload
                if (SupportingDocument != null && SupportingDocument.Length > 0)
                {
                    try
                    {
                        var fileName = Path.GetFileName(SupportingDocument.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/documents", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await SupportingDocument.CopyToAsync(stream);
                        }

                        claim.SupportingDocumentPath = $"/documents/{fileName}";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error uploading file: {ex.Message}");
                        ModelState.AddModelError("", "Error uploading file. Please try again.");
                        return View(claim); // Return to the form with the current data
                    }
                }

                // Set default status if not provided
                claim.Status = claim.Status ?? "Pending";

                // Add claim to the context
                _context.Claims.Add(claim);

                try
                {
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Claim submitted successfully.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving claim: {ex.Message}");
                    ModelState.AddModelError("", "Unable to save claim. Please try again.");
                }
            }

            return View(claim); // Return to the form with validation messages
        }

        // GET: Claims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

        // POST: Claims/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LecturerName,ClaimDate,Amount,Status,SupportingDocumentPath")] Claim claim)
        {
            if (id != claim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claim);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Claim updated successfully.";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimExists(claim.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating claim: {ex.Message}");
                    ModelState.AddModelError("", "Unable to update claim. Please try again.");
                }
            }
            return View(claim);
        }

        // GET: Claims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.Claims.FirstOrDefaultAsync(m => m.Id == id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

        // POST: Claims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim != null)
            {
                _context.Claims.Remove(claim);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Claim deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Claim not found.";
            }
            return RedirectToAction("Index");
        }

        // GET: Claims/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }

            var viewModel = new ClaimViewModel
            {
                ClaimDetails = claim, // Assign the found claim to ClaimDetails
                SuccessMessage = TempData["SuccessMessage"] as string // Get success message if any
            };

            return View(viewModel); // Return the view with the claim details
        }

        private bool ClaimExists(int id)
        {
            return _context.Claims.Any(e => e.Id == id);
        }
    }
}
