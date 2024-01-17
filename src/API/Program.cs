using API.Filters;
using Application.Extensions;
using Domain.SeedWork;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services

var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<TransactionalContextFilter>();
});

builder.Services.AddInfrastructureDependencies(configuration);
builder.Services.AddApplicationDependencies();
builder.Services.AddDomainDependencies();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.CreateDatabase(configuration);
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();