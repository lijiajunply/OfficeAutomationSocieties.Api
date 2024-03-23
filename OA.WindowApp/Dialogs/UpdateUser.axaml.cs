using Avalonia.Controls;
using Oa.NetLib.Models;

namespace OA.WindowApp.Dialogs;

public partial class UpdateUser : UserControl
{
    public UpdateUser(UserModel model)
    {
        InitializeComponent();
        NameBox.Text = model.Name;
        PhoneBox.Text = model.PhoneNum;
    }

    public UserModel? Done()
    {
        if (string.IsNullOrEmpty(NameBox.Text) || string.IsNullOrEmpty(PhoneBox.Text)) return null;
        return new UserModel() { Name = NameBox.Text, PhoneNum = PhoneBox.Text };
    }
}