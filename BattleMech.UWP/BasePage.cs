using System;

using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace BattleMech.UWP {
    public class BasePage : Page {
        public async void ShowDialog(string messageText, string title = "BattleMech") {
            var dialog = new MessageDialog(messageText, title);

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => {
                await dialog.ShowAsync();
            });
        }
    }
}