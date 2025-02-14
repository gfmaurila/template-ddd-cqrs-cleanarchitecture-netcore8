using API.Template.Extensions;
using System.Reflection;
using System.Text.Json.Serialization;
using Template.Application;
using Template.Common.Domain.Errors;
using Template.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddSwaggerConfig(builder.Configuration);
builder.Services.UseAuthentication(builder.Configuration);

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new ResultJsonConverter<Guid, IDomainError>());

        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.Converters.Add(new ResultJsonConverter<object, object>());
    });

var app = builder.Build();

// Apply migrations at runtime
app.ApplyMigrations();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();

app.Run();
public partial class Program { }