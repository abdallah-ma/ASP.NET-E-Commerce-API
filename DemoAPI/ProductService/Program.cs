using Microsoft.EntityFrameworkCore;
using ProductService.Helpers;
using ProductService.Extensions;
using ProductService;
using DemoAPI.Common;
using DemoAPI.Common.MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddMassTransitServiceWithRabbitMQ();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<BaseDbContext>(provider => provider.GetRequiredService<AppDbContext>());
builder.Services.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
    
    app.MapOpenApi();
}

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var _dbcontext = services.GetRequiredService<AppDbContext>();

var LoggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
	await _dbcontext.Database.MigrateAsync();
    await StorecontextSeed.SeedAsync(_dbcontext);
}
catch (Exception ex)
{
    var logger = LoggerFactory.CreateLogger<AppDbContext>();
    logger.LogError(ex, ":P");
}

app.UseHttpsRedirection();

app.UseRouting();



app.UseEndpoints(endpoints => endpoints.MapControllers());
app.Run();
