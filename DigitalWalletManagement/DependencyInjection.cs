using Ardalis.GuardClauses;
using DigitalWalletManagement.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DigitalWalletManagement
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraDependencies(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                var connnectionString = configuration.GetConnectionString("");

                Guard.Against.Null(connnectionString, message: "Connection string 'DefaultConnection' not found.");

                options.UseNpgsql(connnectionString, npgsqlOptions =>
                {
                    npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                });

                //options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            });

            return services;
        }
    }
}
