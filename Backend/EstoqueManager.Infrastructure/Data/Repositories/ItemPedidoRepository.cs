using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EstoqueManager.Domain.Entities;
using GerenciamentoEstoque.Domain.Interfaces;
using EstoqueManager.Infrastructure.Data;

namespace EstoqueManager.Infrastructure.Repositories;

public class ItemPedidoRepository : IRepository<ItemPedido>
{
    private readonly DbConnectionFactory _connectionFactory;

    public ItemPedidoRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<ItemPedido>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<ItemPedido>("SELECT * FROM ItensPedidos");
    }

    public async Task<ItemPedido> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<ItemPedido>(
            "SELECT * FROM ItensPedidos WHERE Id = @Id", new { Id = id });
    }

    public async Task AddAsync(ItemPedido entity)
    {
        using var connection = _connectionFactory.CreateConnection();
        entity.Id = await connection.ExecuteScalarAsync<int>(
            "INSERT INTO ItensPedidos (PedidoId, ProdutoId, Quantidade, PrecoUnitario) " +
            "VALUES (@PedidoId, @ProdutoId, @Quantidade, @PrecoUnitario) RETURNING Id",
            entity);
    }

    public async Task UpdateAsync(ItemPedido entity)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "UPDATE ItensPedidos SET PedidoId = @PedidoId, ProdutoId = @ProdutoId, " +
            "Quantidade = @Quantidade, PrecoUnitario = @PrecoUnitario WHERE Id = @Id",
            entity);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync("DELETE FROM ItensPedidos WHERE Id = @Id", new { Id = id });
    }
}