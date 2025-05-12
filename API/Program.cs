using PRUEBA.APLICATION.INTERFACE;
using PRUEBA.APLICATION.SERVICES;
using PRUEBA.DOMINIO.INTERFACE;
using PRUEBA.INFRAESTRUCTURE;
using PRUEBA.INFRAESTRUCTURE.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddSingleton<DapperContext>(provider =>
    new DapperContext(builder.Configuration.GetConnectionString("DefaultConnection"))
);
// Add services to the container.
builder.Services.AddScoped<ISolicitudRepository, SolicitudRepository>();

// Registrar el servicio
builder.Services.AddScoped<ISolicitudService, SolicitudService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
