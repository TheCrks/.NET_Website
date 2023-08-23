namespace WebApplication1.Model
{
    public class GameModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class AddedByStatus
        {
            public int yet { get; set; }
            public int owned { get; set; }
            public int beaten { get; set; }
            public int toplay { get; set; }
            public int dropped { get; set; }
            public int playing { get; set; }
        }

        public class EsrbRating
        {
            public int id { get; set; }
            public string name { get; set; }
            public string slug { get; set; }
        }

        public class Filters
        {
            public List<Year> years { get; set; }
        }

        public class Genre
        {
            public int id { get; set; }
            public string name { get; set; }
            public string slug { get; set; }
            public int games_count { get; set; }
            public string image_background { get; set; }
        }

        public class ParentPlatform
        {
            public Platform platform { get; set; }
        }

        public class Platform
        {
            public Platform platform { get; set; }
            public string released_at { get; set; }
            public RequirementsEn requirements_en { get; set; }
            public RequirementsRu requirements_ru { get; set; }
        }

        public class Platform2
        {
            public int id { get; set; }
            public string name { get; set; }
            public string slug { get; set; }
            public object image { get; set; }
            public object year_end { get; set; }
            public int? year_start { get; set; }
            public int games_count { get; set; }
            public string image_background { get; set; }
        }

        public class Rating
        {
            public int id { get; set; }
            public string title { get; set; }
            public int count { get; set; }
            public double percent { get; set; }
        }

        public class RequirementsEn
        {
            public string minimum { get; set; }
            public string recommended { get; set; }
        }

        public class RequirementsRu
        {
            public string minimum { get; set; }
            public string recommended { get; set; }
        }

        public class Result
        {
            public int id { get; set; }
            public string slug { get; set; }
            public string name { get; set; }
            public string released { get; set; }
            public bool tba { get; set; }
            public string background_image { get; set; }
            public double rating { get; set; }
            public int rating_top { get; set; }
            public List<Rating> ratings { get; set; }
            public int ratings_count { get; set; }
            public int reviews_text_count { get; set; }
            public int added { get; set; }
            public AddedByStatus added_by_status { get; set; }
            public int metacritic { get; set; }
            public int playtime { get; set; }
            public int suggestions_count { get; set; }
            public DateTime updated { get; set; }
            public object user_game { get; set; }
            public int reviews_count { get; set; }
            public string saturated_color { get; set; }
            public string dominant_color { get; set; }
            public List<Platform> platforms { get; set; }
            public List<ParentPlatform> parent_platforms { get; set; }
            public List<Genre> genres { get; set; }
            public List<Store> stores { get; set; }
            public object clip { get; set; }
            public List<Tag> tags { get; set; }
            public EsrbRating esrb_rating { get; set; }
            public List<ShortScreenshot> short_screenshots { get; set; }
        }

        public class Root
        {
            public int count { get; set; }
            public string next { get; set; }
            public string previous { get; set; }
            public List<Result> results { get; set; }
            public string seo_title { get; set; }
            public string seo_description { get; set; }
            public string seo_keywords { get; set; }
            public string seo_h1 { get; set; }
            public bool noindex { get; set; }
            public bool nofollow { get; set; }
            public string description { get; set; }
            public Filters filters { get; set; }
            public List<string> nofollow_collections { get; set; }
        }

        public class ShortScreenshot
        {
            public int id { get; set; }
            public string image { get; set; }
        }

        public class Store
        {
            public int id { get; set; }
            public Store store { get; set; }
        }

        public class Store2
        {
            public int id { get; set; }
            public string name { get; set; }
            public string slug { get; set; }
            public string domain { get; set; }
            public int games_count { get; set; }
            public string image_background { get; set; }
        }

        public class Tag
        {
            public int id { get; set; }
            public string name { get; set; }
            public string slug { get; set; }
            public string language { get; set; }
            public int games_count { get; set; }
            public string image_background { get; set; }
        }

        public class Year
        {
            public int from { get; set; }
            public int to { get; set; }
            public string filter { get; set; }
            public int decade { get; set; }
            public List<Year> years { get; set; }
            public bool nofollow { get; set; }
            public int count { get; set; }
            public int year { get; set; }
        }


    }
}
