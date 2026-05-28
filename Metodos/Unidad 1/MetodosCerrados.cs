using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculus;

namespace Metodos
{
    public class MetodosCerrados
    {
        public delegate double Funcion(double x);

        private static double CalcularXr(string metodo, Funcion f, double xi, double xd)
        {
            if (metodo.ToLower() == "biseccion")
            {
                return (xi + xd) / 2.0;
            }
            else if (metodo.ToLower() == "regla_falsa")
            {
                return (f(xd) * xi - f(xi) * xd) / (f(xd) - f(xi));
            }
            throw new ArgumentException("Método no válido.");
        }

        public static ResultadoMetodos AnalizarRaiz(string funcionTexto, int iteraciones, double tolerancia, double xi, double xd, string metodo)
        {
            Calculo evaluador = new Calculo();

            if (!evaluador.Sintaxis(funcionTexto, 'x'))
            {
                return new ResultadoMetodos { Exito = false, Mensaje = "Error de sintaxis en la función ingresada. Revisa los operadores." };
            }

            Funcion f = x => evaluador.EvaluaFx(x);

            if (f(xi) * f(xd) > 0)
            {
                return new ResultadoMetodos { Exito = false, Mensaje = "Error: f(xi) y f(xd) tienen el mismo signo. No hay garantía de raíz en este intervalo." };
            }
            else if (f(xi) * f(xd) == 0)
            {
                double raizExacta = (f(xi) == 0) ? xi : xd;
                return new ResultadoMetodos { Exito = true, Raiz = raizExacta, Iteraciones = 0, Error = 0, Mensaje = "Raíz exacta en los límites." };
            }

            double xrAnterior = 0;
            double xr = 0;
            double error = 0;

            for (int i = 1; i <= iteraciones; i++)
            {
                xr = CalcularXr(metodo, f, xi, xd);

                if (xr != 0)
                {
                    error = Math.Abs((xr - xrAnterior) / xr);
                }
                else
                {
                    error = double.PositiveInfinity;
                }

                if (i > 1 && (Math.Abs(f(xr)) < tolerancia || error < tolerancia))
                {
                    return new ResultadoMetodos
                    {
                        Exito = true,
                        Raiz = xr,
                        Iteraciones = i,
                        Error = error,
                        Mensaje = "Raíz encontrada."
                    };
                }

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

            return new ResultadoMetodos
            {
                Exito = false, 
                Raiz = xr,
                Iteraciones = iteraciones,
                Error = error,
                Mensaje = "Se superó el límite de iteraciones sin alcanzar la tolerancia deseada."
            };
        }
    }
  }

