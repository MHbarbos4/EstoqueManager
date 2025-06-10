using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueManager.Application.DTOs.Create;

public class CreatePedidoDTO
{
    public DateTime Data { get; set; }
    public int FornecedorId { get; set; }
    public string Status { get; set; } = string.Empty;
}
