using Microsoft.Maui.Converters;

namespace TestMaui.Controls;

public class CustomEntry : Entry
{
    public static readonly BindableProperty BorderColorProperty =
        BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomEntry), 
            Color.FromRgb(255,0 , 0), propertyChanged: (bindable, oldValue, newValue) =>
            {
                var control = (CustomEntry)bindable;
                control.BorderColor = newValue as Color;
            });
    
    public Color BorderColor
    {
        get {return (Color)GetValue(BorderColorProperty);}
        set {SetValue(BorderColorProperty, value);}
    }

    
    
    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(CustomEntry), 
            0, propertyChanged: (bindable, oldValue, newValue) =>
            {
                var control = (CustomEntry)bindable;
                control.CornerRadius = (int)newValue;
            });
    
    
    
    public int CornerRadius
    {
        get {return (int)GetValue(BorderColorProperty);}
        set {SetValue(CornerRadiusProperty, value);}
    }

}