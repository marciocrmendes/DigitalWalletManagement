using Ardalis.GuardClauses;
using DigitalWalletManagement.Commons.Options;
using DigitalWalletManagement.Entities;
using DigitalWalletManagement.Infraestructure;
using DigitalWalletManagement.Infraestructure.Context;
using DigitalWalletManagement.Infraestructure.Interceptors;
using DigitalWalletManagement.Infraestructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace DigitalWalletManagement
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraDependencies(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services
                .AddDbContextConfiguration(configuration)
                .AddIdentitySecurity(configuration);

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services
                .AddScoped<UserRepository>()
                .AddScoped<WalletRepository>();

            return services;
        }

        private static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services
                .AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>()
                .AddDbContext<AppDbContext>((sp, options) =>
                {
                    var connnectionString = configuration.GetConnectionString("Postgres");

                    Guard.Against.Null(connnectionString, message: "Connection string 'Postgres' not found.");

                    options.UseNpgsql(connnectionString, npgsqlOptions =>
                    {
                        npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                        npgsqlOptions.MigrationsAssembly(AppAssembly.Assembly);
                    });

                    options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                });

            return services;
        }

        private static IServiceCollection AddIdentitySecurity(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddIdentityApiEndpoints<User>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequiredLength = 8;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.MaxValue;
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;
                })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddJwtSecurity(configuration);

            return services;
        }

        private static IServiceCollection AddJwtSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection("JwtSettings");
            var securityKey = Encoding.UTF8.GetBytes(
                jwtSection["SecurityKey"] ?? throw new InvalidOperationException("JWT SecurityKey is not configured."));

            services
                .AddAuthorization()
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearerOptions =>
                {
                    bearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSection["Issuer"],
                        ValidateAudience = true,
                        ValidAudience = jwtSection["Audience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(securityKey),
                        ClockSkew = TimeSpan.Zero,
                        ValidateLifetime = true
                    };

                    bearerOptions.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json; charset=utf-8";
                            var message = "An error occurred processing your authentication.";
                            return context.Response.WriteAsync(JsonSerializer.Serialize(message));
                        },
                        //OnMessageReceived = context =>
                        //{
                        //    // For SignalR token handling or other contexts where token is in query string
                        //    var accessToken = context.Request.Query["access_token"];
                        //    if (!string.IsNullOrEmpty(accessToken))
                        //    {
                        //        context.Token = accessToken;
                        //    }
                        //    return Task.CompletedTask;
                        //}
                    };
                });

            return services;
        }
    }
}
