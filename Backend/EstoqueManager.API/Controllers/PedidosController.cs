using EstoqueManager.Application.DTOs.Create;
using EstoqueManager.Application.DTOs.Read;
using EstoqueManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueManager.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidosController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetPedidoDTO>>> GetAll()
    {
        var pedidos = await _pedidoService.GetAllAsync();
        return Ok(pedidos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetPedidoDTO>> GetById(int id)
    {
        try
        {
            var pedido = await _pedidoService.GetByIdAsync(id);
            return Ok(pedido);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreatePedidoDTO dto)
    {
        await _pedidoService.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = 0 }, dto); // Id temporário
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] CreatePedidoDTO dto)
    {
        await _pedidoService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _pedidoService.DeleteAsync(id);
        return NoContent();
    }
}
