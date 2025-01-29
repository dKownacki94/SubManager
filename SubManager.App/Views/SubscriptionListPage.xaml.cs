using SubManager.App.ViewModels;

namespace SubManager.App.Views;

public partial class SubscriptionListPage : ContentPage
{
    public SubscriptionListPage(SubscriptionListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}