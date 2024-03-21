﻿using Avalonia.Controls;
using Avalonia.Interactivity;
using Oa.NetLib.Models;

namespace OA.WindowApp.Dialogs;

public partial class JoinOrCreateOrganize : UserControl
{
    public JoinOrCreateOrganize()
    {
        InitializeComponent();
    }

    private void ConvertClick(object? sender, RoutedEventArgs e)
    {
        CreateBorder.IsVisible = !CreateBorder.IsVisible;
    }

    public (OrganizeModel organize, bool isCreate) Done()
        => (new OrganizeModel() { Name = (CreateBorder.IsVisible ? NameBox.Text : IdBox.Text) ?? "" },
            CreateBorder.IsVisible);
}