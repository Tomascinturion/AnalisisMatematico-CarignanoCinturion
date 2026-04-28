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
        public IActionResult GS(GaussSeidelModel modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Intentamos calcular
                    modelo.Resultado = GaussSeidel.Gauss_Seidel(modelo.Matriz, modelo.Dimension);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    // Si salta el throw, lo atrapamos y lo mandamos a la vista como un error de validación
                    ModelState.AddModelError("ErrorMetodo", ex.ParamName ?? ex.Message);
                    // Vaciamos el resultado para que no muestre soluciones falsas
                    modelo.Resultado = null;
                }
            }

            return View(modelo);
        }
    }
}