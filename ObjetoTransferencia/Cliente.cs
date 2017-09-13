using System;

namespace ObjetoTransferencia
{
    public class Cliente
    {
        public int IdCliente { get; set; }

        //Code Snippet - prop
        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public Boolean Sexo { get; set; }

        public decimal LimiteCompra { get; set; }
    }
}
