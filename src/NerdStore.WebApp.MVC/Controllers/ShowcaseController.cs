using Microsoft.AspNetCore.Mvc;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class ShowcaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
