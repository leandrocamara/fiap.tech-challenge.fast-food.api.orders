using Application.Extensions;
using Domain.SeedWork;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddInfrastructureDependencies(builder.Configuration);
builder.Services.AddApplicationDependencies();
builder.Services.AddDomainDependencies();

#endregion

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