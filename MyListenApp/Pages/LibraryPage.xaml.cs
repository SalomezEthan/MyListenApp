using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Windows.Storage.Pickers;
using MyListenApp.ViewModels.Library;
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
    public sealed partial class LibraryPage : Page
    {
        LibraryViewModel? viewModel;

        public LibraryPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is LibraryViewModel libVm)
            {
                viewModel = libVm;
                this.DataContext = viewModel;

                this.viewModel.CollectSongList();
            }
        }

        private async void ImportButton_Click(object sender, RoutedEventArgs e)
        {
           if (sender is Button button)
           {
                button.IsEnabled = false;
                var picker = new FolderPicker(button.XamlRoot.ContentIslandEnvironment.AppWindowId)
                {
                    SuggestedStartLocation = PickerLocationId.MusicLibrary,
                    ViewMode = PickerViewMode.Thumbnail,
                    CommitButtonText = "Take this song folder"
                };

                var result = await picker.PickSingleFolderAsync();
                if (result is not null)
                {
                    viewModel?.ImportSongList(result.Path);
                }

                button.IsEnabled = true;
           }
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is SongListViewModel songListItem)
            {
                App.NavigationService.NavigateTo(typeof(SongListPage), songListItem);
            }
        }

        private void ImportButton_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
