using System;
using Avalonia.Controls;
using Oa.NetLib.Models;

namespace OA.WindowApp.Dialogs;

public partial class UseResource : UserControl
{
    public UseResource(ResourceModel model)
    {
        InitializeComponent();
        StartPicker.SelectedDate =
            string.IsNullOrEmpty(model.StartTime) ? DateTime.Today : DateTime.Parse(model.StartTime);
        EndPicker.SelectedDate =
            string.IsNullOrEmpty(model.EndTime) ? DateTime.Today : DateTime.Parse(model.EndTime);
    }

    public ResourceModel Done() => new()
    {
        StartTime = StartPicker.SelectedDate!.Value.ToString("yyyy-MM-dd"),
        EndTime = EndPicker.SelectedDate!.Value.ToString("yyyy-MM-dd")
    };
}