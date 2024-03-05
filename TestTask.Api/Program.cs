using Microsoft.AspNetCore.Mvc.Filters;
using TestTask.Api.Filters;
using TestTask.Application;
using TestTask.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(config => config.Filters.Add<ErrorHandlingFilterAttribute>());

builder.Services
    .AddDbAccess(builder.Configuration)
    .AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
