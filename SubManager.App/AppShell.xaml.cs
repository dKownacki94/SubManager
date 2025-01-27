using SubManager.App.Views;

namespace SubManager.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("edit", typeof(SubscriptionEditPage));
        }
    }
}
