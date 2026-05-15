using System.ComponentModel.DataAnnotations;

namespace LojaApi.Models;

public class Fornecedor
{
    [Key]
    public int Codigo { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
}