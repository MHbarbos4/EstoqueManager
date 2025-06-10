using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstoqueManager.Application.DTOs.Create;
using EstoqueManager.Application.DTOs.Read;
using EstoqueManager.Application.Interfaces;
using EstoqueManager.Domain.Entities;
using GerenciamentoEstoque.Domain.Interfaces;

namespace EstoqueManager.Application.Services;

public class MovimentacaoService : IMovimentacaoService
{
    private readonly IRepository<Movimentacao> _movimentacaoRepository;
    private readonly IRepository<Produto> _produtoRepository;

    public MovimentacaoService(IRepository<Movimentacao> movimentacaoRepository, IRepository<Produto> produtoRepository)
    {
        _movimentacaoRepository = movimentacaoRepository;
        _produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<GetMovimentacaoDTO>> GetAllAsync()
    {
        var movimentacoes = await _movimentacaoRepository.GetAllAsync();
        return movimentacoes.Select(m => new GetMovimentacaoDTO
        {
            Id = m.Id,
            ProdutoId = m.ProdutoId,
            Tipo = m.Tipo,
            Quantidade = m.Quantidade,
            Data = m.Data,
            Motivo = m.Motivo
        });
    }

    public async Task<GetMovimentacaoDTO> GetByIdAsync(int id)
    {
        var movimentacao = await _movimentacaoRepository.GetByIdAsync(id);
        if (movimentacao == null)
            throw new Exception("Movimentação não encontrada.");
        return new GetMovimentacaoDTO
        {
            Id = movimentacao.Id,
            ProdutoId = movimentacao.ProdutoId,
            Tipo = movimentacao.Tipo,
            Quantidade = movimentacao.Quantidade,
            Data = movimentacao.Data,
            Motivo = movimentacao.Motivo
        };
    }

    public async Task AddAsync(CreateMovimentacaoDTO dto)
    {
        // Valida o produto
        var produto = await _produtoRepository.GetByIdAsync(dto.ProdutoId);
        if (produto == null)
            throw new Exception("Produto não encontrado.");

        // Valida a quantidade para saídas
        if (dto.Tipo == "Saida" && produto.Quantidade < dto.Quantidade)
            throw new Exception("Quantidade insuficiente em estoque.");

        // Cria a movimentação
        var movimentacao = new Movimentacao
        {
            ProdutoId = dto.ProdutoId,
            Tipo = dto.Tipo,
            Quantidade = dto.Quantidade,
            Data = dto.Data,
            Motivo = dto.Motivo
        };
        await _movimentacaoRepository.AddAsync(movimentacao);

        // Atualiza a quantidade do produto
        produto.Quantidade += dto.Tipo == "Entrada" ? dto.Quantidade : -dto.Quantidade;
        await _produtoRepository.UpdateAsync(produto);
    }

    public async Task UpdateAsync(int id, CreateMovimentacaoDTO dto)
    {
        // Busca a movimentação existente
        var movimentacaoExistente = await _movimentacaoRepository.GetByIdAsync(id);
        if (movimentacaoExistente == null)
            throw new Exception("Movimentação não encontrada.");

        // Busca o produto
        var produto = await _produtoRepository.GetByIdAsync(dto.ProdutoId);
        if (produto == null)
            throw new Exception("Produto não encontrado.");

        // Reverte a quantidade anterior
        produto.Quantidade += movimentacaoExistente.Tipo == "Entrada" ? -movimentacaoExistente.Quantidade : movimentacaoExistente.Quantidade;

        // Valida a nova quantidade para saídas
        if (dto.Tipo == "Saida" && produto.Quantidade < dto.Quantidade)
            throw new Exception("Quantidade insuficiente em estoque.");

        // Atualiza a movimentação
        var movimentacao = new Movimentacao
        {
            Id = id,
            ProdutoId = dto.ProdutoId,
            Tipo = dto.Tipo,
            Quantidade = dto.Quantidade,
            Data = dto.Data,
            Motivo = dto.Motivo
        };
        await _movimentacaoRepository.UpdateAsync(movimentacao);

        // Atualiza a quantidade do produto
        produto.Quantidade += dto.Tipo == "Entrada" ? dto.Quantidade : -dto.Quantidade;
        await _produtoRepository.UpdateAsync(produto);
    }

    public async Task DeleteAsync(int id)
    {
        // Busca a movimentação
        var movimentacao = await _movimentacaoRepository.GetByIdAsync(id);
        if (movimentacao == null)
            throw new Exception("Movimentação não encontrada.");

        // Reverte a quantidade do produto
        var produto = await _produtoRepository.GetByIdAsync(movimentacao.ProdutoId);
        if (produto != null)
        {
            produto.Quantidade += movimentacao.Tipo == "Entrada" ? -movimentacao.Quantidade : movimentacao.Quantidade;
            await _produtoRepository.UpdateAsync(produto);
        }

        // Exclui a movimentação
        await _movimentacaoRepository.DeleteAsync(id);
    }
}