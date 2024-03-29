using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using DynamicData;
using Oa.NetLib.Data;
using Oa.NetLib.Models;

namespace OA.WindowApp.Dialogs;

public partial class LookAnnouncements : UserControl
{
    private readonly string _jwt;

    public LookAnnouncements(IEnumerable<AnnouncementModel> array, string jwt)
    {
        InitializeComponent();
        Items.ItemsSource = array;
        _jwt = jwt;
    }

    private async void RemoveClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Control control) return;
        if (control.DataContext is not AnnouncementModel model) return;
        using var org = new Organize(_jwt);
        await org.RemoveAnnouncement(model);
        var list = Items.ItemsSource as AnnouncementModel[];
        list?.Remove([model]);
    }
}