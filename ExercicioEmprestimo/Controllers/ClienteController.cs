using Microsoft.AspNetCore.Mvc;

namespace ExercicioEmprestimo.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
