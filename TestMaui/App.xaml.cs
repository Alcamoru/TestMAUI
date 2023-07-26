namespace TestMaui;

public partial class App : Application
{
	public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}