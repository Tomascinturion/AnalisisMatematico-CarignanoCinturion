using Metodos;

namespace TabajoAnalisisMatematico.Models
{
    public class RegresionPolinomialModel
    {
        public List<double[]> Puntos { get; set; }
        public int Grado { get; set; }
        public ResultadoRegresion? Resultado { get; set; }
        public string? MensajeError { get; set; }
    }
}
