using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueManager.Application.DTOs.Create
{
    public class CreateItemPedidoDTO
    {
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal? PrecoUnitario { get; set; }
    }
}
