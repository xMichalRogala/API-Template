using TemplateApi.Configuration.Extensions;
using TemplateApi.CQRS.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddCustomServices();
builder.AddAuthDbContext();
builder.AddApplicationDbContext();
builder.Services.AddCustomCqrs(opt =>
{
    opt.AllowCommandExecuteByMoreThanOneCommandHandler = false;
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