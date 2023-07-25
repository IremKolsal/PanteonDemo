using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class BuildingController : Controller
    {
        [Authorize]
        //[AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
    }
}
