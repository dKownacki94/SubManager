using SubManager.App.ViewModels;

namespace SubManager.App.Views;

public partial class SubscriptionEditPage : ContentPage
{
	public SubscriptionEditPage(SubscriptionEditViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}