using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Metodos.Unidad_4
{
    public class RegresionPolinomial
    {
        public ResultadoRegresion CalcularRegresionPolinomial(List<double[]> PuntosCargados, int grado)
        {
            double tolerancia = 80.0;
            int n = PuntosCargados.Count;
            int dimension = grado + 1;

            if (n < dimension)
            {
                throw new Exception($"Para un polinomio de grado {grado} se necesitan al menos {dimension} puntos.");
            }

            double[][] matriz = new double[dimension][];
            for (int i = 0; i < dimension; i++)
            {
                matriz[i] = new double[dimension + 1];
            }

            foreach (var punto in PuntosCargados)
            {
                double x = punto[0];
                double y = punto[1];

                for (int fila = 0; fila < dimension; fila++)
                {
                    for (int col = 0; col < dimension; col++)
                    {
                        // Calcula los coeficientes de las incógnitas
                        matriz[fila][col] += Math.Pow(x, fila + col);
                    }
                    // Calcula los términos independientes
                    matriz[fila][dimension] += y * Math.Pow(x, fila);
                }
            }

            //Resolver matriz con Gauss-Jordan 
            double[] vectorResultado = GaussJordan.Gauss_Jordan(matriz);

            //Armar el String de la Función 
            string funcion = string.Empty;

            for (int i = 0; i < vectorResultado.Length; i++)
            {
                double ai = Math.Round(vectorResultado[i], 4);

                if (ai == 0) continue; // Si el coeficiente es 0, no lo escribimos 

                string termino = "";
                double aiAbs = Math.Abs(ai); // Usamos valor absoluto para manejar el signo a mano

                if (i == 0)
                {
                    termino = $"{aiAbs}";
                }
                else if (i == 1)
                {
                    termino = $"{aiAbs}x";
                }
                else
                {
                    termino = $"{aiAbs}x^{i}";
                }

                // Concatenar armando hacia atrás (como pide el PDF)
                if (funcion == string.Empty)
                {
                    funcion = ai < 0 ? $"-{termino}" : termino;
                }
                else
                {
                    string signo = ai > 0 ? " + " : " - ";
                    funcion = termino + signo + funcion;
                }
            }

            funcion = "y = " + funcion;


            //Calcular Correlación (r) 
            double sr = 0;
            double st = 0;
            double sumY = PuntosCargados.Sum(p => p[1]);
            double mediaY = sumY / n;

            foreach (var punto in PuntosCargados)
            {
                double x = punto[0];
                double y = punto[1];
                double suma = 0;

                // Calculamos el valor Y evaluando el polinomio obtenido
                for (int i = 0; i < vectorResultado.Length; i++)
                {
                    suma += vectorResultado[i] * Math.Pow(x, i);
                }

                sr += Math.Pow(suma - y, 2);
                st += Math.Pow(mediaY - y, 2);
            }

            double r = 0;
            if (st != 0)
            {
                r = Math.Sqrt((st - sr) / st) * 100;
            }
            else if (sr == 0)
            {
                r = 100;
            }

            //Devolver el objeto
            string efectividad = r >= tolerancia ? "El ajuste es aceptable." : "El ajuste no es aceptable.";

            return new ResultadoRegresion
            {
                FuncionObtenida = funcion,
                Correlacion = $"{Math.Round(r, 4)}%",
                EfectividadAjuste = efectividad
            };
        }
    }
}
