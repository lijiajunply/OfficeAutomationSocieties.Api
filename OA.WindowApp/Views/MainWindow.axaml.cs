using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls.Notifications;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Threading;
using DynamicData;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Windowing;
using Oa.NetLib.Data;
using Oa.NetLib.Models;
using OA.WindowApp.Models;
using OA.WindowApp.Pages;
using OA.WindowApp.ViewModels.Pages;

namespace OA.WindowApp.Views;

public partial class MainWindow : AppWindow
{
    private Dictionary<string, PageModelBase> Stack { get; }
    private WindowNotificationManager? _manager;
    private LoginModel Setting { get; set; }
    public string Jwt { get; private set; } = "";
    public UserModel User { get; private set; } = new();

    public MainWindow()
    {
        Stack = new Dictionary<string, PageModelBase>()
        {
            { "Home", new HomeViewModel() },
            { "Project", new ProjectViewModel() },
            { "Organize", new OrganizeViewModel() },
            { "Setting", new SettingViewModel() },
            { "Help", new HelpViewModel() }
        };

        Setting = SettingStatic.Read();
        InitializeComponent();

        SplashScreen = new MainAppSplashScreen("OA", InitApp);
        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;

        FrameView.IsNavigationStackEnabled = true;
        FrameView.NavigationPageFactory = new NavigationFactory();
    }

    private void BackClick(object? sender, RoutedEventArgs e)
    {
        FrameView.GoBack();
    }

    private void ItemInvoked(object? sender, NavigationViewItemInvokedEventArgs e)
    {
        if (e.InvokedItemContainer.Tag is not string s) return;
        if (string.IsNullOrEmpty(s)) return;
        if (Setting.IsNull() && s is not ("Setting" or "Help"))
        {
            if(FrameView.Content is LoginView)return;
            Navigate(new LoginView());
            return;
        }
        Navigate(s);
    }

    public void Navigate(string page)
    {
        if (string.IsNullOrEmpty(page)) return;
        Navigate(Stack[page]);
    }

    private void Navigate(object context)
    {
        FrameView.NavigateFromObject(context);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _manager = new WindowNotificationManager(this) { MaxItems = 3, Position = NotificationPosition.BottomRight };
    }

    public void NotificationShow(string title, string message, NotificationType type = NotificationType.Information)
    {
        _manager?.Show(new Notification(title, message, type));
    }

    private async void InitApp()
    {
        bool b;
        using var userApp = new User();
        if (Setting.IsNull()) b = false;
        else
        {
            var jwt = await userApp.Login(Setting);
            b = !string.IsNullOrEmpty(jwt);
            Jwt = jwt;
            userApp.Jwt = jwt;
        }

        if (b)
        {
            await Init(userApp);
        }
        else
        {
            Dispatcher.UIThread.Post(() => FrameView.NavigateFromObject(new LoginView()));
        }
    }

    private async Task Init(User userApp)
    {
        User = await userApp.GetUserData();
        using var proj = new Project(Jwt);
        using var org = new Organize(Jwt);
        var projects = await proj.GetUserProjects();
        var organizes = await org.GetUserOrganizes();

        var home = Stack["Home"] as HomeViewModel;
        home!.Projects.Add(projects);
        home.Organizes.Add(organizes);
        home.TaskNotes.Add(User.TaskNotes);
        home.User = User;

        var project = Stack["Project"] as ProjectViewModel;
        project!.Projects.Add(projects);

        var organize = Stack["Organize"] as OrganizeViewModel;
        organize!.Organizes.Add(organizes);

        Dispatcher.UIThread.Post(() => FrameView.NavigateFromObject(home));
    }

    public async Task Login(LoginModel model)
    {
        using var user = new User();
        Jwt = await user.Login(model);
        if (string.IsNullOrEmpty(Jwt)) return;
        Setting = model;
        Setting.Save();
        user.Jwt = Jwt;
        await Init(user);
        Navigate("Home");
    }

    public async Task Signup(SignModel model)
    {
        using var userApp = new User();
        Jwt = await userApp.Signup(model);
        if (string.IsNullOrEmpty(Jwt)) return;
        Setting = new LoginModel() { PhoneNum = model.PhoneNum, Password = model.Password };
        Setting.Save();
        userApp.Jwt = Jwt;
        User = await userApp.GetUserData();
        var home = Stack["Home"] as HomeViewModel;
        home!.User = User;
        Navigate("Home");
    }

    public void Logout()
    {
        Setting = new LoginModel();
        Setting.Save();
        Navigate(new LoginView());
        var home = Stack["Home"] as HomeViewModel;
        home!.User = new UserModel();
        home.TaskNotes.Clear();
        home.Projects.Clear();
        home.Organizes.Clear();
        
        var proj = Stack["Project"] as ProjectViewModel;
        proj!.Projects.Clear();
        proj.Project = new ProjectModel();
        
        var org = Stack["Organize"] as OrganizeViewModel;
        org!.Organizes.Clear();
        org.Organize = new OrganizeModel();
        NotificationShow("提示", "登出成功", NotificationType.Success);
    }

    public void Add(ProjectModel p)
    {
        var home = Stack["Home"] as HomeViewModel;
        home?.Projects.Add(p);
        var project = Stack["Project"] as ProjectViewModel;
        project?.Projects.Add(p);
    }

    public void Add(OrganizeModel p)
    {
        var home = Stack["Home"] as HomeViewModel;
        home?.Organizes.Add(p);
        var organizeViewModel = Stack["Organize"] as OrganizeViewModel;
        organizeViewModel?.Organizes.Add(p);
    }

    public void Switch(OrganizeModel model)
    {
        if (Stack["Organize"] is not OrganizeViewModel organize) return;
        if (organize.Organizes.Any(x => x.Id == model.Id))
            organize.Organize = model;
        Navigate("Organize");
    }

    public void Switch(ProjectModel model)
    {
        if (Stack["Project"] is not ProjectViewModel Project) return;
        if (Project.Projects.Any(x => x.Id == model.Id))
            Project.Project = model;
        Navigate("Project");
    }
    
    public void Switch(GanttModel model)
    {
        var home = Stack["Home"] as HomeViewModel;
        home?.TaskNotes.Add(model);
    }
    
    public void Remove(GanttModel model)
    {
        var home = Stack["Home"] as HomeViewModel;
        home?.TaskNotes.Remove(model);
    }
    
    public void Remove(ProjectModel model)
    {
        var home = Stack["Home"] as HomeViewModel;
        home?.Projects.Remove(model);
        if (Stack["Project"] is not ProjectViewModel project) return;
        project.Projects.Remove(model);
    }
}