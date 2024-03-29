using System;
using Avalonia.Controls;
using Oa.NetLib.Models;

namespace OA.WindowApp.Dialogs;

public partial class AddResource : UserControl
{
    public AddResource()
    {
        InitializeComponent();
    }

    public ResourceModel Done() => new()
    {
        CreateTime = DateTime.Today.ToString("yyyy-MM-dd"), Name = NameBox.Text ?? "",
        Introduce = IntroduceBox.Text ?? ""
    };
}