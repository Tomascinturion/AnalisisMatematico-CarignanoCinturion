using Metodos.Unidad_4;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TabajoAnalisisMatematico.Models;

namespace TabajoAnalisisMatematico.Controllers
{
    public class RegresionPolinomialController : Controller
    {
        [HttpGet]
        public IActionResult RegresionPolinomial()
        {
            var modelo = new RegresionPolinomialModel();
            return View(modelo);
        }
        [HttpPost]
        public IActionResult RegresionPolinomial(RegresionPolinomialModel modelo, string PuntosCargadosOcultos)
        {
            try
            {
                // 1. Limpiamos errores de cálculos anteriores
                modelo.MensajeError = null;

                modelo.Puntos = new List<double[]>();

                // 2. Transformamos el string plano en la lista de arrays (¡El mismo cambio que en la Lineal!)
                if (!string.IsNullOrWhiteSpace(PuntosCargadosOcultos))
                {
                    string[] lineas = PuntosCargadosOcultos.Split('|');

                    foreach (string linea in lineas)
                    {
                        if (string.IsNullOrWhiteSpace(linea)) continue;

                        string[] coordenadas = linea.Split(';');

                        if (coordenadas.Length == 2)
                        {
                            // Parseamos forzando la coma por tu configuración de Windows
                            double x = Convert.ToDouble(coordenadas[0].Trim().Replace(".", ","));
                            double y = Convert.ToDouble(coordenadas[1].Trim().Replace(".", ","));

                            modelo.Puntos.Add(new double[] { x, y });
                        }
                    }
                }

                // 3. Validamos la cantidad de puntos según el grado
                int dimension = modelo.Grado + 1;
                if (modelo.Puntos.Count < dimension)
                {
                    throw new Exception($"Para un polinomio de grado {modelo.Grado} se necesitan al menos {dimension} puntos.");
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
