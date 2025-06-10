using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EstoqueManager.Domain.Entities;

public class Pedido
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public int FornecedorId { get; set; }
    public string Status { get; set; } = string.Empty;
}
