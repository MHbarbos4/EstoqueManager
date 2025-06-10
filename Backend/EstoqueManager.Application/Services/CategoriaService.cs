using EstoqueManager.Application.DTOs;
using EstoqueManager.Application.DTOs.Create;
using EstoqueManager.Application.DTOs.Read;
using EstoqueManager.Application.Interfaces;
using EstoqueManager.Domain.Entities;
using GerenciamentoEstoque.Domain.Interfaces;

namespace EstoqueManager.Application.Services;

public class CategoriaService : ICategoriaService
{
    private readonly IRepository<Categoria> _categoriaRepository;
    private readonly IRepository<Produto> _produtoRepository;

    public CategoriaService(IRepository<Categoria> categoriaRepository, IRepository<Produto> produtoRepository)
    {
        _categoriaRepository = categoriaRepository;
        _produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<GetCategoriaDTO>> GetAllAsync()
    {
        var categorias = await _categoriaRepository.GetAllAsync();
        return categorias.Select(c => new GetCategoriaDTO
        {
            Id = c.Id,
            Nome = c.Nome
        });
    }

    public async Task<GetCategoriaDTO> GetByIdAsync(int id)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(id);
        if (categoria == null)
            throw new Exception("Categoria não encontrada.");
        return new GetCategoriaDTO
        {
            Id = categoria.Id,
            Nome = categoria.Nome
        };
    }

    public async Task<int> AddAsync(CreateCategoriaDTO dto)
    {
        var categoria = new Categoria
        {
            Nome = dto.Nome
        };
        await _categoriaRepository.AddAsync(categoria);
        return categoria.Id;
    }

    public async Task UpdateAsync(int id, CreateCategoriaDTO dto)
    {
        var categoria = new Categoria
        {
            Id = id,
            Nome = dto.Nome
        };
        await _categoriaRepository.UpdateAsync(categoria);
    }

    public async Task DeleteAsync(int id)
    {
        var produtos = await _produtoRepository.GetAllAsync();
        if (produtos.Any(p => p.CategoriaId == id))
            throw new Exception("Não é possível excluir categoria com produtos associados.");

        await _categoriaRepository.DeleteAsync(id);
    }
}