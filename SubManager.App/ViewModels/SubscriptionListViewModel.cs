using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        }

        [ObservableProperty]
        private Subscription _selectedSubscription;

        [ObservableProperty]
        public ObservableCollection<Subscription> subscriptions;

        [RelayCommand]
        void Appearing()
        {
            LoadSubscriptionsCommand.Execute(null);
        }

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
            await Shell.Current.GoToAsync("edit");
        }

        [RelayCommand]
        private async Task SelectedSubscriptionChanged(Subscription selectedSubscription)
        {
            if (selectedSubscription != null)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "id",selectedSubscription.Id}
                };

               await Shell.Current.GoToAsync("edit",parameters);
               SelectedSubscription = null;
            }
        }
    }
}