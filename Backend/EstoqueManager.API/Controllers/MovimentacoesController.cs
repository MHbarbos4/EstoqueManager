using EstoqueManager.Application.DTOs.Create;
using EstoqueManager.Application.DTOs.Read;
using EstoqueManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueManager.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimentacoesController : ControllerBase
{
    private readonly IMovimentacaoService _movimentacaoService;

    public MovimentacoesController(IMovimentacaoService movimentacaoService)
    {
        _movimentacaoService = movimentacaoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetMovimentacaoDTO>>> GetAll()
    {
        var movimentacoes = await _movimentacaoService.GetAllAsync();
        return Ok(movimentacoes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetMovimentacaoDTO>> GetById(int id)
    {
        try
        {
            var movimentacao = await _movimentacaoService.GetByIdAsync(id);
            return Ok(movimentacao);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateMovimentacaoDTO dto)
    {
        try
        {
            await _movimentacaoService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = 0 }, dto); // Id temporário
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] CreateMovimentacaoDTO dto)
    {
        try
        {
            await _movimentacaoService.UpdateAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _movimentacaoService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}