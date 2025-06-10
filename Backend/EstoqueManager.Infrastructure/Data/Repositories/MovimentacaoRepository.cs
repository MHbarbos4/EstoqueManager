using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EstoqueManager.Domain.Entities;
using GerenciamentoEstoque.Domain.Interfaces;
using EstoqueManager.Infrastructure.Data;
using System.Data;

namespace EstoqueManager.Infrastructure.Repositories;

public class MovimentacaoRepository : IRepository<Movimentacao>
{
    private readonly DbConnectionFactory _connectionFactory;

    public MovimentacaoRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Movimentacao>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Movimentacao>("SELECT * FROM Movimentacoes");
    }

    public async Task<Movimentacao> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Movimentacao>("SELECT * FROM Movimentacoes WHERE Id = @Id", new { Id = id });
    }

    public async Task AddAsync(Movimentacao entity)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "INSERT INTO Movimentacoes (ProdutoId, Tipo, Quantidade, Data, Motivo) " +
            "VALUES (@ProdutoId, @Tipo, @Quantidade, @Data, @Motivo)",
            entity);
    }

    public async Task UpdateAsync(Movimentacao entity)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "UPDATE Movimentacoes SET ProdutoId = @ProdutoId, Tipo = @Tipo, Quantidade = @Quantidade, " +
            "Data = @Data, Motivo = @Motivo WHERE Id = @Id",
            entity);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync("DELETE FROM Movimentacoes WHERE Id = @Id", new { Id = id });
    }
}