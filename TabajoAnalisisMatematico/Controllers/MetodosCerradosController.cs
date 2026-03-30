using Microsoft.AspNetCore.Mvc;
using TabajoAnalisisMatematico.Models;
using Metodos;



namespace TabajoAnalisisMatematico.Controllers
{
    public class MetodosCerradosController : Controller
    {
        [HttpGet]
        public IActionResult Cerrados()
        {

            return View(new MetodosCerradoAbierto());
        }


        [HttpPost]
        public IActionResult Cerrados(MetodosCerradoAbierto modelo)
        {

            if (ModelState.IsValid)
            {
                modelo.Resultado = MetodosCerrados.AnalizarRaiz(
                    modelo.FuncionTexto,
                    modelo.IteracionesMaximas,
                    modelo.Tolerancia,
                    modelo.Xi,
                    modelo.Xd,
                    modelo.MetodoElegido
                );
            }

            return View("~/Views/Home/Index.cshtml", modelo);
        }
    }
}
