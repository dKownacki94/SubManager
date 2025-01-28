using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SubManager.App.ViewModels;
using SubManager.App.Views;
using SubManager.Core.Interfaces;
using SubManager.Infrastructure.Data;
using SubManager.Infrastructure.Repositories;
using SubManager.Infrastructure.Services;

namespace SubManager.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            string dbPath = Path.Combine(FileSystem.Current.AppDataDirectory, "SubManager.db");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Filename={dbPath}"));

            builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

            builder.Services.AddTransient<SubscriptionListPage>();
            builder.Services.AddTransient<SubscriptionListViewModel>();

            builder.Services.AddTransient<SubscriptionEditPage>();
            builder.Services.AddTransient<SubscriptionEditViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
