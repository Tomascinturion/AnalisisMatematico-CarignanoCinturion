
using Calculus;

namespace Metodos
{
    public class MetodosAbiertos
    {
        public delegate double Funcion(double x);

        public ResultadoMetodos Calcular(string metodo, Funcion funcion, double xi, double xd, int iteraciones, double tolerancia)
        {
            if (Math.Abs(funcion(xi)) < tolerancia)
                return new ResultadoMetodos { Raiz = xi };

            if (metodo == "Secante" && Math.Abs(funcion(xd)) < tolerancia)
                return new ResultadoMetodos { Raiz = xd };

            double xr = 0;
            double xrAnterior = 0;
            double error = 0;

            /*for (int i = 1; i <= iteraciones; i++)
            {
                xr = CalcularXr(metodo, funcion, xi, xd, tolerancia);

                if (double.IsNaN(xr))
                    throw new Exception("El método diverge");

                if (i > 1)
                    error = Math.Abs((xr - xrAnterior) / xr);

                if (Math.Abs(funcion(xr)) < tolerancia || error < tolerancia)
                {
                    return new ResultadoMetodos
                    {
                        Raiz = xr,
                        Error = error,
                        Iteraciones = i
                    };
                }

                if (metodo == "Tangente")
                {
                    xi = xr;
                }
                else
                {
                    xi = xd;
                    xd = xr;
                }

                xrAnterior = xr;
            }*/

            return new ResultadoMetodos
            {
                Raiz = xr,
                Error = error,
                Iteraciones = iteraciones
            };
        }

        private double CalcularXr(string metodo, Funcion funcion, Calculo AnalizadorDeFunciones, double xi, double xd, double tolerancia)
        {
            if (metodo == "Tangente")
            {
                double derivada = AnalizadorDeFunciones.Dx(xi);

                if (Math.Abs(derivada) < tolerancia || double.IsNaN(derivada))
                    return double.NaN;

                return xi - (funcion(xi) / derivada);
            }

            if (metodo == "Secante")
            {
                double fxi = funcion(xi);
                double fxd = funcion(xd);

                if (Math.Abs(fxd - fxi) < tolerancia)
                    return double.NaN;

                return (fxd * xi - fxi * xd) / (fxd - fxi);
            }

            return double.NaN;
        }

        private double CalcularXr(string metodo, Calculo analizador, double xi, double xd, double tolerancia)
        {
            if (metodo == "Tangente")
            {
                double derivada = analizador.Dx(xi);

                if (Math.Abs(derivada) < tolerancia || double.IsNaN(derivada))
                    return double.NaN;

                double fx = analizador.EvaluaFx(xi);

                return xi - (fx / derivada);
            }

            if (metodo == "Secante")
            {
                double fxi = analizador.EvaluaFx(xi);
                double fxd = analizador.EvaluaFx(xd);

                if (Math.Abs(fxd - fxi) < tolerancia)
                    return double.NaN;

                return (fxd * xi - fxi * xd) / (fxd - fxi);
            }

            return double.NaN;
        }
    }
}