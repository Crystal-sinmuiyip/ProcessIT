using Microsoft.AspNetCore.Mvc;
using Restaurant.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Restaurant.Areas.Admin.Models;

/// This is the admin controller
namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin,Staff")]
    public class AdministrationAreaController : Controller
    {
        protected readonly ApplicationDbContext _context;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly RoleManager<IdentityRole> _roleManager;


        public AdministrationAreaController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

    }
  
}
