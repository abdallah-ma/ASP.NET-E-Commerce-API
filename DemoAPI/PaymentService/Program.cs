using DemoAPI.Common.MassTransit;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using PaymentService.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddMassTransitServiceWithRabbitMQ();
builder.Services.AddGrpc();
builder.Services.AddServices(builder.Configuration);

builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(4057, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

var app = builder.Build();



// Configure the HTTP request pipeline.
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
app.UseHttpsRedirection();

app.UseRouting();

app.UseGrpcWeb();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<PaymentService.PaymentMicroService>().EnableGrpcWeb();
    
});


app.UseEndpoints(endpoints => endpoints.MapControllers());


app.Run();

