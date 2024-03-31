using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
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
        var result = await org.RemoveAnnouncement(model);
        if (!result) return;
        var array = Items.ItemsSource as IEnumerable<AnnouncementModel>;
        var list = array?.ToList();
        list?.Remove(model);
        Items.ItemsSource = list;
    }
}