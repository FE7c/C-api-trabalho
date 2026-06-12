using LojaApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Controllers
builder.Services.AddControllers();

// 🔹 Banco de dados (SQLite)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString)
);

// 🔹 Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Chave secreta do token
var chaveJwt = "MINHA_CHAVE_SUPER_SECRETA_123456";
// Configura autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
options.TokenValidationParameters = new TokenValidationParameters
{
// Valida chave
ValidateIssuerSigningKey = true,
// Define chave secreta
IssuerSigningKey =
new SymmetricSecurityKey(
Encoding.UTF8.GetBytes(chaveJwt)
),
// Não valida servidor
ValidateIssuer = false,
// Não valida cliente
ValidateAudience = false
};
});

builder.Services.AddAuthorization();

var app = builder.Build();

// 🔹 Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔹 CORS (deve vir antes de UseAuthentication)
app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();