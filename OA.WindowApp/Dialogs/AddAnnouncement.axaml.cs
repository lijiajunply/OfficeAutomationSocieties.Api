using System;
using Avalonia.Controls;
using Oa.NetLib.Models;

namespace OA.WindowApp.Dialogs;

public partial class AddAnnouncement : UserControl
{
    public AddAnnouncement(AnnouncementModel? model = null)
    {
        InitializeComponent();
        TextBlock.Text = model == null ? "创建" : "更改";
        if (model == null) return;
        TitleBox.Text = model.Title;
        ContextBox.Text = model.Context;
    }

    public AnnouncementModel Done() => new()
        { Time = DateTime.Today.ToString("yyyy-MM-dd"), Context = ContextBox.Text ?? "", Title = TitleBox.Text ?? "" };
}