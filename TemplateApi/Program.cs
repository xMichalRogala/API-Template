using Auth.Domain.Models;
using TemplateApi.Configuration.Extensions;

var builder = WebApplication.CreateBuilder(args);

var hehe = new SignInDto();

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

app.Run();