using Dapper;
using EstoqueManager.Domain.Entities;
using GerenciamentoEstoque.Domain.Interfaces;
using EstoqueManager.Infrastructure.Data;
using System.Data;

namespace EstoqueManager.Infrastructure.Repositories;

public class FornecedorRepository : IRepository<Fornecedor>
{
    private readonly DbConnectionFactory _connectionFactory;

    public FornecedorRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Fornecedor>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Fornecedor>("SELECT * FROM Fornecedores");
    }

    public async Task<Fornecedor> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Fornecedor>("SELECT * FROM Fornecedores WHERE Id = @Id", new { Id = id });
    }

    public async Task AddAsync(Fornecedor entity)
    {
        using var connection = _connectionFactory.CreateConnection();
        entity.Id = await connection.ExecuteScalarAsync<int>(
            "INSERT INTO Fornecedores (Nome, Contato) VALUES (@Nome, @Contato) RETURNING Id",
            entity);
    }

    public async Task UpdateAsync(Fornecedor entity)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "UPDATE Fornecedores SET Nome = @Nome, Contato = @Contato WHERE Id = @Id",
            entity);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync("DELETE FROM Fornecedores WHERE Id = @Id", new { Id = id });
    }
}