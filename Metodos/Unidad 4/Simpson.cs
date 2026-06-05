using Calculus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metodos.Unidad_4
{
    public class Simpson
    {
        public double CalcularIntegralSimpson13Simple(string funcion, double xi, double xd)
        {
            Calculo calculo = new Calculo();

            if (calculo.Sintaxis(funcion, 'x'))
            {
                double h = (xd - xi) / 2.0;

                return (h / 3.0) *
                       (
                           calculo.EvaluaFx(xi) +
                           4 * calculo.EvaluaFx(xi + h) +
                           calculo.EvaluaFx(xd)
                       );
            }
            else
            {
                throw new Exception("Función mal ingresada");
            }
        }

        public double CalcularIntegralSimpson13Multiple(string funcion, double xi, double xd, int n)
        {
            if (n % 2 != 0)
                throw new Exception("Para Simpson 1/3 múltiple la cantidad de subintervalos debe ser par.");

            Calculo calculo = new Calculo();

            if (calculo.Sintaxis(funcion, 'x'))
            {
                double h = (xd - xi) / n;

                double sumaPares = 0;
                double sumaImpares = 0;

                for (int i = 1; i < n; i++)
                {
                    double fx = calculo.EvaluaFx(xi + h * i);

                    if (i % 2 == 0)
                        sumaPares += fx;
                    else
                        sumaImpares += fx;
                }

                return (h / 3.0) *
                       (
                           calculo.EvaluaFx(xi) +
                           4 * sumaImpares +
                           2 * sumaPares +
                           calculo.EvaluaFx(xd)
                       );
            }
            else
            {
                throw new Exception("Función mal ingresada");
            }
        }

        public double CalcularIntegralSimpson38(string funcion, double xi, double xd)
        {
            Calculo calculo = new Calculo();

            if (calculo.Sintaxis(funcion, 'x'))
            {
                double h = (xd - xi) / 3.0;

                return (3.0 * h / 8.0) *
                       (
                           calculo.EvaluaFx(xi) +
                           3 * calculo.EvaluaFx(xi + h) +
                           3 * calculo.EvaluaFx(xi + 2 * h) +
                           calculo.EvaluaFx(xd)
                       );
            }
            else
            {
                throw new Exception("Función mal ingresada");
            }
        }

        public double CalcularIntegralSimpsonCombinado(string funcion, double xi, double xd, int n)
        {
            Calculo calculo = new Calculo();

            if (calculo.Sintaxis(funcion, 'x'))
            {
                double h = (xd - xi) / n;
                double resultado = 0.0;

                // Si la cantidad de subintervalos es impar,
                // aplicamos Simpson 3/8 a los últimos 3 intervalos
                if (n % 2 != 0)
                {
                    double nuevoXi = xi + h * (n - 3);

                    resultado += CalcularIntegralSimpson38(funcion, nuevoXi, xd);

                    n -= 3;
                }

                // Simpson 1/3 Múltiple para la parte restante
                double sumaImpares = 0.0;
                double sumaPares = 0.0;

                for (int i = 1; i < n; i++)
                {
                    double x = xi + i * h;

                    if (i % 2 == 0)
                        sumaPares += calculo.EvaluaFx(x);
                    else
                        sumaImpares += calculo.EvaluaFx(x);
                }

                double xFinal = xi + n * h;

                resultado += (h / 3.0) *
                              (
                                  calculo.EvaluaFx(xi) +
                                  calculo.EvaluaFx(xFinal) +
                                  4 * sumaImpares +
                                  2 * sumaPares
                              );

                return resultado;
            }
            else
            {
                throw new Exception("Función mal ingresada");
            }
        }
    }
}
