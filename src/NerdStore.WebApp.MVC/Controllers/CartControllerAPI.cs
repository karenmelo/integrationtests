using Microsoft.AspNetCore.Mvc;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class CartControllerAPI : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
