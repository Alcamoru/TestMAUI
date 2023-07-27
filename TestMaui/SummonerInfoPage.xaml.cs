using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MingweiSamuel.Camille;
using MingweiSamuel.Camille.Enums;
using MingweiSamuel.Camille.SummonerV4;
using Region = MingweiSamuel.Camille.Enums.Region;

namespace TestMaui;

public partial class SummonerInfoPage : ContentPage
{
    public SummonerInfoPage(RiotApi api, Summoner summoner)
    {
        InitializeComponent();
        var summonerStats = api.LeagueV4.GetLeagueEntriesForSummoner(Region.EUW, summoner.Id)[0];
        summonerNameLabel.Text = "Nom d'invocateur: " + summoner.Name;
        summonerLevelLabel.Text = "Niveau" + summoner.SummonerLevel;
        summonerTier.Text = summonerStats.Wins.ToString();
        Defaites.Text = summonerStats.Losses.ToString();
    }
}
