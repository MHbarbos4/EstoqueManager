using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstoqueManager.Application.DTOs;
using EstoqueManager.Application.DTOs.Create;
using EstoqueManager.Application.DTOs.Read;

namespace EstoqueManager.Application.Interfaces;

public interface IItemPedidoService
{
    Task<IEnumerable<GetItemPedidoDTO>> GetAllAsync();
    Task<GetItemPedidoDTO> GetByIdAsync(int id);
    Task AddAsync(CreateItemPedidoDTO dto);
    Task UpdateAsync(int id, CreateItemPedidoDTO dto);
    Task DeleteAsync(int id);
}
