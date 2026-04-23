using System.Diagnostics;
using Metodos;
using Microsoft.AspNetCore.Mvc;
using TabajoAnalisisMatematico.Models;

namespace TabajoAnalisisMatematico.Controllers
{
    public class GaussSeidelController : Controller
    {
        [HttpGet]
        public IActionResult GS()
        {

            return View(new GaussSeidelModel());
        }


        [HttpPost]
        public IActionResult GJ(GaussSeidelModel modelo)
        {
            if (ModelState.IsValid)
            {
                modelo.Resultado = GaussSeidel.Gauss_Seidel(modelo.Matriz, modelo.Dimension);
            }

            return View(modelo);
        }
    }
}