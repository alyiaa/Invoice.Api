using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
