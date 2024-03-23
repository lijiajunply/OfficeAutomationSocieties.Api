using Avalonia.Controls;
using Oa.NetLib.Models;

namespace OA.WindowApp.Dialogs;

public partial class OrganizeAddProject : UserControl
{
    public OrganizeAddProject()
    {
        InitializeComponent();
    }

    public ProjectModel Done() => new() { Name = NameBox.Text ?? "", Introduce = IntroduceBox.Text ?? "" };
}