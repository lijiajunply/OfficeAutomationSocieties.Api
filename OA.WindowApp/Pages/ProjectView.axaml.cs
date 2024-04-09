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
        if (model.Projects.Count == 0)
        {
            Viewer.IsVisible = false;
            return;
        }
        model.Project = model.Projects[0];
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
        if (gantt.UserId != view.User.UserId) return;
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
        if (gantt.UserId != view.User.UserId) return;
        using var proj = new Project(view.Jwt);
        if (await proj.RemoveGantt(gantt.Id))
            view.NotificationShow("删除任务", "删除成功");
        else
            view.NotificationShow("删除任务", "删除失败", NotificationType.Error);
    }

    private async void ShowUserClick(object? sender, RoutedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        if (DataContext is not ProjectViewModel model) return;
        using var proj = new Project(view.Jwt);
        var td = new TaskDialog
        {
            Title = "查看成员",
            Content = new ShowUsers(await proj.GetProjectMember(model.Project.Id)),
            FooterVisibility = TaskDialogFooterVisibility.Never,
            Buttons =
            {
                TaskDialogButton.OKButton
            },
            XamlRoot = (Visual)VisualRoot!
        };
        await td.ShowAsync();
    }

    private async void UpdateProjectClick(object? sender, RoutedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        if (sender is not Control control) return;
        if (control.DataContext is not ProjectModel model) return;
        var td = new TaskDialog
        {
            Title = "更改项目",
            Content = new UpdateProject(model),
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
            if (dialog.Content is not UpdateProject project) return;
            var result = project.Done();
            using var proj = new Project(view.Jwt);
            if (await proj.UpdateProject(result))
                view.NotificationShow("更改项目", "更改成功");
            else
                view.NotificationShow("更改项目", "更改失败", NotificationType.Error);
        };
        await td.ShowAsync();
    }

    private async void TaskDoneClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Control control) return;
        if (control.DataContext is not GanttModel gantt) return;
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        gantt.IsDone = !gantt.IsDone;
        using var proj = new Project(view.Jwt);
        if (await proj.UpdateGantt(gantt))
            view.NotificationShow("完成任务", "更改成功");
        else
            view.NotificationShow("完成任务", "更改失败", NotificationType.Error);
    }
}