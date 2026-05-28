using Microsoft.AspNetCore.Mvc;
using TabajoAnalisisMatematico.Models;

namespace TabajoAnalisisMatematico.Controllers
{
    public class RegresionLinealController : Controller
    {
        [HttpGet]
        public IActionResult RegresionLineal()
        {
            var modeloVista = new RegresionLinealModel();
            return View(modeloVista);
        }
        [HttpPost]
        public IActionResult RegresionLineal(RegresionLinealModel modeloVista)
        {
            try
            {
                var calculadora = new Metodos.RegresionLineal();

                modeloVista.Resultado = calculadora.CalcularRegresionLineal(modeloVista.Puntos);

                modeloVista.MensajeError = null;
            }
            catch (Exception ex)
            {
                modeloVista.MensajeError = ex.Message;
            }

            return View(modeloVista);
        }
    }
}
