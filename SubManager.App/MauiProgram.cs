using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Filename=SubManager.db"));

            builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
