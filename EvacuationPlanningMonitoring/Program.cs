using EvacuationPlanningMonitoring.Repositorys;
using EvacuationPlanningMonitoring.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
    }
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IConnectionMultiplexer>(cfg =>
{
    var redisConnectionString = builder.Configuration.GetConnectionString("RedisConnection");
    if (string.IsNullOrEmpty(redisConnectionString))
    {
        redisConnectionString = Environment.GetEnvironmentVariable("RedisConnection");
    }
    IConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
    return multiplexer;
});

builder.Services.AddMyServices(builder.Configuration);
builder.Services.AddRepository(builder.Configuration);
var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", false);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
