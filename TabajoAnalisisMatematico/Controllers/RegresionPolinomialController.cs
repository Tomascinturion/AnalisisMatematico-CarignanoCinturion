using Metodos.Unidad_4;
using Microsoft.AspNetCore.Mvc;
using TabajoAnalisisMatematico.Models;

namespace TabajoAnalisisMatematico.Controllers
{
    public class RegresionPolinomialController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new RegresionPolinomialModel());
        }
        [HttpPost]
        public IActionResult RegresionPolinomial(RegresionPolinomialModel modelo)
        {
            try
            {
                // 1. Limpiamos errores de cálculos anteriores
                modelo.MensajeError = null;

                // 2. Validamos que haya puntos cargados
                if (modelo.Puntos == null || modelo.Puntos.Count == 0)
                {
                    throw new Exception("Por favor, ingrese al menos un punto antes de calcular.");
                }

                // 3. Instanciamos tu clase matemática
                var calculadora = new RegresionPolinomial();

                // 4. Vamos directo al cálculo polinomial usando el grado que eligió el usuario
                modelo.Resultado = calculadora.CalcularRegresionPolinomial(
                    modelo.Puntos,
                    modelo.Grado
                );
            }
            catch (Exception ex)
            {
                modelo.MensajeError = ex.Message;
            }
            return View(modelo);
        }
    }
}
