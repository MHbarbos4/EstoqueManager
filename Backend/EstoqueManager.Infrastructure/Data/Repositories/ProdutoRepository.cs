using Dapper;
using EstoqueManager.Domain.Entities;
using GerenciamentoEstoque.Domain.Interfaces;
using EstoqueManager.Infrastructure.Data;
using System.Data;

namespace EstoqueManager.Infrastructure.Repositories;

public class ProdutoRepository : IRepository<Produto>
{
    private readonly DbConnectionFactory _connectionFactory;

    public ProdutoRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Produto>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Produto>("SELECT * FROM Produtos");
    }

    public async Task<Produto> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Produto>("SELECT * FROM Produtos WHERE Id = @Id", new { Id = id });
    }

    public async Task AddAsync(Produto entity)
    {
        using var connection = _connectionFactory.CreateConnection();
        entity.Id = await connection.ExecuteScalarAsync<int>(
            "INSERT INTO Produtos (Nome, Preco, Quantidade, CategoriaId, FornecedorId) " +
            "VALUES (@Nome, @Preco, @Quantidade, @CategoriaId, @FornecedorId) RETURNING Id",
            entity);
    }

    public async Task UpdateAsync(Produto entity)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "UPDATE Produtos SET Nome = @Nome, Preco = @Preco, Quantidade = @Quantidade, " +
            "CategoriaId = @CategoriaId, FornecedorId = @FornecedorId WHERE Id = @Id",
            entity);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync("DELETE FROM Produtos WHERE Id = @Id", new { Id = id });
    }
}