using Metodos;
using Metodos.Unidad_4;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TabajoAnalisisMatematico.Models;

namespace TabajoAnalisisMatematico.Controllers
{
    public class TrapeciosController : Controller
    {
        [HttpGet]
        public IActionResult Trapecio()
        {
            var modelo = new IntegracionSimple();
            return View(modelo);
        }
        
        [HttpPost]
        public IActionResult TrapecioSimple(IntegracionSimple modelo, string PuntosCargadosOcultos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Trapecios trapecioSimple = new Trapecios();
                    modelo.Resultado = trapecioSimple.CalcularIntegralTrapeciosSimple(modelo.Funcion, modelo.Xi, modelo.Xd);
                    ViewBag.Resultado = modelo.Resultado;
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
            }

            return View(modelo);
        }

        [HttpPost]
        public IActionResult TrapecioMultiple(IntegracionMultiple modelo, string PuntosCargadosOcultos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Trapecios trapecioMultiple = new Trapecios();
                    modelo.Resultado = trapecioMultiple.CalcularIntegralTrapeciosMultiple(modelo.Funcion, modelo.Xi, modelo.Xd, modelo.N);
                    ViewBag.Resultado = modelo.Resultado;
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
            }

            return View(modelo);
        }
    }
}
