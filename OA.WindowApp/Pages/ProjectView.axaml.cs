﻿using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Interactivity;
using FluentAvalonia.UI.Controls;
using Oa.NetLib.Data;
using Oa.NetLib.Models;
using OA.WindowApp.Dialogs;
using OA.WindowApp.Models;
using OA.WindowApp.ViewModels.Pages;
using OA.WindowApp.Views;

namespace OA.WindowApp.Pages;

public partial class ProjectView : UserControl
{
    public ProjectView()
    {
        InitializeComponent();
    }

    protected override void OnInitialized()
    {
        if (DataContext is not ProjectViewModel model) return;
        if (model.Projects.Count == 0) return;
        model.Project = model.Projects[0];
    }

    public void ProjectViewFromId(string id)
    {
        if (DataContext is not ProjectViewModel model) return;
        var p = model.Projects.FirstOrDefault(x => x.Id == id);
        if (p != null) model.Project = p;
    }

    private async void AddTaskClick(object? sender, RoutedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        if (DataContext is not ProjectViewModel model) return;
        var td = new TaskDialog
        {
            Title = "添加任务",
            Content = new AddOrChangeGantt(),
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
            if (dialog.Content is not AddOrChangeGantt project) return;
            var result = project.Done();
            if (string.IsNullOrEmpty(result.ToDo)) return;
            using var proj = new Project(view.Jwt);
            result = await proj.AddGantt(model.Project.Id, result);
            model.Project.GanttList.Add(result);
        };
        await td.ShowAsync();
    }
    
    private async void TaskChangeClick(object? sender, RoutedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        if (sender is not Control control) return;
        if (control.DataContext is not GanttModel gantt) return;
        if(gantt.UserId != view.User.UserId)return;
        var td = new TaskDialog
        {
            Title = "更改任务",
            Content = new AddOrChangeGantt(gantt),
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
            if (dialog.Content is not AddOrChangeGantt project) return;
            var result = project.Done();
            gantt.Update(result);
            using var proj = new Project(view.Jwt);
            if (await proj.UpdateGantt(gantt))
                view.NotificationShow("更改任务", "更改成功");
            else
                view.NotificationShow("更改任务", "更改失败", NotificationType.Error);
        };
        await td.ShowAsync();
    }

    private async void RemoveGanttClick(object? sender, RoutedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        if (sender is not Control control) return;
        if (control.DataContext is not GanttModel gantt) return;
        if(gantt.UserId != view.User.UserId)return;
        using var proj = new Project(view.Jwt);
        if (await proj.RemoveGantt(gantt.Id))
            view.NotificationShow("删除任务", "删除成功");
        else
            view.NotificationShow("删除任务", "删除失败", NotificationType.Error);
    }
}