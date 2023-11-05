using Microsoft.OpenApi.Models;

namespace TweetBook.Installers;

public class MvcInstaller : IInstaller
{
    public void InstallServices(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddControllersWithViews();
        serviceCollection.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });
    }
}