using Microsoft.AspNetCore.Mvc;

namespace ExercicioEmprestimo.Controllers
{
    public class ParcelasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
