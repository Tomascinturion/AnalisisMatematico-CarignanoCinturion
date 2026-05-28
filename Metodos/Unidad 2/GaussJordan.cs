namespace Metodos
{
    public class GaussJordan
    {
        public static double[] Gauss_Jordan(double[][] matriz)
        {
            int n = matriz.GetLength(0);
            double[] resultado = new double[n];

            for (int i = 0; i < n; i++)
            {
                double pivote = matriz[i] [i];

                if (pivote == 0)
                    throw new ArgumentException("Pivote igual a 0, el método no puede continuar.");

                // Normalizar fila
                for (int j = 0; j < n + 1; j++)
                {
                    matriz[i] [j] /= pivote;
                }

                // Hacer 0 arriba y abajo del pivote
                for (int k = 0; k < n; k++)
                {
                    if (k != i)
                    {
                        double factor = matriz[k] [i];

                        for (int j = 0; j < n + 1; j++)
                        {
                            matriz[k] [j] -= factor * matriz[i] [j];
                        }
                    }
                }
            }

            // Las soluciones quedan directamente en la última columna
            for (int i = 0; i < n; i++)
            {
                resultado[i] = matriz[i][n];
            }

            return resultado;
        }
    }
}