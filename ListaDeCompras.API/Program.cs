using ListaDeCompras.BW.CU;
using ListaDeCompras.DA.Acciones;
using ListaDeCompras.DA;
using Microsoft.EntityFrameworkCore;
using ListaDeCompras.DA.Interfaces;
using ListaDeCompras.BW.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Configurar CORS (permite peticiones desde Alexa u otros orígenes)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Configurar Entity Framework Core con SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de dependencias entre capas
builder.Services.AddTransient<IGestionListaBW, GestionListaBW>();
builder.Services.AddTransient<IGestionListaDA, GestionListaDA>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Activar CORS
app.UseCors("AllowAllOrigins");

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

