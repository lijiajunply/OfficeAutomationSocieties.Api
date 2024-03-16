using Avalonia;
using Avalonia.Controls;
using OA.WindowApp.ViewModels;

namespace OA.WindowApp.Models;

public static class ViewOpera
{
    public static T? GetView<T>(StyledElement? control) where T : StyledElement
    {
        while (true)
        {
            if (control is null) return default;
            if (control is T t) return t;
            control = control.Parent;
        }
    }

    public static T? GetViewData<T>(StyledElement? control) where T : ViewModelBase
    {
        while (true)
        {
            if (control is null) return default;
            if (control.DataContext is T t) return t;
            control = control.Parent;
        }
    }

    public static ItemCollection? GetList(Control control)
    {
        while (true)
        {
            if (control is null) return null;
            if (control is ItemsControl itemsControl) return itemsControl.Items;
            if (control.Parent == null) return null;
            if (control.Parent is ItemsControl items) return items.Items;
            control = (Control)control.Parent;
        }
    }
}