using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metodos
{
    public class RegresionLineal
    {
        public ResultadoRegresion CalcularRegresionLineal(List<double[]> PuntosCargados)
        {
            double tolerancia = 80.0;
            // Paso 1: Obtener cantidad de puntos
            int n = PuntosCargados.Count;
            if (n < 2)
            {
                throw new Exception("Se necesitan al menos 2 puntos para calcular una recta.");
            }

            // Variables para las sumatorias
            double sumX = 0, sumY = 0, sumXY = 0, sumX2 = 0;

            // Pasos 2, 3, 4 y 5: Calcular sumatorias en un solo bucle
            foreach (var punto in PuntosCargados)
            {
                double x = punto[0];
                double y = punto[1];

                sumX += x;
                sumY += y;
                sumXY += (x * y);
                sumX2 += (x * x);
            }

            // Paso 6: Calcular a1 (Pendiente)
            double denominadorA1 = (n * sumX2) - Math.Pow(sumX, 2);
            if (denominadorA1 == 0)
            {
                throw new Exception("Los puntos están alineados verticalmente. No es posible calcular la regresión lineal.");
            }
            double a1 = ((n * sumXY) - (sumX * sumY)) / denominadorA1;

            // Paso 7: Calcular a0 (Intersección con eje Y)
            double mediaX = sumX / n;
            double mediaY = sumY / n;
            double a0 = mediaY - (a1 * mediaX);

            // Paso 8: Calcular sr y st
            double st = 0;
            double sr = 0;

            foreach (var punto in PuntosCargados)
            {
                double x = punto[0];
                double y = punto[1];

                // 8a. St += (MediaY - Y)^2
                st += Math.Pow(mediaY - y, 2);

                // 8b. Sr += (a1*x + a0 - Y)^2
                sr += Math.Pow((a1 * x) + a0 - y, 2);
            }

            // Paso 9: Calcular coeficiente de correlación r
            double r = 0;
            if (st != 0) // Evitamos división por cero si todos los puntos tienen la misma Y
            {
                r = Math.Sqrt((st - sr) / st) * 100;
            }
            else if (sr == 0)
            {
                r = 100; // Ajuste horizontal perfecto
            }

            // Paso 10: Formatear las salidas para la vista
            // Controlamos el signo de a0 para que no imprima "y = 2x + -3"
            string signoA0 = a0 >= 0 ? "+" : "-";

            // Armamos el string de la función redondeando a 4 decimales como en tu imagen
            string funcion = $"y = {Math.Round(a1, 4)}x {signoA0} {Math.Abs(Math.Round(a0, 4))}";

            string porcentajeR = $"{Math.Round(r, 4)}%";

            string efectividad = r >= tolerancia
                ? "El ajuste es aceptable."
                : "El ajuste no es aceptable.";

            return new ResultadoRegresion
            {
                FuncionObtenida = funcion,
                Correlacion = porcentajeR,
                EfectividadAjuste = efectividad
            };
        }
    }
}
