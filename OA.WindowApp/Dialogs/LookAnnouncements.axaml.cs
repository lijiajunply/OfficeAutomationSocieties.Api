using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using FluentAvalonia.UI.Controls;
using Oa.NetLib.Data;
using Oa.NetLib.Models;

namespace OA.WindowApp.Dialogs;

public partial class LookAnnouncements : UserControl
{
    private readonly string _jwt;
    private readonly string _id;

    public LookAnnouncements(IEnumerable<AnnouncementModel> array, string jwt, string id)
    {
        InitializeComponent();
        Items.ItemsSource = array;
        _jwt = jwt;
        _id = id;
    }

    private async void RemoveClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Control control) return;
        if (control.DataContext is not AnnouncementModel model) return;
        using var org = new Organize(_jwt);
        var result = await org.RemoveAnnouncement(model, _id);
        if (!result) return;
        var array = Items.ItemsSource as IEnumerable<AnnouncementModel>;
        var list = array?.ToList();
        list?.Remove(model);
        Items.ItemsSource = list;
    }
    
    private async void AddAnnouncementClick(object? sender, RoutedEventArgs e)
        {
            var td = new TaskDialog
            {
                Title = "添加资源",
                Content = new AddAnnouncement(),
                FooterVisibility = TaskDialogFooterVisibility.Never,
                Buttons =
                {
                    TaskDialogButton.OKButton,
                    TaskDialogButton.CloseButton
                },
                XamlRoot = (Visual)VisualRoot!
            };
    
            td.Closing += async (dialog, args) =>
            {
                if ((TaskDialogStandardResult)args.Result != TaskDialogStandardResult.OK) return;
                if (dialog.Content is not AddAnnouncement announcement) return;
                var done = announcement.Done();
                if (string.IsNullOrEmpty(done.Title)) return;
                using var org = new Organize(_jwt); 
                await org.AddAnnouncement(done,_id);
            };
            await td.ShowAsync();
        }
}