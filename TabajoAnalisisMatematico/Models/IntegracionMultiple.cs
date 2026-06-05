namespace TabajoAnalisisMatematico.Models
{
    public class IntegracionMultiple
    {
        public string Funcion { get; set; }
        public double Xi { get; set; }
        public double Xd { get; set; }
        public int N { get; set; }
        public double? Resultado { get; set; }
        public string? MensajeError { get; set; }
    }
}