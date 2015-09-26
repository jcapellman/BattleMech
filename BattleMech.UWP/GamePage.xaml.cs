using Windows.UI.Xaml;

namespace BattleMech.UWP {
    public sealed partial class GamePage {
        readonly MainGame _game;

        public GamePage() {
            this.InitializeComponent();
            
            var launchArguments = string.Empty;
            _game = MonoGame.Framework.XamlGame<MainGame>.Create(launchArguments, Window.Current.CoreWindow, swapChainPanel);
        }
    }
}