using System.Collections.Generic;
using Avalonia.Controls;
using Oa.NetLib.Models;

namespace OA.WindowApp.Dialogs;

public partial class ShowUsers : UserControl
{
    public ShowUsers(IEnumerable<UserModel> array)
    {
        InitializeComponent();
        UserItems.ItemsSource = array;
    }
}