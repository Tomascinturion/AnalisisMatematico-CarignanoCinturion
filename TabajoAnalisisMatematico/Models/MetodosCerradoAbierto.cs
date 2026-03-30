using Metodos;

namespace TabajoAnalisisMatematico.Models
{
    public class MetodosCerradoAbierto
    {

        public string FuncionTexto { get; set; }
        public int IteracionesMaximas { get; set; }
        public double Tolerancia { get; set; }
        public double Xi { get; set; }
        public double Xd { get; set; }
        public string MetodoElegido { get; set; }
        public ResultadoMetodos? Resultado { get; set; }
    }
}
