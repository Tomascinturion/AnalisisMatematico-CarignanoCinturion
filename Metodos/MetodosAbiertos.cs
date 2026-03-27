
using Calculus;

namespace Metodos
{
    public class MetodosAbiertos
    {
        public ResultadoMetodos Calcular(string funcionTexto, string metodo, double xi, double xd, int iteraciones, double tolerancia)
        {
            //TODO: VALIDAR TAMBIEN EN FRONT LOS CAMPOS NUMERICOS QUE SE RECIBEN COMO STRING 
            metodo = metodo.ToLower();

            Calculo analizador = new Calculo();

            try
            {
                if (!analizador.Sintaxis(funcionTexto, 'x'))
                {
                    return new ResultadoMetodos
                    {
                        Exito = false,
                        Mensaje = "Error de sintaxis en la función."
                    };
                }
            }
            catch
            {
                return new ResultadoMetodos
                {
                    Exito = false,
                    Mensaje = "La función ingresada no es válida."
                };
            }

            if (Math.Abs(analizador.EvaluaFx(xi)) < tolerancia)
                return new ResultadoMetodos
                {
                    Exito = true,
                    Mensaje = "Raíz encontrada en xi.",
                    Raiz = xi,
                    Error = 0,
                    Iteraciones = 0
                };

            if (metodo == "secante" && Math.Abs(analizador.EvaluaFx(xd)) < tolerancia)
                return new ResultadoMetodos
                {
                    Exito = true,
                    Mensaje = "Raíz encontrada en xd.",
                    Raiz = xd,
                    Error = 0,
                    Iteraciones = 0
                };

            double xr = 0;
            double xrAnterior = 0;
            double error = 0;

            for (int i = 1; i <= iteraciones; i++)
            {
                xr = CalcularXr(metodo, analizador, xi, xd, tolerancia);

                if (double.IsNaN(xr))
                {
                    return new ResultadoMetodos
                    {
                        Exito = false,
                        Mensaje = "El método diverge. No se pudo calcular la raíz."
                    };
                }

                if (i > 1)
                {
                    if (xr != 0)
                        error = Math.Abs((xr - xrAnterior) / xr);
                    else
                        error = double.PositiveInfinity;
                }

                if (Math.Abs(analizador.EvaluaFx(xr)) < tolerancia || error < tolerancia)
                {
                    return new ResultadoMetodos
                    {
                        Exito = true,
                        Mensaje = "Raíz encontrada.",
                        Raiz = xr,
                        Error = error,
                        Iteraciones = i
                    };
                }

                if (metodo == "tangente")
                {
                    xi = xr;
                }
                else
                {
                    xi = xd;
                    xd = xr;
                }

                xrAnterior = xr;
            }

            return new ResultadoMetodos
            {
                Exito = false,
                Mensaje = "Se alcanzó el límite de iteraciones sin cumplir la tolerancia.",
                Raiz = xr,
                Error = error,
                Iteraciones = iteraciones
            };
        }

        private double CalcularXr(string metodo, Calculo analizador, double xi, double xd, double tolerancia)
        {
            if (metodo == "tangente")
            {
                double derivada = analizador.Dx(xi);

                if (Math.Abs(derivada) < tolerancia || double.IsNaN(derivada))
                    return double.NaN;

                double fxi = analizador.EvaluaFx(xi);

                return xi - (fxi / derivada);
            }

            if (metodo == "secante")
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