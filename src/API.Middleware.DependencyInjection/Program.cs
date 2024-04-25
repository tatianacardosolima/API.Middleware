using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionando memória de cache
builder.Services.AddMemoryCache();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    if (!context.Request.Headers.TryGetValue("X-API-KEY", out var xapikey)
        && context.GetEndpoint()?.Metadata.GetMetadata<AllowAnonymousAttribute>() is null)
    {
        await context.Response.WriteAsync("Não foi possível localizar a API Key");
        return;
    }
    await next.Invoke();
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
