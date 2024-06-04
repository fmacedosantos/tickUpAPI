using api.Services;
using TickUp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = "ConexaoPadrao";
builder.Services.AddSingleton<UsuarioService>(new UsuarioService(connectionString));
builder.Services.AddSingleton<EventoService>(new EventoService(connectionString));
builder.Services.AddSingleton<IngressoService>(new IngressoService(connectionString));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
