using LojaApi.Data;
using LojaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaApi.Controllers;

[ApiController]
[Route("api/fornecedor")]
public class FornecedorController : ControllerBase
{
    private readonly AppDbContext _context;

    public FornecedorController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var fornecedores = await _context.Fornecedores.ToListAsync();
        return Ok(fornecedores);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Fornecedor fornecedor)
    {
        _context.Fornecedores.Add(fornecedor);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { codigo = fornecedor.Codigo }, fornecedor);
    }

    [HttpPut("{codigo}")]
    public async Task<IActionResult> Update(int codigo, [FromBody] Fornecedor fornecedor)
    {
        var existing = await _context.Fornecedores.FindAsync(codigo);
        if (existing is null) return NotFound();

        existing.Nome = fornecedor.Nome;
        existing.Cnpj = fornecedor.Cnpj;
        existing.Email = fornecedor.Email;
        existing.Telefone = fornecedor.Telefone;

        await _context.SaveChangesAsync();
        return Ok(existing);
    }

    [HttpDelete("{codigo}")]
    public async Task<IActionResult> Delete(int codigo)
    {
        var existing = await _context.Fornecedores.FindAsync(codigo);
        if (existing is null) return NotFound();

        _context.Fornecedores.Remove(existing);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // Questão 7
    [HttpGet("nome/{nome}")]
    public async Task<IActionResult> GetByNome(string nome)
    {
        var fornecedores = await _context.Fornecedores
            .Where(f => f.Nome.Contains(nome))
            .ToListAsync();
        return Ok(fornecedores);
    }
}