using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using MyListen.Common.Entities;
using MyListen.Common.Services;
using MyListen.Library;
using MyListen.Song;
using MyListen.SongList;
using MyListenApp.Services;
using MyListenApp.ViewModels.Library;
using MyListenApp.ViewModels.Player;
using MyListenApp.ViewModels.Song;
using MyListenApp.ViewModels.SongList;
using MyListenInfra.Win;
using System;
using System.IO;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyListenApp
{
    public partial class App : Application
    {
        private Window? _window;

        public static PageNavigation NavigationService { get; } = new PageNavigation();
        public static BackgroundService BackgroundService { get; } = new BackgroundService();

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            RequestedTheme = ApplicationTheme.Dark;
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            string appPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyListenApp");
            Directory.CreateDirectory(appPath);

            var playbackQueue = new PlaybackQueue();

            IExternServiceCompositionRoot externServices = new WinInfraCompositionRoot(appPath);
            var song = new SongComposantFactory(externServices.SongRepo);
            var songList = new SongListComposantFactory(externServices.SongListRepo, externServices.SongRepo, playbackQueue, externServices.Player);
            var library = new LibraryComposantFactory(externServices.SongListRepo, externServices.SongRepo, externServices.SongListImporter, externServices.SongImporter);
            var player = new MyListen.Player.PlayerServiceFactory(playbackQueue, externServices.Player, externServices.SongRepo);

            var songVMFactory = new SongViewModelFactory(song);
            var songVMMap = new SongViewModelMap(songVMFactory);
            var songListVM = new SongListViewModelFactory(songList, songVMMap);
            var libListVM = new LibraryViewModelFactory(library, songListVM);
            var playerVMFactory = new PlayerViewModelFactory(player, songVMMap);

            _window = new MainWindow(libListVM.CreateLibraryViewModel(), playerVMFactory.CreatePlayerViewModel());
            _window.Activate();
        }
    }
}
