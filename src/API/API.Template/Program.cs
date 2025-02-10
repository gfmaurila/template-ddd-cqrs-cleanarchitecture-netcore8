using API.Template.Extensions;
using FluentValidation;

/// <summary>
/// Entry point for the application.
/// Configures services, middleware, and initializes the web API.
/// </summary>
var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

#region Dependency Injection Configuration

/// <summary>
/// Registers MediatR for handling application commands and queries.
/// </summary>
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

/// <summary>
/// Registers FluentValidation validators found in the current assembly.
/// </summary>
builder.Services.AddValidatorsFromAssembly(assembly);

/// <summary>
/// Configures controllers with JSON serialization options.
/// </summary>
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Converts enums to string representation in JSON responses.
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());

        // Uses CamelCase for JSON properties (optional).
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

/// <summary>
/// Registers additional services such as database connections.
/// </summary>
builder.Services.AddConnections();

/// <summary>
/// Enables API endpoint discovery.
/// </summary>
builder.Services.AddEndpointsApiExplorer();

#endregion

#region Swagger & Authentication Configuration

/// <summary>
/// Configures Swagger for API documentation.
/// </summary>
builder.Services.AddSwaggerConfig(builder.Configuration);

/// <summary>
/// Configures authentication services.
/// </summary>
builder.Services.UseAuthentication(builder.Configuration);

#endregion

#region Infrastructure Services Configuration

/// <summary>
/// Registers infrastructure services, such as repositories, cache, etc.
/// </summary>
//builder.Services.AddInfrastructureServices(builder.Configuration);

#endregion

#region Logging Configuration

/// <summary>
/// Configures Serilog for application logging.
/// </summary>
//builder.Host.UseSerilog((context, config) =>
//{
//    config.ReadFrom.Configuration(builder.Configuration);
//});

#endregion

var app = builder.Build();

#region Middleware Pipeline Configuration

/// <summary>
/// Applies database migrations at runtime.
/// </summary>
//app.ApplyMigrations();

/// <summary>
/// Enables Swagger UI for API exploration.
/// </summary>
app.UseSwagger();
app.UseSwaggerUI();

/// <summary>
/// Enforces HTTPS for all requests.
/// </summary>
app.UseHttpsRedirection();

/// <summary>
/// Enables authorization middleware.
/// </summary>
app.UseAuthorization();

/// <summary>
/// Maps controllers to endpoints.
/// </summary>
app.MapControllers();

#endregion

/// <summary>
/// Starts the application.
/// </summary>
app.Run();

/// <summary>
/// Defines a partial class for `Program`, allowing test configurations if necessary.
/// </summary>
public partial class Program { }