using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaui.Controls;

public partial class CustomLabel : ContentView
{


    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string),
        typeof(CustomLabel), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (CustomLabel)bindable;
            control.Titlelabel.Text = newValue as string;
        });
    public CustomLabel()
    {
        InitializeComponent();
    }

    public string Title
    {
        get => GetValue(TitleProperty) as string;
        set => SetValue(TitleProperty, value);
    }
}