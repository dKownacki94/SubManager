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

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var viewModel = BindingContext as SubscriptionListViewModel;
        if (viewModel != null && e.CurrentSelection.FirstOrDefault() is Subscription selectedSubscription)
        {
            viewModel.SelectedSubscriptionChangedCommand.Execute(selectedSubscription);
        }

        ((CollectionView)sender).SelectedItem = null;
    }
}