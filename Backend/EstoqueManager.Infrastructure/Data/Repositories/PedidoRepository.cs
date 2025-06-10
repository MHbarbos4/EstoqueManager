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

public class PedidoRepository : IRepository<Pedido>
{
    private readonly DbConnectionFactory _connectionFactory;

    public PedidoRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Pedido>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Pedido>("SELECT * FROM Pedidos");
    }

    public async Task<Pedido> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Pedido>("SELECT * FROM Pedidos WHERE Id = @Id", new { Id = id });
    }

    public async Task AddAsync(Pedido entity)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "INSERT INTO Pedidos (Data, FornecedorId, Status) VALUES (@Data, @FornecedorId, @Status)",
            entity);
    }

    public async Task UpdateAsync(Pedido entity)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "UPDATE Pedidos SET Data = @Data, FornecedorId = @FornecedorId, Status = @Status WHERE Id = @Id",
            entity);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync("DELETE FROM Pedidos WHERE Id = @Id", new { Id = id });
    }
}