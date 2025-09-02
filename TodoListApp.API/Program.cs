using Microsoft.EntityFrameworkCore;
using TodoListApp.Infrastructure.Data;
using TodoListApp.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Register application service
builder.Services.AddScoped<ITodoService, TodoService>();


builder.Services.AddCors(options =>
{
    
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:8080") // frontend URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


// Add DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowFrontend");

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
