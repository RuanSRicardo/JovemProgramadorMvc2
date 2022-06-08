using Microsoft.AspNetCore.Mvc;

namespace JovemProgramadorMvc2.Controllers
{
    public class ConhecimentosController : Controller
    {
        public ConhecimentosController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
