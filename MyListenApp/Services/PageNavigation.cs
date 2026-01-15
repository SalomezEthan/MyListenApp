using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

namespace MyListenApp.Services;

public sealed class PageNavigation
{
    Frame? currentFrame;
    NavigationItem? CurrentPage;
    Stack<NavigationItem> NavigatedPages = [];

    public void SetFrame(Frame frame)
    {
        currentFrame = frame;
    }

    public void NavigateTo(Type pageType, object? parameter = null)
    {
        if (currentFrame is null)
        {
            throw new InvalidOperationException("La Frame n'est pas définie.");
        }

        if (CurrentPage is not null)
        {
            NavigatedPages.Push(CurrentPage);
        }

        currentFrame.Navigate(pageType, parameter);
        CurrentPage = new NavigationItem(pageType, parameter);
    }

    public void NavigateBack()
    {
        if (currentFrame is null)
        {
            throw new InvalidOperationException("La Frame n'est pas définie.");
        }
        if (NavigatedPages.Count == 0)
        {
            return;
        }

        var previousPage = NavigatedPages.Pop();
        currentFrame.Navigate(previousPage.PageType, previousPage.Parameter);
        CurrentPage = previousPage;
    }
}

internal sealed record NavigationItem(Type PageType, object? Parameter);