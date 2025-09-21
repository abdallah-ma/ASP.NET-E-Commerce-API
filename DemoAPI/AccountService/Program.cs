using AccountService.Extensions;
using AccountService;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();




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
    app.UseSwaggerUI(options => 
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

