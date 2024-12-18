using FinalProject.Areas.AdminArea.ViewModels;
using FinalProject.DAL;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Areas.AdminArea.Controllers
{
        [Area("AdminArea")]
        [Authorize(Roles ="Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DashboardController(AppDbContext appDbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            DashboardVM dashboardVM = new()
            {
                LinkTrackers = await _appDbContext.LinkTrackers.Where(d => !d.IsDeleted).ToListAsync(),
                AppUsers = await _userManager.Users.ToListAsync(),
            };
            return View(dashboardVM);
        }
        [HttpPost]
        public async Task<IActionResult> DeletePhishingRecord(int id)
        {
            var phishingRecord = await _appDbContext.LinkTrackers.FirstOrDefaultAsync(l => l.Id == id);

            if (phishingRecord == null)
            {
                return NotFound("The record with the specified ID was not found.");
            }

            _appDbContext.LinkTrackers.Remove(phishingRecord);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
