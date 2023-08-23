namespace WebApplication1.Model
{
    public class CommentModel
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public int Rating { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            CommentModel otherComment = (CommentModel)obj;
            return this.Id == otherComment.Id;
        }
    }
}
