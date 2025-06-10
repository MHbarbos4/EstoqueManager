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

public class UsuarioRepository : IRepository<Usuario>
{
    private readonly DbConnectionFactory _connectionFactory;

    public UsuarioRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Usuario>("SELECT * FROM Usuarios");
    }

    public async Task<Usuario> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Usuario>(
            "SELECT * FROM Usuarios WHERE Id = @Id", new { Id = id });
    }

    public async Task AddAsync(Usuario entity)
    {
        using var connection = _connectionFactory.CreateConnection();
        entity.Id = await connection.ExecuteScalarAsync<int>(
            "INSERT INTO Usuarios (Email, Senha, Role) VALUES (@Email, @Senha, @Role) RETURNING Id",
            entity);
    }

    public async Task UpdateAsync(Usuario entity)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "UPDATE Usuarios SET Email = @Email, Senha = @Senha, Role = @Role WHERE Id = @Id",
            entity);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync("DELETE FROM Usuarios WHERE Id = @Id", new { Id = id });
    }
}