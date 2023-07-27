using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaui;

public partial class ErrorPopupPage : CommunityToolkit.Maui.Views.Popup
{
    public ErrorPopupPage(string ErrorMessage)
    {
        InitializeComponent();
        errorName.Text += ErrorMessage;
    }

    private void CloseButtonClicked(object sender, EventArgs e)
    {
        Close();
    }
}