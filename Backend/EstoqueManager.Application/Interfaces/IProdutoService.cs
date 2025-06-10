using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstoqueManager.Application.DTOs.Create;
using EstoqueManager.Application.DTOs.Read;

namespace EstoqueManager.Application.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<GetProdutoDTO>> GetAllAsync();
    Task<GetProdutoDTO> GetByIdAsync(int id);
    Task<int> AddAsync(CreateProdutoDTO dto);
    Task UpdateAsync(int id, CreateProdutoDTO dto);
    Task DeleteAsync(int id);
}