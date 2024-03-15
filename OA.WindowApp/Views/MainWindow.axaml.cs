using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Windowing;
using OA.WindowApp.ViewModels.Pages;

namespace OA.WindowApp.Views;

public partial class MainWindow : AppWindow
{
    private Dictionary<string, PageModelBase> Stack { get; }
    private WindowNotificationManager? _manager;

    public MainWindow()
    {
        Stack = new Dictionary<string, PageModelBase>()
        {
            { "Home", new HomeViewModel() },
            { "Setting", new SettingViewModel() },
            { "Help", new HelpViewModel() }
        };

        InitializeComponent();
        
        SplashScreen = new MainAppSplashScreen("OA", null);
        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;

        FrameView.IsNavigationStackEnabled = true;
        FrameView.NavigationPageFactory = new NavigationFactory();
    }

    private void BackClick(object? sender, RoutedEventArgs e)
    {
    }

    private void ItemInvoked(object? sender, NavigationViewItemInvokedEventArgs e)
    {
        if (e.InvokedItemContainer.Tag is not string s) return;
        if (string.IsNullOrEmpty(s)) return;
        Navigate(s);
    }

    private void Navigate(string page)
    {
        if (string.IsNullOrEmpty(page)) return;
        Navigate(Stack[page]);
    }

    public void Navigate(object context)
    {
        FrameView.NavigateFromObject(context);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _manager = new WindowNotificationManager(this) { MaxItems = 3, Position = NotificationPosition.BottomRight };
    }
}

public class NavigationFactory : INavigationPageFactory
{
    public Control GetPage(Type srcType)
    {
        throw new NotImplementedException();
    }

    public Control GetPageFromObject(object target)
    {
        throw new NotImplementedException();
    }
}