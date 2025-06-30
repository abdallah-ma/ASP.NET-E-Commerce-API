using AccountService.Extensions;
using AccountService;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Builder.Extensions;
using DemoAPI.Common.MassTransit;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddMassTransitServiceWithRabbitMQ();

builder.Services.AddSingleton<IConnectionMultiplexer>( (ServiceProvider) =>
            {
            var connection = builder.Configuration.GetConnectionString("Redis");
            return ConnectionMultiplexer.Connect(connection);
            });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServices();
builder.Services.AddIdentityServices(builder.Configuration);


builder.Services.AddDbContext<AppIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

var app = builder.Build();




if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}




app.UseRouting();
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});
app.Run();

