namespace TestMaui;

public partial class MainPage : ContentPage
{
    private string summonerName;
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnSummonerClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(summonerNameEntry.Text))
        {
        }
        else
        {
            summonerName = summonerNameEntry.Text;
            Test.Text = summonerName;
            summonerNameEntry.Text = "";
        }
    }


    private void SummonerNameEntry_OnCompleted(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(summonerNameEntry.Text))
        {
            
        }
        else
        {
            summonerName = summonerNameEntry.Text;
            Test.Text = summonerName;
            summonerNameEntry.Text = "";
        }
    }
}