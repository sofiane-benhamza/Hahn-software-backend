using Microsoft.EntityFrameworkCore;
using TodoListApp.Infrastructure.Data;
using TodoListApp.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Register application service
builder.Services.AddScoped<ITodoService, TodoService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyIP", policy =>
    {
        policy
            .SetIsOriginAllowed(origin =>
            {
                // Parse origin and check if port is 80
                if (Uri.TryCreate(origin, UriKind.Absolute, out var uri))
                {
                    return uri.Port == 80; // allow any IP as long as port is 80
                }
                return false;
            })
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});



// Add DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("DEFAULT_CONNECTION")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
});

var app = builder.Build();

app.UseCors("AllowAnyIP");

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
