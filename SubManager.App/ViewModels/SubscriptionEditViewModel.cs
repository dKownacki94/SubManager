using SubManager.Core.Entities;
using SubManager.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SubManager.App.ViewModels
{
    [QueryProperty(nameof(SubscriptionId), "id")]
    public partial class SubscriptionEditViewModel : ObservableObject
    {
        private readonly ISubscriptionService _subscriptionService;
        private Subscription _subscription;

        [ObservableProperty]
        private int _subscriptionId;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private decimal _price;

        [ObservableProperty]
        private DateTime _startDate = DateTime.Today;

        [ObservableProperty]
        private DateTime _endDate = DateTime.Today.AddMonths(1);

        [ObservableProperty]
        private string _avatarPath;

        [ObservableProperty]
        private string pageTitle = "Dodaj Subskrypcję";

        public SubscriptionEditViewModel(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [RelayCommand]
        private void Appearing()
        {
            LoadSubscriptionCommand.Execute(null);
        }

        [RelayCommand]
        private async Task LoadSubscriptionAsync()
        {
            if (SubscriptionId != 0)
            {
                _subscription = await _subscriptionService.GetSubscriptionAsync(SubscriptionId);
                if (_subscription != null)
                {
                    MapSubscriptionToProperties(_subscription);
                    PageTitle = "Edytuj Subskrypcję";
                    return;
                }
            }

            _subscription = new Subscription("Subskrypcja", 0, DateTime.Today, DateTime.Today.AddMonths(1));
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            try
            {
                MapPropertiesToSubscription(_subscription);

                if (SubscriptionId == 0)
                {
                    await _subscriptionService.AddSubscriptionAsync(_subscription);
                }
                else
                {
                    await _subscriptionService.UpdateSubscriptionAsync(_subscription);
                }

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Błąd", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task PickAvatarAsync()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Wybierz avatar",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    var newFilePath = Path.Combine(FileSystem.AppDataDirectory, result.FileName);

                    using (var stream = await result.OpenReadAsync())
                    using (var newStream = File.OpenWrite(newFilePath))
                    {
                        await stream.CopyToAsync(newStream);
                    }

                    AvatarPath = newFilePath;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Błąd", ex.Message, "OK");
            }
        }

        private void MapSubscriptionToProperties(Subscription subscription)
        {
            Name = subscription.Name;
            Price = subscription.Price;
            StartDate = subscription.StartDate;
            EndDate = subscription.EndDate;
            AvatarPath = subscription.AvatarPath;
        }

        private void MapPropertiesToSubscription(Subscription subscription)
        {
            subscription.Name = Name;
            subscription.Price = Price;
            subscription.StartDate = StartDate;
            subscription.EndDate = EndDate;
            subscription.AvatarPath = AvatarPath;
        }
    }
}