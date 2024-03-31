using System;
using Avalonia.Controls;
using Oa.NetLib.Models;

namespace OA.WindowApp.Dialogs;

public partial class AddResource : UserControl
{
    public AddResource(ResourceModel? model = null)
    {
        InitializeComponent();
        TextBlock.Text = model == null ? "创建" : "更改";
        if (model == null) return;
        NameBox.Text = model.Name;
        IntroduceBox.Text = model.Introduce;
    }

    public ResourceModel Done() => new()
    {
        CreateTime = DateTime.Today.ToString("yyyy-MM-dd"), Name = NameBox.Text ?? "",
        Introduce = IntroduceBox.Text ?? ""
    };
}