namespace Cp2_CsharpModel
{
    public class Pedido
    {
        public int Id { get; set; } // ID do pedido
        public string Codigo { get; set; } // Código único do pedido
        public DateTime Data { get; set; } // Data do pedido
        public string Status { get; set; } // Status do pedido: "Pendente", "Entregue", etc.
        public decimal ValorTotal { get; set; } // Valor total do pedido

        // Método para verificar se o pedido está entregue
        public bool EstaEntregue() => Status.Equals("Entregue", StringComparison.OrdinalIgnoreCase);
    }
}
