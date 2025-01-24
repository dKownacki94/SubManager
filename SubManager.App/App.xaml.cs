using Microsoft.EntityFrameworkCore;
using SubManager.Infrastructure.Data;

namespace SubManager.App
{
    public partial class App : Application
    {
        public App(AppDbContext dbContext)
        {
            InitializeComponent();
            dbContext.Database.Migrate();
            MainPage = new AppShell();
        }
    }
}
