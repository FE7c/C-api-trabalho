using LojaApi.Data;
using LojaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaApi.Controllers;

[ApiController]
[Route("api/vendedor")]
public class VendedorController : ControllerBase
{
    private readonly AppDbContext _context;

    public VendedorController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var vendedores = await _context.Vendedores.ToListAsync();
        return Ok(vendedores);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Vendedor vendedor)
    {
        _context.Vendedores.Add(vendedor);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { codigo = vendedor.Codigo }, vendedor);
    }

    [HttpPut("{codigo}")]
    public async Task<IActionResult> Update(int codigo, [FromBody] Vendedor vendedor)
    {
        var existing = await _context.Vendedores.FindAsync(codigo);
        if (existing is null) return NotFound();

        existing.Nome = vendedor.Nome;
        existing.Email = vendedor.Email;
        existing.Telefone = vendedor.Telefone;
        existing.Salario = vendedor.Salario;

        await _context.SaveChangesAsync();
        return Ok(existing);
    }

    [HttpDelete("{codigo}")]
    public async Task<IActionResult> Delete(int codigo)
    {
        var existing = await _context.Vendedores.FindAsync(codigo);
        if (existing is null) return NotFound();

        _context.Vendedores.Remove(existing);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // Questão 8
    [HttpGet("salario/{valor}")]
    public async Task<IActionResult> GetBySalario(decimal valor)
    {
        var vendedores = await _context.Vendedores
            .Where(v => v.Salario > valor)
            .ToListAsync();
        return Ok(vendedores);
    }
}