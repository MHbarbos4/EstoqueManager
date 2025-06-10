using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstoqueManager.Application.DTOs;
using EstoqueManager.Application.DTOs.Create;
using EstoqueManager.Application.DTOs.Read;
using EstoqueManager.Application.Interfaces;
using EstoqueManager.Domain.Entities;
using GerenciamentoEstoque.Domain.Interfaces;

namespace EstoqueManager.Application.Services;

public class ItemPedidoService : IItemPedidoService
{
    private readonly IRepository<ItemPedido> _itemPedidoRepository;
    private readonly IRepository<Produto> _produtoRepository;
    private readonly IRepository<Pedido> _pedidoRepository;

    public ItemPedidoService(
        IRepository<ItemPedido> itemPedidoRepository,
        IRepository<Produto> produtoRepository,
        IRepository<Pedido> pedidoRepository)
    {
        _itemPedidoRepository = itemPedidoRepository;
        _produtoRepository = produtoRepository;
        _pedidoRepository = pedidoRepository;
    }

    public async Task<IEnumerable<GetItemPedidoDTO>> GetAllAsync()
    {
        var itens = await _itemPedidoRepository.GetAllAsync();
        var produtos = await _produtoRepository.GetAllAsync();
        return itens.Select(i => new GetItemPedidoDTO
        {
            Id = i.Id,
            PedidoId = i.PedidoId,
            ProdutoId = i.ProdutoId,
            ProdutoNome = produtos.First(p => p.Id == i.ProdutoId).Nome,
            PrecoUnitario = i.PrecoUnitario,
            Quantidade = i.Quantidade
        });
    }

    public async Task<GetItemPedidoDTO> GetByIdAsync(int id)
    {
        var item = await _itemPedidoRepository.GetByIdAsync(id);
        if (item == null)
            throw new Exception("Item de pedido não encontrado.");

        var produto = await _produtoRepository.GetByIdAsync(item.ProdutoId);
        return new GetItemPedidoDTO
        {
            Id = item.Id,
            PedidoId = item.PedidoId,
            ProdutoId = item.ProdutoId,
            ProdutoNome = produto.Nome,
            PrecoUnitario = item.PrecoUnitario,
            Quantidade = item.Quantidade
        };
    }

    public async Task AddAsync(CreateItemPedidoDTO dto)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(dto.PedidoId);
        if (pedido == null)
            throw new Exception("Pedido não encontrado.");

        var produto = await _produtoRepository.GetByIdAsync(dto.ProdutoId);
        if (produto == null)
            throw new Exception("Produto não encontrado.");

        if (dto.PrecoUnitario.HasValue && dto.PrecoUnitario < 0)
            throw new ArgumentException("O preço unitário não pode ser negativo.");

        var item = new ItemPedido
        {
            PedidoId = dto.PedidoId,
            ProdutoId = dto.ProdutoId,
            Quantidade = dto.Quantidade,
            PrecoUnitario = dto.PrecoUnitario ?? produto.Preco // Usa Preco do produto se não fornecido
        };

        await _itemPedidoRepository.AddAsync(item);
    }

    public async Task UpdateAsync(int id, CreateItemPedidoDTO dto)
    {
        var item = await _itemPedidoRepository.GetByIdAsync(id);
        if (item == null)
            throw new Exception("Item de pedido não encontrado.");

        var pedido = await _pedidoRepository.GetByIdAsync(dto.PedidoId);
        if (pedido == null)
            throw new Exception("Pedido não encontrado.");

        var produto = await _produtoRepository.GetByIdAsync(dto.ProdutoId);
        if (produto == null)
            throw new Exception("Produto não encontrado.");

        if (dto.PrecoUnitario.HasValue && dto.PrecoUnitario < 0)
            throw new ArgumentException("O preço unitário não pode ser negativo.");

        item.PedidoId = dto.PedidoId;
        item.ProdutoId = dto.ProdutoId;
        item.Quantidade = dto.Quantidade;
        item.PrecoUnitario = dto.PrecoUnitario ?? produto.Preco;

        await _itemPedidoRepository.UpdateAsync(item);
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _itemPedidoRepository.GetByIdAsync(id);
        if (item == null)
            throw new Exception("Item de pedido não encontrado.");

        await _itemPedidoRepository.DeleteAsync(id);
    }
}