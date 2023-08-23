using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing.Constraints;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.Design;
using WebApplication1.Model;
using WebApplication1.Services;
using static WebApplication1.Model.GameModel;
using static WebApplication1.Model.NextGameModel;

namespace WebApplication1.Pages
{
    public class gameModel : PageModel
    {
        [BindProperty]
        public int CommentId { get; set; }
        [BindProperty]
        public double totalRating { get; set; }
        [BindProperty]
        public int rating { get; set; }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Text { get; set; }

        [BindProperty(SupportsGet = true)]
        public int id { get; set; }
        [BindProperty(SupportsGet = true)]
        public int pageOfRoot { get; set; }
        [BindProperty]
        public GameModel.Root root { get; set; }
        [BindProperty]
        public NextGameModel.Root nextroot { get; set; }
        [BindProperty]
        public GameModel.Result result { get; set; }
        [BindProperty]
        public NextGameModel.Result nextresult { get; set; }
        [BindProperty]
        public AppModel appModel { get; set; }
        public JsonCommentService CommentService { get; set; }

        public List<CommentModel> commentModels { get; set; }
        public List<CommentModel> gameComments { get; set; }
        private readonly ILogger<IndexModel> _logger;
        [BindProperty]
        public int GameID { get; set; }
        public gameModel(ILogger<IndexModel> logger, JsonCommentService jsonCommentService)
        {
            _logger = logger;
            CommentService = jsonCommentService;
        }

        public double CalculateRating()
        {
            double finalRating = 0;
            int count = 0;
            if (commentModels.Count > 0)
            {
                foreach (var comment in commentModels)
                {
                    count++;
                    finalRating += comment.Rating;
                }
                
                return (double)System.Math.Round(finalRating / count, 2);
            }
            else { return -1; }
        }
        public async Task OnGetAsync(int id, int pageOfRoot, JsonAppService jsonAppService)
        {
            if (pageOfRoot <= 0)
            {
                GameID = id;
                string key = "9c8dac46b3ba43fd86a6d59c994fb6f3";
                string u = "https://rawg-video-games-database.p.rapidapi.com/games?key=";
                string url = "https://rawg-video-games-database.p.rapidapi.com/games?key=" + key;
                root = await jsonAppService.GetGameList(url);

                result = jsonAppService.GetGameById(root, id);
                if (result != null)
                {
                    appModel = await jsonAppService.GetAppModel(result.name);
                    appModel.reviewSummary = appModel.reviewSummary.Replace("<br>", ".\n");

                    commentModels = CommentService.GetCommentsByGameId(id);
                    totalRating = CalculateRating();
                }
            }
            else
            {
                GameID = id;
                string url = "https://api.rawg.io/api/games?key=9c8dac46b3ba43fd86a6d59c994fb6f3&page=" + pageOfRoot;
                nextroot = jsonAppService.nextGame(url);
                nextresult = jsonAppService.GetNextGameById(url, id); 
                appModel = await jsonAppService.GetAppModel(nextresult.name);
                if (appModel.reviewSummary != null)
                {
                    appModel.reviewSummary = appModel.reviewSummary.Replace("<br>", ".\n");
                }
                commentModels = CommentService.GetCommentsByGameId(id);
                totalRating = CalculateRating();

            }
            
        }

        public ActionResult OnPostCommentRequest(){
            CommentModel commentModel = new CommentModel();
            commentModel.Name = Name;
            commentModel.Text = Text;
            commentModel.Time = DateTime.Now;
            commentModel.GameId = GameID;
            commentModel.Rating = rating;
            CommentService.AddComment(commentModel);

            return RedirectToPage("/Game", new { id = GameID, pageOfRoot});
        }

        public ActionResult OnPostDeleteRequest()
        {
            CommentService.DeleteComment(CommentId);
            return RedirectToPage("/Game", new { id = GameID, pageOfRoot });
        }

        public ActionResult OnPostUpdateRequest()
        {
            int updatedRating = int.Parse(Request.Form[$"rating-{CommentId}"]);
            CommentService.UpdateComment(CommentService.GetCommentById(CommentId),Text,Name,updatedRating);
            return RedirectToPage("/Game", new { id = GameID, pageOfRoot });
        }
    }
}
