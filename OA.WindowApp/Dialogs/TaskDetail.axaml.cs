using System;
using Avalonia.Controls;
using Oa.NetLib.Models;

namespace OA.WindowApp.Dialogs;

public partial class TaskDetail : UserControl
{
    public TaskDetail(string projName)
    {
        InitializeComponent();
        ProjectName.Text = projName;
    }
    
    protected override void OnDataContextChanged(EventArgs e)
    {
        if(DataContext is not GanttModel model)return;
        Block.Text = model.IsExpired ? "超时了" : "正在进行";
    }
}