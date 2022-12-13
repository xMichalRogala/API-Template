using Auth.Domain.Attributes;
using Auth.Domain.Schemas.Enums;
using Auth.Messages.Commands;
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


app.MapGet("/readPerm", [HasPermission(Permission.Read)] async (ICommandDispatcher commandDispatcher) =>
{
    await commandDispatcher.DispatchAsync(new CommandTest());
});

app.MapGet("/accessPerm", [HasPermission(Permission.Access)] () => "Hello World!");

app.Run();