using Cp2_CsharpBusiness;
using Cp2_CsharpData;
using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configura��o do banco de dados Oracle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle("User Id=rm98323;Password=030904;Data Source=oracle.fiap.com.br/orcl;"));

// Registrando o servi�o de Cliente (inje��o de depend�ncia)
builder.Services.AddScoped<IClienteService, ClienteService>();

// Configura��o do Swagger (para documenta��o da API)
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

// Habilitar a autoriza��o (caso voc� use autentica��o em algum ponto)
app.UseAuthorization();

// Mapear os controllers
app.MapControllers();

// Rodar a aplica��o
app.Run();
