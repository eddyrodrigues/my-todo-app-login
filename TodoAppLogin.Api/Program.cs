using static TodoAppLogin.Api.Extensions.SwaggerExtensions;
using static TodoAppLogin.Api.Extensions.IdentityExtensions;
using static TodoAppLogin.Api.Extensions.ApiExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.ApiConfiguration();
builder.Services.AddEndpointsApiExplorer();
builder.Services.SwaggerConfiguration();
builder.Services.IdentityConfiguration();

using var log = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

Log.Logger = log;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
