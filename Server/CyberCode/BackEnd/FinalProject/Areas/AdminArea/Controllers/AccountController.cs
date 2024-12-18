using FinalProject.Areas.AdminArea.ViewModels.AccountCRUD;
using FinalProject.DAL;
using FinalProject.Extensions;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace FinalProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public AccountController(UserManager<AppUser> userManager, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            AppUser? user;
            if (User.Identity != null && User.Identity.IsAuthenticated && User.Identity.Name != null)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            if (id == null) return NotFound();
            AppUser? user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            AccountUpdateVM accountUpdateVM =new()
            {
                Email = user.Email,
                UserName = user.UserName,
              
                Fullname = user.Fullname
            };
             return View(accountUpdateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, AccountUpdateVM accountUpdateVM)
        {
            if (id == null) return NotFound();
            AppUser? user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            return RedirectToAction("Index");
        }
    }
}