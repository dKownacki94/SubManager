using SubManager.App.ViewModels;
using SubManager.Core.Entities;

namespace SubManager.App.Views;

public partial class SubscriptionListPage : ContentPage
{
    public SubscriptionListPage(SubscriptionListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}