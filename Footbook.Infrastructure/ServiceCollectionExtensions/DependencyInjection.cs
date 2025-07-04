using System.Text;
using CloudinaryDotNet;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Footbook.Data.DataAccess;
using Footbook.Infrastructure.Tokens;
using Footbook.Data.Repositories.Interfaces;
using Footbook.Data.Repositories.Implementations;
using Footbook.Infrastructure.ExternalServices.Cloudinary;
using Footbook.Infrastructure.Services.Interfaces;
using Footbook.Infrastructure.Services.Implementations;
using Footbook.Infrastructure.Validators.Auth;
using Microsoft.Extensions.Options;

namespace Footbook.Infrastructure.ServiceCollectionExtensions;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IStadiumRepository, StadiumRepository>();
        services.AddScoped<IFieldRepository, FieldRepository>();
        services.AddScoped<ISlotRepository, SlotRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        
        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IStadiumService, StadiumService>();
        services.AddScoped<IFieldService, FieldService>();
        services.AddScoped<ISlotService, SlotService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<INotificationService, NotificationService>();
        
        return services;
    }
    
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        #region Azure SQLServer DB
        
        // services.AddDbContext<AppDbContext>(options =>
        //     options.UseSqlServer(
        //         configuration.GetConnectionString("DefaultConnection"),
        //         sqlServerOptions => 
        //         {
        //             sqlServerOptions.EnableRetryOnFailure(
        //                 maxRetryCount: 5,
        //                 maxRetryDelay: TimeSpan.FromSeconds(10),
        //                 errorNumbersToAdd: null);
        //             sqlServerOptions.MigrationsAssembly("Footbook.Data");
        //         }));
        
        #endregion
        
        #region Local DB
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("LocalConnection")));
        
        #endregion
        
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

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<SignupRequestValidator>();
        return services;
    }
    
    public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region Cloudinary
        
        services.Configure<CloudinaryOptions>(configuration.GetSection("Cloudinary"));
        services.AddSingleton<Cloudinary>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<CloudinaryOptions>>().Value;
            var account = new Account(options.CloudName, options.ApiKey, options.ApiSecret);
            return new Cloudinary(account);
        });
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        
        #endregion
        
        return services;
    }
}
