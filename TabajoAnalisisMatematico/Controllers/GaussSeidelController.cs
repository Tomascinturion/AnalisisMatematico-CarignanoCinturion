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
                    GaussSeidel gaussSeidel = new GaussSeidel();
                    // Usar la instancia creada para llamar al método no estático
                    modelo.Resultado = gaussSeidel.Gauss_Seidel(modelo.Matriz, modelo.Dimension);
                }
                catch (Exception ex)
                {
                    // Si tu lógica tira un "throw new...", el código salta automáticamente acá.
                    // Guardamos tu mensaje de error en el ViewBag para mandarlo al HTML.
                    ViewBag.ErrorMessage = ex.Message;
                }
            }

            return View(modelo);
        }
    }
}