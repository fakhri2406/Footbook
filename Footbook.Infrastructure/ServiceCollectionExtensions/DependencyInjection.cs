using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Footbook.Data.DataAccess;
using Footbook.Infrastructure.Tokens;

namespace Footbook.Infrastructure.ServiceCollectionExtensions;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // TODO: register repository implementations, e.g.: 
        // services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // TODO: register business services, e.g.: 
        // services.AddScoped<IUserService, UserService>();
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ITokenGenerator, TokenGenerator>();

        var jwtSection = configuration.GetSection("Jwt");
        var keyBytes = Encoding.UTF8.GetBytes(jwtSection["Key"]!);

        services.Configure<JwtOptions>(opts =>
        {
            opts.Issuer = jwtSection["Issuer"]!;
            opts.Audience = jwtSection["Audience"]!;
            opts.AccessValidFor = TimeSpan.FromMinutes(30);
            opts.SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256);
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSection["Issuer"],
                    ValidAudience = jwtSection["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
                };
            });

        return services;
    }

    public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
    {

        return services;
    }
}
