using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstoqueManager.Application.DTOs.Create;
using EstoqueManager.Application.DTOs.Read;
using EstoqueManager.Application.Interfaces;
using EstoqueManager.Domain.Entities;
using GerenciamentoEstoque.Domain.Interfaces;

namespace EstoqueManager.Application.Services;

public class PedidoService : IPedidoService
{
    private readonly IRepository<Pedido> _repository;

    public PedidoService(IRepository<Pedido> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GetPedidoDTO>> GetAllAsync()
    {
        var pedidos = await _repository.GetAllAsync();
        return pedidos.Select(p => new GetPedidoDTO
        {
            Id = p.Id,
            Data = p.Data,
            FornecedorId = p.FornecedorId,
            Status = p.Status
        });
    }

    public async Task<GetPedidoDTO> GetByIdAsync(int id)
    {
        var pedido = await _repository.GetByIdAsync(id);
        if (pedido == null)
            throw new Exception("Pedido não encontrado.");
        return new GetPedidoDTO
        {
            Id = pedido.Id,
            Data = pedido.Data,
            FornecedorId = pedido.FornecedorId,
            Status = pedido.Status
        };
    }

    public async Task AddAsync(CreatePedidoDTO dto)
    {
        var pedido = new Pedido
        {
            Data = dto.Data,
            FornecedorId = dto.FornecedorId,
            Status = dto.Status
        };
        await _repository.AddAsync(pedido);
    }

    public async Task UpdateAsync(int id, CreatePedidoDTO dto)
    {
        var pedido = new Pedido
        {
            Id = id,
            Data = dto.Data,
            FornecedorId = dto.FornecedorId,
            Status = dto.Status
        };
        await _repository.UpdateAsync(pedido);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}