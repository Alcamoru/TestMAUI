using Camille.Enums;
using Camille.RiotGames;
using Camille.RiotGames.SummonerV4;

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
        var profileIconId = summoner.ProfileIconId;
        ProfileIcon.Source =
            ImageSource.FromUri(
                new Uri($"http://ddragon.leagueoflegends.com/cdn/13.11.1/img/profileicon/{profileIconId}.png"));
        TitleLabel.Text = summoner.Name + " en Ranked";
        SummonerNameLabel.Text = summoner.Name;
        LevelLabel.Text = "Level " + summoner.SummonerLevel;
        var ids = api.MatchV5().GetMatchIdsByPUUID(RegionalRoute.EUROPE, summoner.Puuid, 20);
        List<Dictionary<string, string>> champList = new();
        foreach (var id in ids)
        {
            var match = api.MatchV5().GetMatchAsync(RegionalRoute.EUROPE, id);
            try
            {
                foreach (var participant in match.Result.Info.Participants) {
                    if (participant.SummonerName == summoner.Name)
                    {
                        foreach (var champion in champList)
                        {
                            if (champion["ChampionName"] == participant.ChampionName)
                            {
                                if (participant.Win)
                                {
                                    champion["wins"] = (int.Parse(champion["wins"]) + 1).ToString();
                                }

                                else
                                {
                                    champion["loses"] = (int.Parse(champion["loses"]) + 1).ToString();
                                    champion["nmatches"] = (int.Parse(champion["nmatches"]) + 1).ToString();
                                    champion["kills"] = (int.Parse(champion["kills"]) + participant.Kills).ToString();
                                    champion["deaths"] = (int.Parse(champion["deaths"]) + participant.Deaths).ToString();
                                    champion["assists"] = (int.Parse(champion["assists"]) + participant.Assists).ToString();
                                }
                            }
                            else
                            {
                                var championDict = new Dictionary<string, string>();
                                if (participant.Win)
                                {
                                    championDict.Add("wins", "1");
                                    championDict.Add("loses", "0");
                                }
                                else
                                {
                                    championDict.Add("wins", "0");
                                    championDict.Add("loses", "1");
                                }

                                championDict.Add("nmatches", "1");
                                championDict.Add("ChampionName", participant.ChampionName);
                                championDict.Add("kills", participant.Kills.ToString());
                                championDict.Add("deaths", participant.Deaths.ToString());
                                championDict.Add("assists", participant.Assists.ToString());
                                champList.Add(championDict);
                            }
                        }
                    }
                }
                    
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            

        }
        System.Diagnostics.Debug.WriteLine(champList.Count);
        foreach (var champ in SortTopChamps(champList))
        {
            foreach (var value in champ)
            {
                System.Diagnostics.Debug.WriteLine(value.Key);

                System.Diagnostics.Debug.WriteLine(value.Value);
            }
        }


        /*var champ = topChamps.First();
        double champKDA = (champ.Value["kills"] + champ.Value["assists"]) / champ.Value["deaths"];
        ChampionIconImage.Source = ImageSource.FromUri(new Uri($"http://ddragon.leagueoflegends.com/cdn/13.12.1/img/champion/{champ.Key}.png"));
        ChampionKdaLabel.Text = champKDA.ToString() + " KDA";
        ChampionNameLabel.Text = champ.Key;
        ChampionWinsLoses.Text = champ.Value["wins"].ToString() + "W/" + champ.Value["loses"].ToString() + "L";
        WinrateLabel.Text = (champ.Value["wins"] / (champ.Value["wins"] + champ.Value["loses"]) * 100).ToString() + "%";*/
    }

    private List<Dictionary<string, string>> SortTopChamps(List<Dictionary<string, string>> topChampsArg)
    {
        var topChampsArgSorted = topChampsArg.OrderBy(dictionnary => int.Parse(dictionnary["nmatches"]));

        return topChampsArgSorted.ToList();

        /*Dictionary<string, string> mostPlayed = new Dictionary<string, string>();
        foreach (Dictionary<string,string> dictionary in topChampsArg)
        {
            if (int.Parse(dictionary["nmatches"]) >= int.Parse(mostPlayed["nmatches"]))
            {
                mostPlayed = dictionary;
            }
        }*/
    }
}