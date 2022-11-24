using TemplateApi.Configuration.Startup;

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
app.AddCustomMiddlewares();
app.AddCustomEndpoints();

app.Run();