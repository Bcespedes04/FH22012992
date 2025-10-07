using Microsoft.AspNetCore.Mvc;
using BinarioApp.Models;

namespace BinarioApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // Form vacío
            return View(new BinaryViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(BinaryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            vm.Results = BinaryCalculator.ComputeAll(vm.A, vm.B);
            return View(vm);
        }
    }
}

