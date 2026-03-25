using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metodos
{
    internal class MetodosCerrados
    {
        public delegate double Funcion(double x);

        public static double CalcularXr(string metodo, Funcion f, double xi, double xd)
        {
            if (metodo.ToLower() == "biseccion")
            {
                return (xi + xd) / 2.0;
            }
            else if (metodo.ToLower() == "regla_falsa")
            {
                return (f(xd) * xi - f(xi) * xd) / (f(xd) - f(xi));
            }
            else
            {
                throw new ArgumentException("Método no válido. Usa 'biseccion' o 'regla_falsa'.");
            }
        }

        public static double AnalizarRaiz(Funcion f, int iteraciones, double tolerancia, double xi, double xd, string metodo)
        {
            // 1. Validar el intervalo inicial
            if (f(xi) * f(xd) > 0)
            {
                Console.WriteLine("Error: f(xi) y f(xd) tienen el mismo signo. No se garantiza una raíz.");
                return double.NaN; // Retornamos "Not a Number" como indicador de error
            }
            else if (f(xi) * f(xd) == 0)
            {
                if (f(xi) == 0)
                {
                    Console.WriteLine($"El valor xi ({xi}) es una raíz exacta.");
                    return xi;
                }
                else
                {
                    Console.WriteLine($"El valor xd ({xd}) es una raíz exacta.");
                    return xd;
                }
            }

            // 2. Bucle principal
            double xrAnterior = 0;
            double xr = 0;
            double error = 0;

            for (int i = 1; i <= iteraciones; i++)
            {
                xr = CalcularXr(metodo, f, xi, xd);

                // Calcular el error relativo (evitando división por cero)
                if (xr != 0)
                {
                    error = Math.Abs((xr - xrAnterior) / xr);
                }
                else
                {
                    error = double.PositiveInfinity;
                }

                // Condición de corte: Tolerancia alcanzada
                // Ignoramos la primera iteración para el error, ya que xrAnterior es 0
                if (i > 1 && (Math.Abs(f(xr)) < tolerancia || error < tolerancia))
                {
                    Console.WriteLine($"✅ Raíz encontrada en la iteración {i}");
                    Console.WriteLine($"Error final: {error:F6}");
                    return xr;
                }

                // Actualizar los límites del intervalo
                if (f(xi) * f(xr) > 0)
                {
                    xi = xr;
                }
                else
                {
                    xd = xr;
                }

                xrAnterior = xr;
            }

            // 3. Salida si supera las iteraciones
            Console.WriteLine($"⚠️ Se superó el límite de {iteraciones} iteraciones.");
            return xr;
        }
    }
}
