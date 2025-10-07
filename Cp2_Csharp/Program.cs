using Cp2_CsharpBusiness;
using Cp2_CsharpData;
using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configuração do banco de dados Oracle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle("User Id=rm98323;Password=030904;Data Source=oracle.fiap.com.br/orcl;"));

// Registrando o serviço de Cliente (injeção de dependência)
builder.Services.AddScoped<IClienteService, ClienteService>();

// Configuração do Swagger (para documentação da API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilitar a autorização (caso você use autenticação em algum ponto)
app.UseAuthorization();

// Mapear os controllers
app.MapControllers();

// Rodar a aplicação
app.Run();
