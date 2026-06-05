using Metodos;
using Metodos.Unidad_4;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TabajoAnalisisMatematico.Models;

namespace TabajoAnalisisMatematico.Controllers
{
    public class SimpsonController : Controller
    {
        [HttpGet]
        public IActionResult Simpson()
        {
            var modelo = new IntegracionSimple();
            return View(modelo);
        }
        
        [HttpPost]
        public IActionResult Simpson13Simple(IntegracionSimple modelo, string PuntosCargadosOcultos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Simpson simpsonSimple = new Simpson();
                    modelo.Resultado = simpsonSimple.CalcularIntegralSimpson13Simple(modelo.Funcion, modelo.Xi, modelo.Xd);
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
        public IActionResult Simpson13Multiple(IntegracionMultiple modelo, string PuntosCargadosOcultos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Simpson simpsonMultiple = new Simpson();
                    modelo.Resultado = simpsonMultiple.CalcularIntegralSimpson13Multiple(modelo.Funcion, modelo.Xi, modelo.Xd, modelo.N);
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
        public IActionResult Simpson38(IntegracionSimple modelo, string PuntosCargadosOcultos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Simpson simpsonSimple = new Simpson();
                    modelo.Resultado = simpsonSimple.CalcularIntegralSimpson38(modelo.Funcion, modelo.Xi, modelo.Xd);
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
        public IActionResult SimpsonCombinado(IntegracionMultiple modelo, string PuntosCargadosOcultos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Simpson simpsonMultiple = new Simpson();
                    modelo.Resultado = simpsonMultiple.CalcularIntegralSimpsonCombinado(modelo.Funcion, modelo.Xi, modelo.Xd, modelo.N);
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