using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using FluentAvalonia.UI.Controls;
using Oa.NetLib.Data;
using Oa.NetLib.Models;
using OA.WindowApp.Dialogs;
using OA.WindowApp.Models;
using OA.WindowApp.ViewModels.Pages;
using OA.WindowApp.Views;

namespace OA.WindowApp.Pages;

public partial class OrganizeView : UserControl
{
    private string Jwt { get; set; } = "";

    public OrganizeView()
    {
        InitializeComponent();
    }

    protected override void OnInitialized()
    {
        if (DataContext is not OrganizeViewModel model) return;
        if (model.Organizes.Count == 0)
        {
            Viewer.IsVisible = false;
            return;
        }

        model.Organize = model.Organizes[0];
    }

    public void OrganizeViewFromId(string id)
    {
        if (DataContext is not OrganizeViewModel model) return;
        var p = model.Organizes.FirstOrDefault(x => x.Id == id);
        if (p != null) model.Organize = p;
    }

    private async void AddProjectClick(object? sender, RoutedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        var td = new TaskDialog
        {
            Title = "添加项目",
            Content = new OrganizeAddProject(),
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
            if (dialog.Content is not OrganizeAddProject organizeAddProject) return;
            var project = organizeAddProject.Done();
            if (string.IsNullOrEmpty(project.Name)) return;
            if (DataContext is not OrganizeViewModel model) return;
            using var org = new Organize(Jwt);

            var proj = await org.CreateOrgProject(project);
            if (string.IsNullOrEmpty(proj.Id)) return;
            model.Organize.Projects.Add(proj);
        };
        await td.ShowAsync();
    }

    private async void OrgSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        if (DataContext is not OrganizeViewModel model) return;
        using var org = new Organize(string.IsNullOrEmpty(Jwt) ? view.Jwt : Jwt);
        Jwt = await org.LoginOrganize(model.Organize.Id);
        org.Jwt = Jwt;
        ResourceItems.ItemsSource = (await org.GetResources()).ToList();
        var a = await org.LookAnnouncement();
        if (string.IsNullOrEmpty(a.Id)) return;
        view.NotificationShow("公告", a.Context);
    }

    private async void ShowUserClick(object? sender, RoutedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        if (DataContext is not OrganizeViewModel model) return;
        using var organize = new Organize(view.Jwt);
        var td = new TaskDialog
        {
            Title = "查看成员",
            Content = new ShowUsers(await organize.GetOrganizeMember(model.Organize.Id)),
            FooterVisibility = TaskDialogFooterVisibility.Never,
            Buttons =
            {
                TaskDialogButton.OKButton
            },
            XamlRoot = (Visual)VisualRoot!
        };
        await td.ShowAsync();
    }

    private async void LookAnnouncementsClick(object? sender, RoutedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        using var org = new Organize(view.Jwt);
        var td = new TaskDialog
        {
            Title = "查看公告",
            Content = new LookAnnouncements(await org.LookAnnouncements(), Jwt),
            FooterVisibility = TaskDialogFooterVisibility.Never,
            Buttons =
            {
                TaskDialogButton.OKButton
            },
            XamlRoot = (Visual)VisualRoot!
        };
        await td.ShowAsync();
    }

    private async void AddResourceClick(object? sender, RoutedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        var td = new TaskDialog
        {
            Title = "添加资源",
            Content = new AddResource(),
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
            if (dialog.Content is not AddResource addResource) return;
            var done = addResource.Done();
            if (string.IsNullOrEmpty(done.Name)) return;
            using var org = new Organize(Jwt);
            var resource = await org.AddResource(done);
            if (string.IsNullOrEmpty(resource.Id)) return;
            var list = ResourceItems.ItemsSource as List<ResourceModel>;
            list?.Add(resource);
        };
        await td.ShowAsync();
    }
}