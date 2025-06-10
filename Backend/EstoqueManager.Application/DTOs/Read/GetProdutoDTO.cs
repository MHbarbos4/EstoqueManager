using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EstoqueManager.Application.DTOs.Read;

public class GetProdutoDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
    public int CategoriaId { get; set; }
    public int FornecedorId { get; set; }
}
