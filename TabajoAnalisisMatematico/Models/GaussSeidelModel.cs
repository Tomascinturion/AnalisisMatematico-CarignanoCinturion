using Metodos;

namespace TabajoAnalisisMatematico.Models
{
    public class GaussSeidelModel
    {
        public double[][] Matriz { get; set; }

        public int Dimension { get; set; }
        public ResultadoGaussSeidel? Resultado { get; set; }
    }
}