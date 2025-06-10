using EstoqueManager.Application.DTOs;
using EstoqueManager.Application.DTOs.Create;
using EstoqueManager.Application.DTOs.Read;
using EstoqueManager.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueManager.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ItensPedidosController : ControllerBase
{
    private readonly IItemPedidoService _itemPedidoService;

    public ItensPedidosController(IItemPedidoService itemPedidoService)
    {
        _itemPedidoService = itemPedidoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetItemPedidoDTO>>> GetAll()
    {
        var itens = await _itemPedidoService.GetAllAsync();
        return Ok(itens);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetItemPedidoDTO>> GetById(int id)
    {
        var item = await _itemPedidoService.GetByIdAsync(id);
        return Ok(item);
    }

    [HttpPost]
    
    public async Task<ActionResult> Create([FromBody] CreateItemPedidoDTO dto)
    {
        await _itemPedidoService.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = 0 }, dto);
    }

    [HttpPut("{id}")]
    
    public async Task<ActionResult> Update(int id, [FromBody] CreateItemPedidoDTO dto)
    {
        await _itemPedidoService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    
    public async Task<ActionResult> Delete(int id)
    {
        await _itemPedidoService.DeleteAsync(id);
        return NoContent();
    }
}