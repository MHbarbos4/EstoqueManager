using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstoqueManager.Application.DTOs.Create;
using EstoqueManager.Application.DTOs.Read;

namespace EstoqueManager.Application.Interfaces;

public interface IMovimentacaoService
{
    Task<IEnumerable<GetMovimentacaoDTO>> GetAllAsync();
    Task<GetMovimentacaoDTO> GetByIdAsync(int id);
    Task AddAsync(CreateMovimentacaoDTO dto);
    Task UpdateAsync(int id, CreateMovimentacaoDTO dto);
    Task DeleteAsync(int id);
}