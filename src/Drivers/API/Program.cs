using API.Filters;
using Application.Extensions;
using Entities.SeedWork;
using External.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services

var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.EnableAnnotations();
});
builder.Services.AddControllers(options =>
{
    options.Filters.Add<TransactionalContextFilter>();
});

builder.Services.AddInfrastructureDependencies(configuration);
builder.Services.AddApplicationDependencies();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseReDoc(c =>
    {
        c.DocumentTitle = "FastFood API Documentation - Tech Challenge FIAP";
        c.SpecUrl = "/swagger/v1/swagger.json";
    });

    app.CreateDatabase(configuration);
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();