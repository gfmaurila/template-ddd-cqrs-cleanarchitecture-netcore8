using API.Template.Extensions.Consts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace API.Template.Extensions;


public static class SwaggerConfig
{
    /// <summary>
    /// Configures Swagger for API documentation.
    /// </summary>
    /// <param name="services">The service collection to add Swagger configuration.</param>
    /// <param name="conf">The application configuration.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services, IConfiguration conf)
    {
        services.AddSwaggerGen(c =>
        {
            // Define API documentation version and title
            c.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "API Template",
                    Version = "v1"
                }
            );

            // Security definition for Swagger authentication
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            // Security requirement for Swagger authentication
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        return services;
    }

    /// <summary>
    /// Configures authentication for the application using JWT tokens.
    /// </summary>
    /// <param name="services">The service collection to add authentication configuration.</param>
    /// <param name="configuration">The application configuration.</param>
    public static void UseAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration.GetValue<string>(ConfigConsts.Issuer),
                    ValidAudience = configuration.GetValue<string>(ConfigConsts.Audience),
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration.GetValue<string>(ConfigConsts.Key)))
                };
            });
    }

    /// <summary>
    /// Enables Swagger UI in development mode.
    /// </summary>
    /// <param name="app">The application builder to configure Swagger UI.</param>
    public static void UseDevelopmentSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API.Template");
        });
    }
}



