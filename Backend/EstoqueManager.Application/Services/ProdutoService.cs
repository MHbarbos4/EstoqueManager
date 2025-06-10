using EstoqueManager.Application.DTOs;
using EstoqueManager.Application.DTOs.Create;
using EstoqueManager.Application.DTOs.Read;
using EstoqueManager.Application.Interfaces;
using EstoqueManager.Domain.Entities;
using GerenciamentoEstoque.Domain.Interfaces;

namespace EstoqueManager.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IRepository<Produto> _produtoRepository;
    private readonly IRepository<Categoria> _categoriaRepository;
    private readonly IRepository<Fornecedor> _fornecedorRepository;
    private readonly IRepository<Movimentacao> _movimentacaoRepository;
    private readonly IRepository<ItemPedido> _itemPedidoRepository;

    public ProdutoService(
        IRepository<Produto> produtoRepository,
        IRepository<Categoria> categoriaRepository,
        IRepository<Fornecedor> fornecedorRepository,
        IRepository<Movimentacao> movimentacaoRepository,
        IRepository<ItemPedido> itemPedidoRepository)
    {
        _produtoRepository = produtoRepository;
        _categoriaRepository = categoriaRepository;
        _fornecedorRepository = fornecedorRepository;
        _movimentacaoRepository = movimentacaoRepository;
        _itemPedidoRepository = itemPedidoRepository;
    }

    public async Task<IEnumerable<GetProdutoDTO>> GetAllAsync()
    {
        var produtos = await _produtoRepository.GetAllAsync();
        return produtos.Select(p => new GetProdutoDTO
        {
            Id = p.Id,
            Nome = p.Nome,
            Preco = p.Preco,
            Quantidade = p.Quantidade,
            CategoriaId = p.CategoriaId,
            FornecedorId = p.FornecedorId
        });
    }

    public async Task<GetProdutoDTO> GetByIdAsync(int id)
    {
        var produto = await _produtoRepository.GetByIdAsync(id);
        if (produto == null)
            throw new Exception("Produto não encontrado.");
        return new GetProdutoDTO
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Preco = produto.Preco,
            Quantidade = produto.Quantidade,
            CategoriaId = produto.CategoriaId,
            FornecedorId = produto.FornecedorId
        };
    }

    public async Task<int> AddAsync(CreateProdutoDTO dto)
    {
        // Valida CategoriaId
        var categoria = await _categoriaRepository.GetByIdAsync(dto.CategoriaId);
        if (categoria == null)
            throw new Exception("Categoria não encontrada.");

        // Valida FornecedorId
        var fornecedor = await _fornecedorRepository.GetByIdAsync(dto.FornecedorId);
        if (fornecedor == null)
            throw new Exception("Fornecedor não encontrado.");

        var produto = new Produto
        {
            Nome = dto.Nome,
            Preco = dto.Preco,
            Quantidade = dto.Quantidade,
            CategoriaId = dto.CategoriaId,
            FornecedorId = dto.FornecedorId
        };
        await _produtoRepository.AddAsync(produto);
        return produto.Id;
    }

    public async Task UpdateAsync(int id, CreateProdutoDTO dto)
    {
        // Valida CategoriaId
        var categoria = await _categoriaRepository.GetByIdAsync(dto.CategoriaId);
        if (categoria == null)
            throw new Exception("Categoria não encontrada.");

        // Valida FornecedorId
        var fornecedor = await _fornecedorRepository.GetByIdAsync(dto.FornecedorId);
        if (fornecedor == null)
            throw new Exception("Fornecedor não encontrado.");

        var produto = new Produto
        {
            Id = id,
            Nome = dto.Nome,
            Preco = dto.Preco,
            Quantidade = dto.Quantidade,
            CategoriaId = dto.CategoriaId,
            FornecedorId = dto.FornecedorId
        };
        await _produtoRepository.UpdateAsync(produto);
    }

    public async Task DeleteAsync(int id)
    {
        // Verifica se o produto tem movimentações
        var movimentacoes = await _movimentacaoRepository.GetAllAsync();
        if (movimentacoes.Any(m => m.ProdutoId == id))
            throw new Exception("Não é possível excluir produto com movimentações associadas.");

        // Verifica se o produto está em itens de pedidos
        var itensPedidos = await _itemPedidoRepository.GetAllAsync();
        if (itensPedidos.Any(i => i.ProdutoId == id))
            throw new Exception("Não é possível excluir produto com itens de pedidos associados.");

        await _produtoRepository.DeleteAsync(id);
    }
}