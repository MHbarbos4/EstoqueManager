using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstoqueManager.Application.DTOs.Create;
using EstoqueManager.Application.DTOs.Read;

namespace EstoqueManager.Application.Interfaces;

public interface IFornecedorService
{
    Task<IEnumerable<GetFornecedorDTO>> GetAllAsync();
    Task<GetFornecedorDTO> GetByIdAsync(int id);
    Task<int> AddAsync(CreateFornecedorDTO dto);
    Task UpdateAsync(int id, CreateFornecedorDTO dto);
    Task DeleteAsync(int id);
}