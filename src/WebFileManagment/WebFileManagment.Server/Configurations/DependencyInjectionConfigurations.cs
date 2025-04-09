using WebFileManagment.Service.Services;
using WebFileManagment.StorageBroker.Services;

namespace WebFileManagment.Server.Configurations;

public static class DependencyInjectionConfigurations
{
    public static void Configurations(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IStorageService, StorageService>();
        builder.Services.AddSingleton<IStorageBrokerService, LocalStorageBrokerService>();
    }
}
