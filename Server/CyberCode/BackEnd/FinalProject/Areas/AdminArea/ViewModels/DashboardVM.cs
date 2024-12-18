using FinalProject.Models;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Areas.AdminArea.ViewModels
{
    public class DashboardVM
    {

        public List<AppUser> AppUsers { get; set; } = null!;
        public List<IdentityRole> IdentityRoles { get; set; } = null!;
        public List<LinkTracker> LinkTrackers { get; set; } = null!;
        public List<AppUser> appUsers { get; set; } = null!;
    }

}
