using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageFinances.Areas.User
{
    [Area("User")]
    [Authorize(Roles = "User")]
    [Route("/dashboard")]
    public class HomeController : Controller
    { 
        public IActionResult Index()
        {
            return View();
        }
    }
}
