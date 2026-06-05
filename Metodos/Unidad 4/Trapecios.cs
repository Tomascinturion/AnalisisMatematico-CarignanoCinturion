using Calculus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metodos.Unidad_4
{
    public class Trapecios
    {
        public double CalcularIntegralTrapeciosSimple(string funcion, double xi, double xd)
        {
            Calculo calculo = new Calculo();

            if (calculo.Sintaxis(funcion, 'x'))
            {
                double fxi = calculo.EvaluaFx(xi);
                double fxd = calculo.EvaluaFx(xd);

                return ((fxi + fxd) * (xd - xi)) / 2.0;
            }
            else
            {
                throw new Exception("Función mal ingresada");
            }
        }

        public double CalcularIntegralTrapeciosMultiple(string funcion, double xi, double xd, int n)
        {
            Calculo calculo = new Calculo();

            if (calculo.Sintaxis(funcion, 'x'))
            {
                double h = (xd - xi) / n;
                double suma = 0;

                for (int i = 1; i < n; i++)
                {
                    suma += calculo.EvaluaFx(xi + h * i);
                }

                return (h / 2.0) *
                       (calculo.EvaluaFx(xi) +
                        2 * suma +
                        calculo.EvaluaFx(xd));
            }
            else
            {
                throw new Exception("Función mal ingresada");
            }
        }
    }
}