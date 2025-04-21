using BibliotecaAPI.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("UIPolicy",
        policy => policy
            .AllowAnyOrigin() // Permite solicitudes de cualquier origen
            .AllowAnyMethod() // Permite cualquier método (GET, POST, etc.)
            .AllowAnyHeader()); // Permite cualquier encabezado
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("UIPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
