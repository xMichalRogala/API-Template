using TemplateApi.Configuration.Extensions;
using TemplateApi.CQRS.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddCustomServices();
builder.AddAuthDbContext();
builder.AddApplicationDbContext();
builder.Services.AddCustomCqrs(commandOpt =>
{
    commandOpt.AllowCommandExecuteByMoreThanOneCommandHandler = false;
}, eventOpt =>
{
    eventOpt.ParallelDegree = 2;
    eventOpt.Delay = 5000;
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

app.AddCustomBackgroundTasks();

app.Run();