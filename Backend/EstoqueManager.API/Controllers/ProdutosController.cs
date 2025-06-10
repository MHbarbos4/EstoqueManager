using EstoqueManager.Application.DTOs;
using EstoqueManager.Application.DTOs.Create;
using EstoqueManager.Application.DTOs.Read;
using EstoqueManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueManager.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetProdutoDTO>>> GetAll()
    {
        var produtos = await _produtoService.GetAllAsync();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetProdutoDTO>> GetById(int id)
    {
        try
        {
            var produto = await _produtoService.GetByIdAsync(id);
            return Ok(produto);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateProdutoDTO dto)
    {
        try
        {
            var id = await _produtoService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] CreateProdutoDTO dto)
    {
        try
        {
            await _produtoService.UpdateAsync(id, dto);
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
            await _produtoService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}