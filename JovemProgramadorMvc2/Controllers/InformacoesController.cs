using Microsoft.AspNetCore.Mvc;

namespace JovemProgramadorMvc2.Controllers
{
    public class InformacoesController : Controller
    {
        public InformacoesController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
