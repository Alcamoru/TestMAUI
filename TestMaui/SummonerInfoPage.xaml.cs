using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camille.Enums;
using Camille.RiotGames;
using Camille.RiotGames.MatchV5;
using Camille.RiotGames.SummonerV4;
using CommunityToolkit.Maui.Views;

namespace TestMaui;

public partial class SummonerInfoPage : ContentPage
{
    public SummonerInfoPage(RiotGamesApi api, Summoner summoner)
    {
        GetStats(api, summoner);
    }

    private async void GetStats(RiotGamesApi api, Summoner summoner)
    {
        InitializeComponent();
        int profileIconId = summoner.ProfileIconId;
        ProfileIcon.Source = 
            ImageSource.FromUri(new Uri($"http://ddragon.leagueoflegends.com/cdn/13.11.1/img/profileicon/{profileIconId}.png"));
        TitleLabel.Text = summoner.Name + " en Ranked";
        SummonerNameLabel.Text = summoner.Name;
        LevelLabel.Text = "Level " + summoner.SummonerLevel;
        var ids = api.MatchV5().GetMatchIdsByPUUID(RegionalRoute.EUROPE, summoner.Puuid, count: 20);
        List<Match> matches = new();
        Dictionary<string, Dictionary<string, int>> topChamps = new Dictionary<string, Dictionary<string, int>>();
        foreach (var id in ids)
        {
            try
            {
                var match = api.MatchV5().GetMatchAsync(RegionalRoute.EUROPE, id);
                foreach (Participant participant in match.Result.Info.Participants)
                {
                    if (participant.SummonerName.Equals("Alcamoru"))
                    {
                        if (topChamps.ContainsKey(participant.ChampionName))
                        {
                            if (participant.Win)
                            {
                                topChamps[participant.ChampionName]["wins"] += 1;
                            }
                            else
                            {
                                topChamps[participant.ChampionName]["loses"] += 1;
                            }

                            topChamps[participant.ChampionName]["nmatches"] += 1;
                            topChamps[participant.ChampionName]["kills"] += participant.Kills;
                            topChamps[participant.ChampionName]["deaths"] += participant.Deaths;
                            topChamps[participant.ChampionName]["assists"] += participant.Assists;
                        }
                        else
                        {
                            Dictionary<string, int> statsDict = new Dictionary<string, int>();
                            if (participant.Win)
                            {
                                statsDict.Add("wins", 1);
                                statsDict.Add("loses", 0);
                            }
                            else
                            {
                                statsDict.Add("wins", 0);
                                statsDict.Add("loses", 1);
                            }

                            statsDict.Add("nmatches", 1);
                            statsDict.Add("kills", participant.Kills);
                            statsDict.Add("deaths", participant.Deaths);
                            statsDict.Add("assists", participant.Assists);
                            topChamps.Add(participant.ChampionName, statsDict);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        var champ = topChamps.First();
        double champKDA = (champ.Value["kills"] + champ.Value["assists"]) / champ.Value["deaths"];
        ChampionIconImage.Source = ImageSource.FromUri(new Uri($"http://ddragon.leagueoflegends.com/cdn/13.12.1/img/champion/{champ.Key}.png"));
        ChampionKdaLabel.Text = champKDA.ToString() + " KDA";
        ChampionNameLabel.Text = champ.Key;
        ChampionWinsLoses.Text = champ.Value["wins"].ToString() + "W/" + champ.Value["loses"].ToString() + "L";
        WinrateLabel.Text = (champ.Value["wins"] / (champ.Value["wins"] + champ.Value["loses"]) * 100).ToString() + "%";
    }
}
