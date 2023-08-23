using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using WebApplication1.Model;
using WebApplication1.Services;
using static WebApplication1.Model.GameModel;
using static WebApplication1.Model.NextGameModel;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {


        [BindProperty(SupportsGet = true)]
        public int pageOfRoot { get; set; }

        [BindProperty]
        public string Term { get; set; }

        [BindProperty]
        public string Data { get; set; }

        [BindProperty]
        public string DataImg { get; set; }
        [BindProperty]
        public int GameId { get; set; }
        [BindProperty]
        public List<string> urls { get; set; }
        [BindProperty]
        public List<string> names { get; set; }
        [BindProperty]
        public List<int> Ids { get; set; }
        [BindProperty]
        public GameModel.Root root { get; set; }
        [BindProperty]
        int pagenumber { get; set; }
        [BindProperty]
        public string urlforget { get; set; }
        [BindProperty]
        public int pageRoute { get; set; }
        [BindProperty]
        public NextGameModel.Root nextRoot { get; set; }
        AppModel AppData { get; set; }

        /*private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }*/
        public async Task OnGetAsync(int pageOfRoot, JsonAppService jsonAppService)
        {
            this.pageOfRoot = pageOfRoot;
            Console.WriteLine(pageOfRoot);
            await LoadData(pageOfRoot, jsonAppService);

        }

        private async Task LoadData(int pageOfRoot, JsonAppService jsonAppService)
        {
            int urlPage=pageOfRoot+1;
            urls = new List<string>();
            names = new List<string>();
            Ids = new List<int>();

            string key = "9c8dac46b3ba43fd86a6d59c994fb6f3";
            string u = "https://rawg-video-games-database.p.rapidapi.com/games?key=";
            string url = "https://rawg-video-games-database.p.rapidapi.com/games?key=" + key;
            List<GameModel.Result> finalResults = new List<GameModel.Result>();
            List<NextGameModel.Result> finalResultsnext = new List<NextGameModel.Result>();
            root = await jsonAppService.GetGameList(url);

            if (pageOfRoot <= 0)
            {
                finalResults.AddRange(root.results);
                foreach (GameModel.Result result in finalResults)
                {
                    if (result != null)
                    {
                        urls.Add(result.background_image);
                        names.Add(result.name);
                        Ids.Add(result.id);
                    }
                }
            }
            else
            {
                url = "https://api.rawg.io/api/games?key=9c8dac46b3ba43fd86a6d59c994fb6f3&page=" + urlPage;
                urlforget = url;
                nextRoot = jsonAppService.nextGame(url);
                finalResultsnext.AddRange(nextRoot.results);
                foreach (NextGameModel.Result result in finalResultsnext)
                {
                    if (result != null)
                    {
                        Console.WriteLine(result.name);
                        urls.Add(result.background_image);
                        names.Add(result.name);
                        Ids.Add(result.id);
                    }
                }
                GetPageFromUrl(urlforget);
            }
            
            }
        public void GetPageFromUrl(string url)
        {
            string[] split = url.Split('=');
            pageRoute = int.Parse(split[2]);
        }
        public  IActionResult OnPostWikiRequest(JsonAppService jsonAppService)
        {
            int parsedPage;
            if (int.TryParse(Term, out parsedPage)) // Parse the Term value to int
            {
                pageOfRoot = parsedPage-1; // Assign the parsed value to the page property
            }
            
            return RedirectToPage("/Index", new {pageOfRoot});
        }

        public async Task OnPostGameRequest(JsonAppService jsonAppService)
        {
            string key = "9c8dac46b3ba43fd86a6d59c994fb6f3";
            string u = "https://rawg-video-games-database.p.rapidapi.com/games?key=";
            string url = "https://rawg-video-games-database.p.rapidapi.com/games?key=" + key;
            List<GameModel.Result> finalResults = new List<GameModel.Result>();
            List<NextGameModel.Result> finalResultsnext = new List<NextGameModel.Result>();
            root = await jsonAppService.GetGameList(url);
            finalResults.AddRange(root.results);

            for (int i = 2; i < 12; i++)
            {
               // Console.Write(i + "  ");
                if (i == 2)
                    url = root.next;
                else
                    url = nextRoot.next;
               // Console.WriteLine(url);
                nextRoot = jsonAppService.nextGame(url);
               // Console.WriteLine("after root");
                finalResultsnext.AddRange(nextRoot.results);
            }
           /* foreach (GameModel.Result result in finalResults)
            {
              //  Console.WriteLine(result.name);
            }
            foreach (NextGameModel.Result result in finalResultsnext)
            {
              //  Console.WriteLine(result.name);
            }*/

            foreach (GameModel.Result result in finalResults)
            {
                if (result != null)
                {
                    urls.Add(result.background_image);
                    names.Add(result.name);
                }
            }
            foreach (NextGameModel.Result result in finalResultsnext)
            {
                if (result != null)
                {
                    urls.Add(result.background_image);
                    names.Add(result.name);
                }
            }

        }


    }
}