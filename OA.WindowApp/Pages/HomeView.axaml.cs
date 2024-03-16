using Avalonia.Controls;
using OA.WindowApp.Models;
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
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        NameBlock.Text = view.User.Name;
        TaskItems.ItemsSource = view.User.TaskNotes;
        ProjectItems.ItemsSource = view.User.Projects;
        OrgItems.ItemsSource = view.User.Organizes;
        TaskItemBlock.Text = view.User.TaskNotes.Count == 0 ? "当前没有任务，可以去放松一下了" : "当前任务";
        ProjectItemBlock.Text = view.User.Projects.Count == 0 ? "当前没有项目，点击添加或创建" : "您的项目";
        OrgItemBlock.Text = view.User.Organizes.Count == 0 ? "当前没有组织，点击添加或创建" : "您的组织";
    }
}