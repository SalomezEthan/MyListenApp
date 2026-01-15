using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MyListenApp.Pages;
using MyListenApp.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyListenApp.UserControls.SongList
{
    public sealed partial class SongListViewItem : UserControl
    {
        public SongListViewItem()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel",
            typeof(SongViewModel),
            typeof(SongListViewItem),
            new PropertyMetadata(null)
        );

        public SongViewModel ViewModel
        {
            get => (SongViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                DataContext = value;
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RenameButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is SongViewModel svm)
            {
                App.NavigationService.NavigateTo(typeof(SongPage), svm);
            }
        }
    }
}
