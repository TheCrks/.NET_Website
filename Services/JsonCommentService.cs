//using Newtonsoft.Json;
using System.Text.Json;
using WebApplication1.Model;
namespace WebApplication1.Services
{
    public class JsonCommentService
    {


        public IWebHostEnvironment WebHostEnvironment { get; set; }
        public JsonCommentService(IWebHostEnvironment webHostEnvironment) {
            WebHostEnvironment = webHostEnvironment;
        }

        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "json", "comments.json");
        public List<CommentModel> GetCommentModels()
        {
            List<CommentModel> list = new List<CommentModel>();

            var json = File.OpenText(JsonFileName);
            list = JsonSerializer.Deserialize<List<CommentModel>>(json.ReadToEnd());
            json.Close();
            return list;
        }

        public List<CommentModel> GetCommentsByGameId(int ID)
        {
            List<CommentModel> list = GetCommentModels();

            return list.FindAll(x => x.GameId == ID);
        }

        public CommentModel GetCommentById(int ID)
        {
            List<CommentModel> list = GetCommentModels();

            return list.FirstOrDefault(x => x.Id == ID);
        }

       public void AddComment(CommentModel newComment)
        {
            List<CommentModel> list = GetCommentModels();
            newComment.Id = list.Max(x => x.Id)+1;
            list.Add(newComment);

            Stream json = File.Create(JsonFileName);
            JsonSerializer.Serialize<List<CommentModel>>(json, list);
            json.Close();
        }

        public void DeleteComment(int id)
        {
            List<CommentModel> list = GetCommentModels();
            CommentModel comment = GetCommentById(id);
            list.Remove(comment);

            Stream json = File.Create(JsonFileName);
            JsonSerializer.Serialize<List<CommentModel>>(json, list);
            json.Close();
        }

        public void UpdateComment(CommentModel comment, string text, string name, int rating)
        {
            List<CommentModel> list = GetCommentModels();
            CommentModel OldComment = GetCommentById(comment.Id);
            list.Remove(OldComment);
            comment.Text = text;
            comment.Name = name;
            comment.Time = DateTime.Now;
            comment.Rating = rating;
            list.Add(comment);

            Stream json = File.Create(JsonFileName);
            JsonSerializer.Serialize<List<CommentModel>>(json, list);
            json.Close();
        }
    }
}
