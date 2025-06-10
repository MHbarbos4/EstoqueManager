using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstoqueManager.Application.DTOs.Create;
using EstoqueManager.Application.DTOs.Read;

namespace EstoqueManager.Application.Interfaces;

public interface ICategoriaService
{
    Task<IEnumerable<GetCategoriaDTO>> GetAllAsync();
    Task<GetCategoriaDTO> GetByIdAsync(int id);
    Task<int> AddAsync(CreateCategoriaDTO dto);
    Task UpdateAsync(int id, CreateCategoriaDTO dto);
    Task DeleteAsync(int id);
}