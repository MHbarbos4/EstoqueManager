using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstoqueManager.Application.DTOs.Create;
using EstoqueManager.Application.DTOs.Read;

namespace EstoqueManager.Application.Interfaces;

public interface IPedidoService
{
    Task<IEnumerable<GetPedidoDTO>> GetAllAsync();
    Task<GetPedidoDTO> GetByIdAsync(int id);
    Task AddAsync(CreatePedidoDTO dto);
    Task UpdateAsync(int id, CreatePedidoDTO dto);
    Task DeleteAsync(int id);
}