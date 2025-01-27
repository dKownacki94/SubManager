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

        public SubscriptionEditViewModel(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        partial void OnSubscriptionIdChanged(int value)
        {
            LoadSubscriptionCommand.Execute(null);
        }

        [ObservableProperty]
        private int _subscriptionId;

        private Subscription _subscription;

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

        [RelayCommand]
        private async Task LoadSubscription()
        {
            if (SubscriptionId != 0)
            {
                _subscription = await _subscriptionService.GetSubscriptionAsync(SubscriptionId);
                if (_subscription != null)
                {
                    Name = _subscription.Name;
                    Price = _subscription.Price;
                    StartDate = _subscription.StartDate;
                    EndDate = _subscription.EndDate;
                    AvatarPath = _subscription.AvatarPath;

                    PageTitle = "Edytuj Subskrypcję";
                }
            }
            else
            {
                _subscription = new Subscription("Subskrypcja", 0, DateTime.Today, DateTime.Today.AddMonths(1));
            }
        }

        [RelayCommand]
        private async Task Save()
        {
            try
            {
                _subscription.Name = Name;
                _subscription.Price = Price;
                _subscription.StartDate = StartDate;
                _subscription.EndDate = EndDate;
                _subscription.AvatarPath = AvatarPath;

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
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task PickAvatar()
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
    }
}