using Microsoft.AspNetCore.Mvc;
using TabajoAnalisisMatematico.Models;

namespace TabajoAnalisisMatematico.Controllers
{
    public class RegresionLinealController : Controller
    {
        [HttpGet]
        public IActionResult RegresionLineal()
        {
            var modelo = new RegresionLinealModel();
            return View(modelo);
        }
        [HttpPost]
        public IActionResult RegresionLineal(RegresionLinealModel modelo, string PuntosCargadosOcultos)
        {
            try
            {
                {
                    // 1. Inicializamos la lista vacía
                    modelo.Puntos = new List<double[]>();

                    // 2. Transformamos el string plano en la lista de arrays
                    if (!string.IsNullOrWhiteSpace(PuntosCargadosOcultos))
                    {
                        // Separamos por punto y coma (cada elemento es un punto entero)
                        string[] lineas = PuntosCargadosOcultos.Split('|');

                        foreach (string linea in lineas)
                        {
                            if (string.IsNullOrWhiteSpace(linea)) continue;

                            // Separamos por coma (para dividir X e Y)
                            string[] coordenadas = linea.Split(';');

                            if (coordenadas.Length == 2)
                            {
                                // Parseamos forzando la coma para que tu Windows no tire error
                                double x = Convert.ToDouble(coordenadas[0].Trim().Replace(".", ","));
                                double y = Convert.ToDouble(coordenadas[1].Trim().Replace(".", ","));

                                modelo.Puntos.Add(new double[] { x, y });
                            }
                        }
                    }

                    // 3. Validamos que el usuario no haya tocado calcular sin ingresar datos
                    if (modelo.Puntos.Count < 2)
                    {
                        throw new Exception("Debe ingresar al menos 2 puntos para calcular la recta.");
                    }
                    var calculadora = new Metodos.RegresionLineal();

                    modelo.Resultado = calculadora.CalcularRegresionLineal(modelo.Puntos);

                    modelo.MensajeError = null;
                }
            }
            catch (Exception ex)
            {
                modelo.MensajeError = ex.Message;
            }

            return View(modelo);
        }
    }
}
