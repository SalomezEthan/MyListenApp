using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using MyListenApp.Pages;
using MyListenApp.ViewModels.Library;
using MyListenApp.ViewModels.Player;

namespace MyListenApp
{
    internal sealed partial class MainWindow : Window
    {
        const int MIN_WIDTH = 850;
        const int MIN_HEIGHT = 480;

        const int DEFAULT_WIDTH = 1200;
        const int DEFAULT_HEIGHT = 640;

        readonly PlayerViewModel playerViewModel;

        public MainWindow(LibraryViewModel libViewModel, PlayerViewModel playerVm)
        {
            InitializeComponent();
            this.playerViewModel = playerVm;
            ExtendsContentIntoTitleBar = true;
            SetCustomPresenter();
            AnimatedBackground.MediaPlayer.IsLoopingEnabled = true;
            App.BackgroundService.SetPlayer(AnimatedBackground.MediaPlayer);
            App.NavigationService.SetFrame(PageContent);
            App.NavigationService.NavigateTo(typeof(LibraryPage), libViewModel);
        }

        void SetCustomPresenter()
        {
            var presenter = OverlappedPresenter.Create();
            presenter.PreferredMinimumHeight = MIN_HEIGHT;
            presenter.PreferredMinimumWidth = MIN_WIDTH;
            AppWindow.SetPresenter(presenter);
            AppWindow.Resize(new Windows.Graphics.SizeInt32(DEFAULT_WIDTH, DEFAULT_HEIGHT));
        }
        
        private void CustomTitleBar_PaneToggleRequested(Microsoft.UI.Xaml.Controls.TitleBar sender, object args)
        {
            NavView.IsPaneOpen = !NavView.IsPaneOpen;
        }

        private void NavView_BackRequested(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs args)
        {
            App.NavigationService.NavigateBack();
        }
    }
}
