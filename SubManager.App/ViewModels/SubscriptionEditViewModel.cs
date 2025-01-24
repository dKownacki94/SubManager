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
            Subscription = new Subscription("Subskrypcja", 0, DateTime.Today, DateTime.Today.AddMonths(1));
        }

        [ObservableProperty]
        private int subscriptionId;

        [ObservableProperty]
        private Subscription subscription;

        [ObservableProperty]
        private string pageTitle = "Dodaj Subskrypcję";

        [RelayCommand]
        private async Task LoadSubscription()
        {
            if (SubscriptionId != 0)
            {
                Subscription = await _subscriptionService.GetSubscriptionAsync(SubscriptionId);
                PageTitle = "Edytuj Subskrypcję";
            }
        }

        [RelayCommand]
        private async Task Save()
        {
            try
            {
                if (SubscriptionId == 0)
                {
                    await _subscriptionService.AddSubscriptionAsync(Subscription);
                }
                else
                {
                    await _subscriptionService.UpdateSubscriptionAsync(Subscription);
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
                    Subscription.AvatarPath = newFilePath;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Błąd", ex.Message, "OK");
            }
        }

        public async void OnNavigatedTo()
        {
            await LoadSubscriptionCommand.ExecuteAsync(null);
        }
    }
}