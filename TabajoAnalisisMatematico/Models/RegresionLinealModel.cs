using Metodos;

namespace TabajoAnalisisMatematico.Models
{
    public class RegresionLinealModel
    {
        public List<double[]> Puntos { get; set; }
        public ResultadoRegresion? Resultado { get; set; }
        public string? MensajeError { get; set; }
    }
}
