using Avalonia.Controls;
using Oa.NetLib.Models;

namespace OA.WindowApp.Dialogs;

public partial class UpdateProject : UserControl
{
    private readonly string _id;

    public UpdateProject(ProjectModel model)
    {
        InitializeComponent();
        NameBox.Text = model.Name;
        IntroduceBox.Text = model.Introduce;
        _id = model.Id;
    }

    public ProjectModel Done() => string.IsNullOrEmpty(NameBox.Text) || string.IsNullOrEmpty(IntroduceBox.Text)
        ? new ProjectModel()
        : new ProjectModel { Id = _id, Name = NameBox.Text, Introduce = IntroduceBox.Text };
}