using Camille.Enums;
using Camille.RiotGames;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using static Microsoft.Maui.Controls.VisualMarker;
using Platform = Microsoft.Maui.ApplicationModel.Platform;

namespace TestMaui;

public partial class MainPage : ContentPage
{
    public string summonerName;
    
    public MainPage()
    {
        InitializeComponent();
        summonerNameEntry.Unfocused += OnEntryUnfocused;
        summonerNameEntry.HorizontalTextAlignment = TextAlignment.Center;
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
    
    private void OnEntryUnfocused(object sender, FocusEventArgs e)
    {
        summonerNameEntry.Unfocus();
    }


    async private void GetSummoner(string summonerNameArg)
    {
        var streamReader =
            new StreamReader(
                @"C:\Users\alcam\OneDrive\Documents\Developpement\wintest\TestMaui\TestMaui\RIOT_TOKEN.txt");
        var token = streamReader.ReadLine();
        var api = RiotGamesApi.NewInstance(token);
        try
        {
            var summoner = api.SummonerV4().GetBySummonerName(PlatformRoute.EUW1, summonerNameArg);
            await Navigation.PushAsync(new SummonerInfoPage(api, summoner));
                summonerNameEntry.Text = "";
        }
        catch (AggregateException e)
        {
            this.ShowPopup(new ErrorPopupPage("Ce nom d'invocateur est invalide" + e.Message));
        }
    }
}