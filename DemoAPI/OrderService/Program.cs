using OrderService.Extensions;
using Microsoft.EntityFrameworkCore;
using OrderService;
using DemoAPI.Common;
using OrderService.Helpers;
using DemoAPI.Common.MassTransit;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddMassTransitServiceWithRabbitMQ();

builder.Services.AddServices();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<BaseDbContext>(provider => provider.GetRequiredService<AppDbContext>());



var app = builder.Build();

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

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());
app.Run();

