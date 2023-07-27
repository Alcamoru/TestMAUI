using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using MingweiSamuel.Camille;
using static Microsoft.Maui.Controls.VisualMarker;
using Region = MingweiSamuel.Camille.Enums.Region;

namespace TestMaui;

public partial class MainPage : ContentPage
{
    public string summonerName;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnEntryComplete(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(summonerNameEntry.Text))
        {
            this.ShowPopup(new ErrorPopupPage("Veuillez entrer une chaine de caractères valide."));
        }
        else
        {
            summonerName = summonerNameEntry.Text;
            GetSummoner(summonerName);
        }
    }


    private void GetSummoner(string summonerNameArg)
    {
        var streamReader =
            new StreamReader(
                @"C:\Users\alcam\OneDrive\Documents\Developpement\wintest\TestMaui\TestMaui\RIOT_TOKEN.txt");
        var token = streamReader.ReadLine();
        var api = RiotApi.NewInstance(token ?? throw new InvalidOperationException());
        try
        {
            var summoner = api.SummonerV4.GetBySummonerName(Region.EUW, summonerNameArg);
            Navigation.PushAsync(new SummonerInfoPage(api, summoner));
            Test.Text = summonerNameArg;
            summonerNameEntry.Text = "";
        }
        catch (AggregateException e)
        {
            this.ShowPopup(new ErrorPopupPage("Ce nom d'invocateur est invalide"));
        }
    }
}