using Auth.Domain.Attributes;
using Auth.Domain.Schemas.Enums;
using TemplateApi.Configuration.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddCustomServices();
builder.AddAuthDbContext();
builder.AddApplicationDbContext();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.AddCustomMiddlewares();
app.AddCustomEndpoints();


app.MapGet("/readPerm", [HasPermission(Permission.Read)]() => "Hello World!");

app.MapGet("/accessPerm", [HasPermission(Permission.Access)] () => "Hello World!");

app.MapGet("/accessWithoutPerm", () => "Hello World!");

app.Run();