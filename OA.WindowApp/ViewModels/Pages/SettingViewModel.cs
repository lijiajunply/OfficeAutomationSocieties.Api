using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using Avalonia.Styling;
using FluentAvalonia.Styling;

namespace OA.WindowApp.ViewModels.Pages;

public class SettingViewModel : PageModelBase
{
    public string[] AppThemes { get; } =
        [_system, _light, _dark];

    public FlowDirection[] AppFlowDirections { get; } =
        [FlowDirection.LeftToRight, FlowDirection.RightToLeft];

    public string CurrentAppTheme
    {
        get => _currentAppTheme;
        set
        {
            if (!SetField(ref _currentAppTheme, value)) return;
            var newTheme = GetThemeVariant(value);
            if (newTheme != null!)
                Application.Current!.RequestedThemeVariant = newTheme;
            _faTheme!.PreferSystemTheme = value == _system;
        }
    }

    private ThemeVariant? GetThemeVariant(string value)
        => value switch
        {
            _light => ThemeVariant.Light,
            _dark => ThemeVariant.Dark,
            _ => null
        };

    public FlowDirection CurrentFlowDirection
    {
        get => _currentFlowDirection;
        set
        {
            if (!SetField(ref _currentFlowDirection, value)) return;
            var lifetime = Application.Current!.ApplicationLifetime;
            if (lifetime is IClassicDesktopStyleApplicationLifetime cdl)
            {
                if (cdl.MainWindow!.FlowDirection == value)
                    return;
                cdl.MainWindow.FlowDirection = value;
            }
            else if (lifetime is ISingleViewApplicationLifetime single)
            {
                var mainWindow = TopLevel.GetTopLevel(single.MainView);
                if (mainWindow!.FlowDirection == value)
                    return;
                mainWindow.FlowDirection = value;
            }
        }
    }

    private string _currentAppTheme = _system;
    private FlowDirection _currentFlowDirection;

    private const string _system = "System";
    private const string _dark = "Dark";
    private const string _light = "Light";
    private readonly FluentAvaloniaTheme? _faTheme = Application.Current!.Styles[0] as FluentAvaloniaTheme;
}