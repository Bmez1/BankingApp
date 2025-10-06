using Application.Abstractions.Authentication;

using Domain.Repositories;

using Infrastructure.Authentication;
using Infrastructure.Database;
using Infrastructure.DomainEvents;
using Infrastructure.Outbox;
using Infrastructure.Repositories;
using Infrastructure.Time;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using SharedKernel;

using System.Text;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddServices()
            .AddDatabase(configuration)
            .AddHealthChecks(configuration)
            .AddAuthenticationInternal(configuration);

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IDomainEventsDispatcher, DomainEventsDispatcher>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        // Register the outbox
        services.AddHostedService<ProcessOutboxMessagesJob>();

        services.Scan(scan => scan
            .FromAssemblyOf<ApplicationDbContext>()
            .AddClasses(classes => classes.AssignableTo<IRepository>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseSqlServer(connectionString));

        return services;
    }

    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("Database")!);

        return services;
    }

    private static IServiceCollection AddAuthenticationInternal(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}
