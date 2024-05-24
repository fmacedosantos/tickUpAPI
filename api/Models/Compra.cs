namespace api.Models
{
    public class Compra
    {
        public string Data { get; set; }
        public double Valor { get; set; }
        public string FormaPagamento { get; set; }
        public int IdCompra { get; set; }
        public int QuantidadeComprada { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
    }
}
