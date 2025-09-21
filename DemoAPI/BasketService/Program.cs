using BasketService.Extensions;
using StackExchange.Redis;
using DemoAPI.Common.MassTransit;
using BasketService;
using Grpc.AspNetCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;

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
builder.Services.AddGrpc();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(4055, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

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

app.UseGrpcWeb();

app.UseAuthentication();
app.UseAuthorization();



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<BasketService.BasketService>().EnableGrpcWeb();
});

app.Run();

