using Auth.Domain.Attributes;
using Auth.Domain.Schemas.Enums;
using TemplateApi.Configuration.Extensions;
using TemplateApi.CQRS.Commands.Abstract;
using TemplateApi.CQRS.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddCustomServices();
builder.AddAuthDbContext();
builder.AddApplicationDbContext();
builder.Services.AddCustomCqrs(commandOptions =>
{
    commandOptions.MaxDegreeOfParaleism = 5;
    commandOptions.AllowCommandExecuteByMoreThanOneCommandHandler = true;
});
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