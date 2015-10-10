using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using BattleMech.DataLayer.PCL.Views.Assets;
using BattleMech.PCL.Objects.Game;

namespace BattleMech.UWP {
    sealed partial class App {
        public static GamePage Game;

        public static string Token;

        public static List<ActiveAssetsVIEW> Assets;

        public static Level CurrentLevel;

        public App() {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e) {
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;

            var rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null) {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;
                
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null) {
#if GAME_ONLY
                rootFrame.Navigate(typeof(GamePage), e.Arguments);
#else
                rootFrame.Navigate(typeof(MainMenuPage), e.Arguments);
#endif
            }

            Window.Current.Activate();
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e) {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e) {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}