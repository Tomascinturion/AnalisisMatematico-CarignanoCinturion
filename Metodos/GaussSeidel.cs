using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metodos
{
    public class GaussSeidel
    {
        public ResultadoGaussSeidel Gauss_Seidel(double[][] matriz, int dimension)
        {

            double tolerancia = 0.0001;
            bool esSolucion = false;
            int contador = 0;
            double[] vectorResultado = new double[dimension];
            vectorResultado.Initialize(); //Inicializa con ceros
            double[] vectorAnterior = new double[dimension];

            if (!EsDiagonalmenteDominante(matriz, dimension))
            {
                throw new InvalidOperationException(
                    "La matriz no es diagonalmente dominante. Gauss-Seidel puede no converger."
                );
            }

            while (contador <= 100 && !esSolucion)
            {
                contador++;
                if (contador > 1)
                {
                    vectorResultado.CopyTo(vectorAnterior, 0);
                }

                for(int row = 0; row < dimension; row++)
                {
                    double resultado = matriz[row][dimension]; //Termino independiente
                    double coeficienteIncognita = matriz[row][row]; //Coeficiente de la incógnita actual
                    for(int col = 0; col < dimension; col++)
                    {
                        if (col != row)
                        {
                            //Resto el producto de los coeficientes por las soluciones anteriores
                            resultado -= matriz[row][col] * vectorResultado[col];
                        }
                    }

                    if (matriz[row][row] == 0)
                    {
                        throw new DivideByZeroException(
                            $"Elemento diagonal en fila {row} es 0. No se puede aplicar Gauss-Seidel."
                        );
                    }

                    //Divido por el coeficiente de la incógnita actual
                    coeficienteIncognita = resultado / coeficienteIncognita; 
                    vectorResultado[row] = coeficienteIncognita;
                }

                int contadorMismoResultado = 0;
                double errorRelativo = 0;
                for(int i = 0; i < dimension; i++)
                {
                    errorRelativo = Math.Abs((vectorResultado[i] - vectorAnterior[i]) / vectorResultado[i]);
                    if(errorRelativo < tolerancia)
                    {
                        //Verifica la igualdad del valor del coeficiente con la iteracion anterior
                        contadorMismoResultado++;
                    }
                }

                //Si todos los resultados son iguales al de la iteracion anterior,
                //se considera que se ha encontrado la solución
                esSolucion = contadorMismoResultado == dimension;
            }

            if (contador <= 100)
            {
                return new ResultadoGaussSeidel
                {
                    Iteraciones = contador,
                    Resultado = vectorResultado
                }; 

                
            }
            else
            {
                throw new ArgumentOutOfRangeException("El método no convergió después de 100 iteraciones.");
            }
        }

        public bool EsDiagonalmenteDominante(double[][] matriz, int dimension)
        {
            for (int i = 0; i < dimension; i++)
            {
                double sumaFila = 0;
                double diagonal = Math.Abs(matriz[i][i]);

                for (int j = 0; j < dimension; j++)
                {
                    if (i != j)
                    {
                        sumaFila += Math.Abs(matriz[i][j]);
                    }
                }

                // condición de dominancia diagonal
                if (diagonal < sumaFila)
                {
                    return false;
                }
            }

            return true;
        }
    }
}