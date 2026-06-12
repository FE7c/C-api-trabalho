using LojaApi.Data;
using LojaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaApi.Controllers;

[ApiController]
[Route("api/cliente")]
public class ClienteController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClienteController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clientes = await _context.Clientes.ToListAsync();
        return Ok(clientes);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { codigo = cliente.Codigo }, cliente);
    }

    [HttpPut("{codigo}")]
    public async Task<IActionResult> Update(int codigo, [FromBody] Cliente cliente)
    {
        var existing = await _context.Clientes.FindAsync(codigo);
        if (existing is null) return NotFound();

        existing.Nome = cliente.Nome;
        existing.Email = cliente.Email;
        existing.Telefone = cliente.Telefone;

        await _context.SaveChangesAsync();
        return Ok(existing);
    }

    [HttpDelete("{codigo}")]
    public async Task<IActionResult> Delete(int codigo)
    {
        var existing = await _context.Clientes.FindAsync(codigo);
        if (existing is null) return NotFound();

        _context.Clientes.Remove(existing);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}