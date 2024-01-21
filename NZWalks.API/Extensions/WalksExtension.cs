using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Repositories.Class;
using NZWalks.API.Repositories.Interface;


namespace NZWalks.API.Extensions;

public static class WalksExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddControllers();
        services.AddEndpointsApiExplorer(); 
        services.AddSwaggerGen();
        services.AddScoped<IRegionRepository, RegionRepository>();
        services.AddScoped<IWalkRepository, WalkRepository>();
        services.AddAutoMapper(typeof(MappingProfiles));
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}