using Microsoft.UI.Xaml.Controls;
using MyArchitecture.PresenterLayer;
using MyListenApp.Pages;
using MyListenApp.ViewModels.Library;
using MyListenApp.ViewModels.Song;
using MyListenApp.ViewModels.SongList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyListenApp.Services
{
    internal sealed class NavigationService
    {
        Frame? frame;
        NavigationItem? currentPage;

        readonly Dictionary<Type, Type> pages = new()
        {
            [typeof(LibraryViewModel)] = typeof(LibraryPage),
            [typeof(SongListViewModel)] = typeof(SongListPage),
            [typeof(SongViewModel)] = typeof(SongPage),
        };

        readonly Stack<NavigationItem> navigationHistory = [];

        public void SetFrame(Frame frame)
        {
            this.frame = frame;
        }

        public void NavigateTo(BaseViewModel viewModel)
        {
            if (!pages.TryGetValue(viewModel.GetType(), out var pageType))
            {
                throw new ArgumentException("La page n'est pas rensignée dans le dictionnaire");
            }

            var newNavItem = new NavigationItem(pageType, viewModel);
            if (currentPage == newNavItem)
            {
                return;
            }

            if (currentPage is not null)
            {
                navigationHistory.Push(currentPage);
            }

            frame?.Navigate(pageType, viewModel);
            currentPage = newNavItem;
        }

        public void NavigateBack()
        {
            if (!navigationHistory.TryPop(out var navItem))
            {
                return;
            }

            frame?.Navigate(navItem.PageType, navItem.ViewModel);
            currentPage = navItem;
        }
    }

    internal sealed record NavigationItem(Type PageType, BaseViewModel ViewModel);
}
