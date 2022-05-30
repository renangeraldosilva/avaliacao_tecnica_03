using Microsoft.AspNetCore.Mvc;

namespace ExercicioEmprestimo.Controllers
{
    public class EmprestimoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
