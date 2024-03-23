using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using FluentAvalonia.UI.Controls;
using Oa.NetLib.Data;
using Oa.NetLib.Models;
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
            Content = new TextBox(),
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
            if (dialog.Content is not TextBox box) return;
            if (string.IsNullOrEmpty(box.Text)) return;
            using var org = new Organize(string.IsNullOrEmpty(Jwt) ? view.Jwt : Jwt);
            if (DataContext is not OrganizeViewModel model) return;
            var proj = await org.CreateOrgProject(new ProjectModel() { Name = box.Text });
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
    }
}