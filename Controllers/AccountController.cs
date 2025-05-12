using Microsoft.AspNetCore.Mvc;

namespace One_Vision.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
