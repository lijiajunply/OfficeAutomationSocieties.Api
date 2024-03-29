using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Oa.NetLib.Models;

namespace OA.WindowApp.Converters;

public class ResourceTemplate : IDataTemplate
{
    public Control Build(object? param)
    {
        if (param is not ResourceModel model) return new TextBlock();
        if (string.IsNullOrEmpty(model.StartTime))
        {
            return new TextBlock() { Text = "未开始" };
        }

        var t1 = new TextBlock() { Text = model.StartTime };
        var t2 = new TextBlock() { Text = model.EndTime };
        var grid = new Grid
        {
            Children = { t1, t2 },
            ColumnDefinitions = ColumnDefinitions.Parse("*,*")
        };
        Grid.SetColumn(t1,0);
        Grid.SetColumn(t2,1);
        return grid;
    }

    public bool Match(object? data) => data is ResourceModel;
}