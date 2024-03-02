using Audium.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Audium.Persistance;

public static class ContextExtension
{
    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<AudiumDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Audium"));
        });
    }
}
