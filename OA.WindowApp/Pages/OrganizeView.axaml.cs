using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Interactivity;
using FluentAvalonia.UI.Controls;
using Oa.NetLib.Data;
using Oa.NetLib.Models;
using OA.WindowApp.Converters;
using OA.WindowApp.Dialogs;
using OA.WindowApp.Models;
using OA.WindowApp.ViewModels.Pages;
using OA.WindowApp.Views;

namespace OA.WindowApp.Pages;

public partial class OrganizeView : UserControl
{
    private string _id = "";

    public OrganizeView()
    {
        InitializeComponent();
    }

    #region Init

    protected override void OnInitialized()
    {
        if (DataContext is not OrganizeViewModel model) return;
        if (model.Organizes.Count == 0)
        {
            Content = new EmptyControl("当前无组织");
            return;
        }

        model.Organize = model.Organizes[0];
    }

    private async void OrgSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        if (DataContext is not OrganizeViewModel model) return;
        _id = model.Organize.Id;
        using var org = new Organize(view.Jwt);
        ResourceItems.ItemsSource = (await org.GetResources(_id)).ToList();
        var a = await org.LookAnnouncement(_id);
        if (string.IsNullOrEmpty(a.Id)) return;
        view.NotificationShow("公告", a.Context);
    }

    #endregion

    #region Org

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
            using var org = new Organize(view.Jwt);

            var proj = await org.CreateOrgProject(project, _id);
            if (string.IsNullOrEmpty(proj.Id)) return;
            model.Organize.Projects.Add(proj);
        };
        await td.ShowAsync();
    }

    #endregion

    #region Button

    private async void ShowUserClick(object? sender, RoutedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        using var organize = new Organize(view.Jwt);
        var td = new TaskDialog
        {
            Title = "查看成员",
            Content = new ShowUsers(await organize.GetOrganizeMember(_id)),
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
            Content = new LookAnnouncements(await org.LookAnnouncements(_id), view.Jwt, _id),
            FooterVisibility = TaskDialogFooterVisibility.Never,
            Buttons =
            {
                TaskDialogButton.OKButton
            },
            XamlRoot = (Visual)VisualRoot!
        };
        await td.ShowAsync();
    }

    #endregion

    #region Resource

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
            using var org = new Organize(view.Jwt);
            var resource = await org.AddResource(done, _id);
            if (string.IsNullOrEmpty(resource.Id)) return;
            var list = ResourceItems.ItemsSource as List<ResourceModel>;
            list?.Add(resource);
        };
        await td.ShowAsync();
    }

    private async void RemoveResourceClick(object? sender, RoutedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        if (sender is not Control control) return;
        if (control.DataContext is not ResourceModel model) return;
        using var org = new Organize(view.Jwt);
        if (!await org.DeleteResource(model.Id, _id)) return;
        view.NotificationShow("删除资源", "删除成功");
        var list = ResourceItems.ItemsSource as List<ResourceModel>;
        list?.Remove(model);
    }

    private async void UpdateResourceClick(object? sender, RoutedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        if (sender is not Control control) return;
        if (control.DataContext is not ResourceModel model) return;
        var td = new TaskDialog
        {
            Title = "更改资源",
            Content = new AddResource(model),
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
            if (dialog.Content is not AddResource announcement) return;
            var done = announcement.Done();
            if (string.IsNullOrEmpty(done.Name)) return;
            using var org = new Organize(view.Jwt);
            done.Id = model.Id;
            done.CreateTime = model.CreateTime;
            var resource = await org.UpdateResource(done, _id);
            if (resource)
            {
                view.NotificationShow("更改资源", "更改成功");
                model.Name = done.Name;
                model.Introduce = done.Introduce;
            }
            else
            {
                view.NotificationShow("更改资源", "更改失败", NotificationType.Error);
            }
        };
        await td.ShowAsync();
    }

    private async void UseClick(object? sender, RoutedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        if (sender is not Control control) return;
        if (control.DataContext is not ResourceModel model) return;
        var td = new TaskDialog
        {
            Title = "使用资源",
            Content = new UseResource(model),
            FooterVisibility = TaskDialogFooterVisibility.Never,
            Buttons =
            {
                new TaskDialogButton("借出", "借出"),
                new TaskDialogButton("归还", "归还"),
                new TaskDialogButton("取消", "")
            },
            XamlRoot = (Visual)VisualRoot!
        };

        td.Closing += async (dialog, args) =>
        {
            if ((string)args.Result == "借出")
            {
                if (dialog.Content is not UseResource useResource) return;
                var done = useResource.Done();
                using var org = new Organize(view.Jwt);
                model.StartTime = done.StartTime;
                model.EndTime = done.EndTime;
                var resource = await org.UpdateResource(model, _id);
                if (resource)
                {
                    view.NotificationShow("更改资源", "更改成功");
                    model.Name = done.Name;
                    model.Introduce = done.Introduce;
                }
                else
                {
                    view.NotificationShow("更改资源", "更改失败", NotificationType.Error);
                }

                return;
            }

            if ((string)args.Result == "归还")
            {
                model.StartTime = model.EndTime = "";
                using var org = new Organize(view.Jwt);
                var resource = await org.UpdateResource(model, _id);
                if (resource)
                {
                    view.NotificationShow("更改资源", "更改成功");
                }
                else
                {
                    view.NotificationShow("更改资源", "更改失败", NotificationType.Error);
                }
            }
        };
        await td.ShowAsync();
    }

    #endregion
}