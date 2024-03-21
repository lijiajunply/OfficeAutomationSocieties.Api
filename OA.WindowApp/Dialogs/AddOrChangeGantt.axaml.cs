using System;
using Avalonia.Controls;
using Oa.NetLib.Models;

namespace OA.WindowApp.Dialogs;

public partial class AddOrChangeGantt : UserControl
{
    public AddOrChangeGantt(GanttModel? model = null)
    {
        InitializeComponent();
        if (model == null)
        {
            StartPicker.SelectedDate = EndPicker.SelectedDate = DateTime.Today;            
        }
        else
        {
            NameBox.Text = model.ToDo;
            StartPicker.SelectedDate = DateTime.Parse(model.StartTime);
            EndPicker.SelectedDate = DateTime.Parse(model.EndTime);
        }

    }

    public GanttModel Done()
    {
        if (string.IsNullOrEmpty(NameBox.Text) || StartPicker.SelectedDate == null || EndPicker.SelectedDate == null)
            return new GanttModel();
        return new GanttModel()
        {
            ToDo = NameBox.Text, StartTime = StartPicker.SelectedDate.Value.ToString("yyyy-MM-dd"),
            EndTime = EndPicker.SelectedDate.Value.ToString("yyyy-MM-dd")
        };
    }
}