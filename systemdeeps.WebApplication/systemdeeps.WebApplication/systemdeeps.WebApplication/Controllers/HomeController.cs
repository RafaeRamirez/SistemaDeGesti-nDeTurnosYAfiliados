using Microsoft.AspNetCore.Mvc;

namespace systemdeeps.WebApplication.Controllers
{
    // Simple landing page to navigate quickly
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}