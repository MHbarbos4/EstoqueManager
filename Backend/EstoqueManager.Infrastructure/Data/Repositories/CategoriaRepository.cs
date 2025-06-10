using Dapper;
using EstoqueManager.Domain.Entities;
using GerenciamentoEstoque.Domain.Interfaces;
using EstoqueManager.Infrastructure.Data;
using System.Data;

namespace EstoqueManager.Infrastructure.Repositories;

public class CategoriaRepository : IRepository<Categoria>
{
    private readonly DbConnectionFactory _connectionFactory;

    public CategoriaRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Categoria>("SELECT * FROM Categorias");
    }

    public async Task<Categoria> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Categoria>("SELECT * FROM Categorias WHERE Id = @Id", new { Id = id });
    }

    public async Task AddAsync(Categoria entity)
    {
        using var connection = _connectionFactory.CreateConnection();
        entity.Id = await connection.ExecuteScalarAsync<int>(
            "INSERT INTO Categorias (Nome) VALUES (@Nome) RETURNING Id",
            entity);
    }

    public async Task UpdateAsync(Categoria entity)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "UPDATE Categorias SET Nome = @Nome WHERE Id = @Id",
            entity);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync("DELETE FROM Categorias WHERE Id = @Id", new { Id = id });
    }
}