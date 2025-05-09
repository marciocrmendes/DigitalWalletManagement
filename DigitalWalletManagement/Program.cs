using DigitalWalletManagement;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services
    .AddFastEndpoints()
    .AddInfraDependencies(configuration)
    .AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseFastEndpoints()
   .UseHttpsRedirection();

await app.RunAsync();
