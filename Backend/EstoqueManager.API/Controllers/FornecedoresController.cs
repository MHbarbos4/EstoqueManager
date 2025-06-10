using EstoqueManager.Application.DTOs;
using EstoqueManager.Application.DTOs.Create;
using EstoqueManager.Application.DTOs.Read;
using EstoqueManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueManager.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FornecedoresController : ControllerBase
{
    private readonly IFornecedorService _fornecedorService;

    public FornecedoresController(IFornecedorService fornecedorService)
    {
        _fornecedorService = fornecedorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetFornecedorDTO>>> GetAll()
    {
        var fornecedores = await _fornecedorService.GetAllAsync();
        return Ok(fornecedores);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetFornecedorDTO>> GetById(int id)
    {
        try
        {
            var fornecedor = await _fornecedorService.GetByIdAsync(id);
            return Ok(fornecedor);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateFornecedorDTO dto)
    {
        try
        {
            var id = await _fornecedorService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] CreateFornecedorDTO dto)
    {
        try
        {
            await _fornecedorService.UpdateAsync(id, dto);
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
            await _fornecedorService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}