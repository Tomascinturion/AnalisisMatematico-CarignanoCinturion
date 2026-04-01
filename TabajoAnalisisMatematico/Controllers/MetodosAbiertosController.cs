using System.Diagnostics;
using Metodos;
using Microsoft.AspNetCore.Mvc;
using TabajoAnalisisMatematico.Models;

namespace TabajoAnalisisMatematico.Controllers
{
    public class MetodosAbiertosController : Controller
    {
        [HttpGet]
        public IActionResult Abiertos()
        {

            return View(new MetodosCerradoAbierto());
        }


        [HttpPost]
        public IActionResult Abiertos(MetodosCerradoAbierto modelo)
        {
            MetodosAbiertos metodosAbiertos = new MetodosAbiertos();

            if (ModelState.IsValid)
            {
                modelo.Resultado = metodosAbiertos.Calcular(
                    modelo.FuncionTexto,
                    modelo.MetodoElegido,
                    modelo.Xi,
                    modelo.Xd,
                    modelo.IteracionesMaximas,
                    modelo.Tolerancia);
            }

            return View("~/Views/Home/Index.cshtml", modelo);
        }
    }
}