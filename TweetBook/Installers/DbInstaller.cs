using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TweetBook.Data;
using TweetBook.Services;

namespace TweetBook.Installers;

public class DbInstaller: IInstaller
{
    public void InstallServices(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        serviceCollection.AddDbContext<DataContext>(options =>
            options.UseSqlite(connectionString));
        serviceCollection.AddDatabaseDeveloperPageExceptionFilter();

        serviceCollection.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<DataContext>();

        serviceCollection.AddSingleton<IPostService, PostService>();
    }
}