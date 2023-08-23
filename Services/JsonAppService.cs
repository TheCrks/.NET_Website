using System.Net;
using System.Text.Json;
using WebApplication1.Model;
using System.Net.Http.Headers;
using System.Net.Http.Headers;
using RestSharp;
using System.Text;
using System;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using static WebApplication1.Model.GameModel;
using static WebApplication1.Model.NextGameModel;

public class JsonAppService
{

    public async Task<AppModel> GetAppModel(string term)
    {
        var body = "";
        var client = new HttpClient();
        string url = "https://steam2.p.rapidapi.com/search/"+term+"/page/1";
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(url),
            Headers =
    {
        { "X-RapidAPI-Key", "e61958c0f6msh1f1e0612b228a7fp12dedcjsn22e2847fb783" },
        { "X-RapidAPI-Host", "steam2.p.rapidapi.com" },
    },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            body = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(body);
        }

        var appModels = JsonConvert.DeserializeObject<List<AppModel>>(body);

        if (appModels.Count > 0)
        {
            AppModel appModel = appModels[0];
            //Console.WriteLine(appModel.title);
            return appModel;
        }
        else
        {
            // Handle the case when no AppModel objects are returned
            // You can throw an exception, return null, or handle it according to your application's logic
            return null;
        }
    }

    public async Task<GameModel.Root> GetGameList(string url)
    {
        var client = new HttpClient();
        var body = "";
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(url),
            Headers =
    {
        { "X-RapidAPI-Key", "e61958c0f6msh1f1e0612b228a7fp12dedcjsn22e2847fb783" },
        { "X-RapidAPI-Host", "rawg-video-games-database.p.rapidapi.com" },
    },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            body = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(body);
        }
        GameModel.Root root = JsonConvert.DeserializeObject<GameModel.Root>(body);
        return root;
    }

    public List<GameModel.Result> GetGames(GameModel.Root root)
    {
        List<GameModel.Result> result = root.results;
        return result;
    }

    public GameModel.Result GetGameById(GameModel.Root root, int ID) {
        List<GameModel.Result> list = GetGames(root);

        return list.FirstOrDefault(x => x.id == ID);
    }

    public  NextGameModel.Root nextGame(string url)
    {
        var json = "";
        using (WebClient wc = new WebClient())
        {
            json = wc.DownloadString(url);
        }
        //Console.WriteLine(json);
        NextGameModel.Root root = JsonConvert.DeserializeObject<NextGameModel.Root>(json);
        return root;
    }
    public NextGameModel.Result GetNextGameById(string url, int ID)
    {
        List<NextGameModel.Result> list = nextGame(url).results;

        return list.FirstOrDefault(x => x.id == ID);
    }
}
        


