using DigitalWalletManagement;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services
    .AddFastEndpoints()
    .AddOpenApi()
    .AddApplicationConfiguration(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TelehealthCenter API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints()
   .UseHttpsRedirection();

await app.RunAsync();
