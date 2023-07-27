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
        int profilIconId = summoner.ProfileIconId;
        profileIcon.Source = ImageSource.FromUri(new Uri($"http://ddragon.leagueoflegends.com/cdn/13.11.1/img/profileicon/{profilIconId}.png"));
        summonerNameLabel.Text = summonerStats.SummonerName + " en Ranked";
        summonerLevelLabel.Text = "Niveau" + summoner.SummonerLevel;
        summonerTierLabel.Text = "Tier" + summonerStats.Tier.ToLower();
        summonerRankLabel.Text = "Rank" + summonerStats.Rank;
        summonerWinsLabel.Text = "Wins" + summonerStats.Wins;
        summonerLossesLabel.Text = "Losses" + summonerStats.Losses;
    }
}
