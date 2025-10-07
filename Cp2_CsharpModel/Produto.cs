namespace Cp2_CsharpModel
{
    public class Produto
    {
        public int Id { get; set; } // Id do produto (chave primária)
        public string Codigo { get; set; } // Código único do produto
        public string Nome { get; set; } // Nome do produto
        public decimal Preco { get; set; } // Preço do produto
        public int Estoque { get; set; } // Quantidade em estoque

        // Validação do estoque
        public void AjustarEstoque(int quantidade)
        {
            if (Estoque + quantidade < 0)
                throw new InvalidOperationException("Estoque não pode ser negativo.");
            Estoque += quantidade;
        }
    }
}
