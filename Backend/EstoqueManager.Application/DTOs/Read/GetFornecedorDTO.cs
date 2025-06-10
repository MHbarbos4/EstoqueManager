using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EstoqueManager.Application.DTOs.Read;

public class GetFornecedorDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Contato { get; set; } = string.Empty;
}