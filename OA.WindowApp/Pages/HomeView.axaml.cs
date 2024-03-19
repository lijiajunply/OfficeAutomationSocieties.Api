using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using FluentAvalonia.UI.Controls;
using Oa.NetLib.Data;
using Oa.NetLib.Models;
using OA.WindowApp.Dialogs;
using OA.WindowApp.Models;
using OA.WindowApp.Views;

namespace OA.WindowApp.Pages;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
    }

    protected override async void OnInitialized()
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        var proj = new Project(view.Jwt);
        NameBlock.Text = view.User.Name;
        TaskItems.ItemsSource = view.User.TaskNotes;
        var projects = new List<ProjectModel>();
        foreach (var projectIdentity in view.User.Projects)
        {
            projects.Add(await proj.GetProject(projectIdentity.ProjectId));
        }

        ProjectItems.ItemsSource = projects;
        OrgItems.ItemsSource = view.User.Organizes;
        TaskItemBlock.Text = view.User.TaskNotes.Count == 0 ? "当前没有任务，可以去放松一下了" : "当前任务";
        ProjectItemBlock.Text = view.User.Projects.Count == 0 ? "当前没有项目，点击添加或创建" : "您的项目";
        OrgItemBlock.Text = view.User.Organizes.Count == 0 ? "当前没有组织，点击添加或创建" : "您的组织";
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
                //view.User.Projects.Add(p);
            }
            else
            {
                var p = await proj.JoinProject(result.context);
                if (string.IsNullOrEmpty(p.Id)) return;
                // view.User.Projects.Add(p);
            }
        };
        await td.ShowAsync();
    }
}