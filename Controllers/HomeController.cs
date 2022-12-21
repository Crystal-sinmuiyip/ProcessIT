using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using System.Diagnostics;
using Restaurant.Data;
using Microsoft.AspNetCore.Identity;
using Restaurant.Areas.Admin.Models;

namespace Restaurant.Controllers
{

    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {

            foreach (string role in new string[] { "Admin", "Staff", "Public" })
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }



            if (User.Identity.IsAuthenticated)
            {
                var u = User;
                var user = await _userManager.GetUserAsync(User);
                if (User.Identity.Name == "c.yip@com")
                {
                    if (!User.IsInRole("Admin"))
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                }
            }

            return View();
        }
    }
}


