using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Media;
using FluentAvalonia.UI.Windowing;

namespace OA.WindowApp.Views;

public class MainAppSplashScreen(string appName, Action? initApp)
    : IApplicationSplashScreen
{
    public string AppName { get; } = appName;

    // ReSharper disable once UnassignedGetOnlyAutoProperty
    public IImage? AppIcon { get; }
    public object SplashScreenContent => new MainAppSplashContent();
    public int MinimumShowTime => 2000;

    private Action? InitApp { get; set; } = initApp;

    public Task RunTasks(CancellationToken cancellationToken)
    {
        return InitApp == null ? Task.CompletedTask : Task.Run(InitApp, cancellationToken);
    }
}