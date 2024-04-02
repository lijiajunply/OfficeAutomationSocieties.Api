using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace OA.WindowApp.Pages;

public partial class HelpView : UserControl
{
    public HelpView()
    {
        InitializeComponent();
    }
    
    private void LaunchRepoLinkItemClick(object? sender, RoutedEventArgs e)
    {
        var uri = new Uri("https://gitee.com/luckyfishisdashen/OfficeAutomationSocieties.Api/issues");
        Process.Start(new ProcessStartInfo(uri.ToString())
            { UseShellExecute = true, Verb = "open" });
    }
}