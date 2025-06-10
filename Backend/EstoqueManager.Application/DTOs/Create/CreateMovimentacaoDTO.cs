using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueManager.Application.DTOs.Create
{
    public class CreateMovimentacaoDTO
    {
        public int ProdutoId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public DateTime Data { get; set; }
        public string? Motivo { get; set; }
    }
}
