using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Input;
using Avalonia.Interactivity;
using DynamicData;
using FluentAvalonia.UI.Controls;
using Oa.NetLib.Data;
using Oa.NetLib.Models;
using OA.WindowApp.Dialogs;
using OA.WindowApp.Models;
using OA.WindowApp.ViewModels.Pages;
using OA.WindowApp.Views;

namespace OA.WindowApp.Pages;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
    }

    protected override void OnInitialized()
    {
        if (DataContext is not HomeViewModel model) return;
        TaskItemBlock.Text = model.TaskNotes.Count == 0 ? "当前没有任务，可以去放松一下了" : "当前任务";
        ProjectItemBlock.Text = model.Projects.Count == 0 ? "当前没有项目，点击添加或创建" : "您的项目";
        OrgItemBlock.Text = model.Organizes.Count == 0 ? "当前没有组织，点击添加或创建" : "您的组织";
    }

    private async void JoinOrCreateProjectClick(object? sender, RoutedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        var td = new TaskDialog
        {
            Title = "添加或创建项目",
            Content = new JoinOrCreateProject(),
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
            if (dialog.Content is not JoinOrCreateProject project) return;
            var result = project.Done();
            using var proj = new Project(view.Jwt);
            if (result.isCreate)
            {
                var p = await proj.CreateProject(new ProjectModel() { Name = result.context });
                if (string.IsNullOrEmpty(p.Id)) return;
                view.Add(p);
                view.NotificationShow("创建项目", "创建成功");
            }
            else
            {
                var p = await proj.JoinProject(result.context);
                if (string.IsNullOrEmpty(p.Id)) return;
                view.Add(p);
                view.NotificationShow("加入项目", "加入成功");
            }
        };
        await td.ShowAsync();
    }

    private void ProjectTapped(object? sender, TappedEventArgs e)
    {
        if (sender is not Control control) return;
        if (control.DataContext is not ProjectModel project) return;
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        if (DataContext is not HomeViewModel model) return;
        var projModel = new ProjectViewModel();
        projModel.Projects.Add(model.Projects);
        var projView = new ProjectView() { DataContext = projModel };
        projView.ProjectViewFromId(project.Id);
        view.Navigate(projView);
    }

    private async void TaskChangeClick(object? sender, RoutedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        if (sender is not Control control) return;
        if (control.DataContext is not GanttModel gantt) return;
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
        using var proj = new Project(view.Jwt);
        if (await proj.RemoveGantt(gantt.Id))
            view.NotificationShow("删除任务", "删除成功");
        else
            view.NotificationShow("删除任务", "删除失败", NotificationType.Error);
    }
}