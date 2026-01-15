using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MyListenApp.ViewModels.Song;
using MyListenApp.ViewModels.SongList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyListenApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SongListPage : Page
    {
        SongListViewModel? viewModel;

        public SongListPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is SongListViewModel songListVM)
            {
                viewModel = songListVM;
                this.DataContext = viewModel;

                viewModel.CollectSongs();
            }
        }

        private void PlayAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel?.PlaySongList();
        }

        private void ShuffleAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void RenameFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ImportFlyoutItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is SongViewModel songVM)
            {
                viewModel?.PlaySongList(songVM.Id);
            }
        }

        private void SongListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
