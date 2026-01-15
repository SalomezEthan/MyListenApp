using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MyListenApp.ViewModels.Player;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyListenApp.UserControls;

internal sealed partial class MusicPlayer : UserControl
{
    public MusicPlayer()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        "ViewModel",
        typeof(PlayerViewModel),
        typeof(MusicPlayer),
        new PropertyMetadata(null)
    );

    public PlayerViewModel ViewModel
    {
        get => (PlayerViewModel)GetValue(ViewModelProperty);
        set
        {
            SetValue(ViewModelProperty, value);
            DataContext = ViewModel;
        }
    }

    private void PreviousButton_Click(object sender, RoutedEventArgs e)
    {
        ViewModel?.Previous();
    }

    private void PlayButton_Click(object sender, RoutedEventArgs e)
    {
        ViewModel?.TogglePlayPause();
    }

    private void NextButton_Click(object sender, RoutedEventArgs e)
    {
        ViewModel?.Next();
    }

    private void ShuffleButton_Click(object sender, RoutedEventArgs e)
    {
    }

    private void LoopButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void LikeMusicButton_Click(object sender, RoutedEventArgs e)
    {

    }
}
