using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubManager.App.Views;
using SubManager.Core.Entities;
using SubManager.Core.Interfaces;
using System.Collections.ObjectModel;

namespace SubManager.App.ViewModels
{
    public partial class SubscriptionListViewModel : ObservableObject
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionListViewModel(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
            Subscriptions = new ObservableCollection<Subscription>();
            LoadSubscriptionsCommand.Execute(null);
        }

        [ObservableProperty]
        public ObservableCollection<Subscription> subscriptions;

        [RelayCommand]
        private async Task LoadSubscriptions()
        {
            var subs = await _subscriptionService.GetSubscriptionsAsync();
            Subscriptions.Clear();
            foreach (var sub in subs)
            {
                Subscriptions.Add(sub);
            }
        }

        [RelayCommand]
        private async Task AddSubscription()
        {
            await Shell.Current.GoToAsync("SubscriptionEditPage");
        }

        [RelayCommand]
        private async Task SelectedSubscriptionChanged(Subscription selectedSubscription)
        {
            if (selectedSubscription != null)
            {
               // await Shell.Current.GoToAsync($"{nameof(Views.SubscriptionDetailPage)}?id={selectedSubscription.Id}");
            }
        }
    }
}