using FinalProject.Areas.AdminArea.ViewModels.PhishingCRUD;
using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
namespace FinalProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
    public class PishingController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IEmailService _emailService;
        private readonly IFileService _fileService;

        public PishingController(AppDbContext appDbContext, IEmailService emailService, IFileService fileService)
        {
            _appDbContext = appDbContext;
            _emailService = emailService;
            _fileService = fileService;
        }
        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Index(PhisingCreateVM phisingCreateVM)
        {
            if (!ModelState.IsValid) return View();
            LinkTracker linkTracker = new LinkTracker();
            linkTracker.isClicked = false;
            linkTracker.userEmail = phisingCreateVM.userEmail;
            _appDbContext.LinkTrackers.Add(linkTracker);
            _appDbContext.SaveChanges();
            string? link = Url.Action(nameof(PhisingEmail), "Pishing", new { phisingCreateVM.userEmail },
                Request.Scheme, Request.Host.ToString());
            if (link == null)
            {
                return NotFound();
            }
            string body = string.Empty;
            string path = "wwwroot/Template/victim.html";
            body = _fileService.ReadFile(path, body);


            body = body.Replace("{{link}}", link);


            string subject = "Verify Email";
            _emailService.Send(phisingCreateVM.userEmail, subject, body);
            return View();
        }
        public async Task<IActionResult> PhisingEmail(string userEmail)
        {
            {
                if (userEmail == null) return NotFound();
                LinkTracker? linkTracker = await _appDbContext.LinkTrackers.FirstOrDefaultAsync(e=>e.userEmail==userEmail);
                if (linkTracker == null) return NotFound("Your link is wrong");
                linkTracker.isClicked = true;
                linkTracker.ClickedAt = DateTime.Now;
                _appDbContext.SaveChanges();
                return Ok("Your GMAIL SUCCESSFULLY RESET");
            }
        }

        
    }
}
