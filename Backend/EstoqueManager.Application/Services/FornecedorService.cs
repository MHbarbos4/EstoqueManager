using EstoqueManager.Application.DTOs;
using EstoqueManager.Application.DTOs.Create;
using EstoqueManager.Application.DTOs.Read;
using EstoqueManager.Application.Interfaces;
using EstoqueManager.Domain.Entities;
using GerenciamentoEstoque.Domain.Interfaces;

namespace EstoqueManager.Application.Services;

public class FornecedorService : IFornecedorService
{
    private readonly IRepository<Fornecedor> _fornecedorRepository;
    private readonly IRepository<Produto> _produtoRepository;
    private readonly IRepository<Pedido> _pedidoRepository;

    public FornecedorService(
        IRepository<Fornecedor> fornecedorRepository,
        IRepository<Produto> produtoRepository,
        IRepository<Pedido> pedidoRepository)
    {
        _fornecedorRepository = fornecedorRepository;
        _produtoRepository = produtoRepository;
        _pedidoRepository = pedidoRepository;
    }

    public async Task<IEnumerable<GetFornecedorDTO>> GetAllAsync()
    {
        var fornecedores = await _fornecedorRepository.GetAllAsync();
        return fornecedores.Select(f => new GetFornecedorDTO
        {
            Id = f.Id,
            Nome = f.Nome,
            Contato = f.Contato
        });
    }

    public async Task<GetFornecedorDTO> GetByIdAsync(int id)
    {
        var fornecedor = await _fornecedorRepository.GetByIdAsync(id);
        if (fornecedor == null)
            throw new Exception("Fornecedor não encontrado.");
        return new GetFornecedorDTO
        {
            Id = fornecedor.Id,
            Nome = fornecedor.Nome,
            Contato = fornecedor.Contato
        };
    }

    public async Task<int> AddAsync(CreateFornecedorDTO dto)
    {
        var fornecedor = new Fornecedor
        {
            Nome = dto.Nome,
            Contato = dto.Contato
        };
        await _fornecedorRepository.AddAsync(fornecedor);
        return fornecedor.Id;
    }

    public async Task UpdateAsync(int id, CreateFornecedorDTO dto)
    {
        var fornecedor = new Fornecedor
        {
            Id = id,
            Nome = dto.Nome,
            Contato = dto.Contato
        };
        await _fornecedorRepository.UpdateAsync(fornecedor);
    }

    public async Task DeleteAsync(int id)
    {
        var produtos = await _produtoRepository.GetAllAsync();
        if (produtos.Any(p => p.FornecedorId == id))
            throw new Exception("Não é possível excluir fornecedor com produtos associados.");

        var pedidos = await _pedidoRepository.GetAllAsync();
        if (pedidos.Any(p => p.FornecedorId == id))
            throw new Exception("Não é possível excluir fornecedor com pedidos associados.");

        await _fornecedorRepository.DeleteAsync(id);
    }
}