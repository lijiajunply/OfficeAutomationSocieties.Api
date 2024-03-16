using System;
using Avalonia.Controls;
using FluentAvalonia.UI.Controls;
using OA.WindowApp.ViewModels.Pages;

namespace OA.WindowApp.Views;

public class NavigationFactory : INavigationPageFactory
{
    public Control GetPage(Type srcType)
    {
        return null!;
    }

    public Control GetPageFromObject(object target)
    {
        if (target is PageModelBase pageModelBase)
        {
            var page = GetType().FullName!.Replace("View", "Page").Replace("NavigationFactory", "")[..^1];
            var fullname = pageModelBase.GetType().FullName!;
            var name = fullname.Split(".")[^1].Replace("Model", "");
            var i = fullname.Split(".")[^2] == "Pages" ? "" : $".{fullname.Split(".")[^2]}";
            name = $"{page}{i}.{name}";
            var type = Type.GetType(name);
            if (type == null) return new TextBlock { Text = "Not Found: " + name };
            var c = (Control)Activator.CreateInstance(type)!;
            c.DataContext = pageModelBase;
            return c;
        }

        if (target is Control control) return control;

        throw new Exception();
    }
}