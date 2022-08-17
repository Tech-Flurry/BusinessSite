namespace TechFlurry.BusinessSite.Models
{
    public class BlogModel : DocumentModel
    {
        public BlogModel()
        {
            Comments = new List<Comment>();
            Tags = new List<string>();
        }
        public AuthorModel Author { get; set; }
        public string Heading { get; set; }
        public string MainImage { get; set; }
        public string ShortTitle { get; set; }
        public string Identifier { get; set; }
        public DateTime DatePosted { get; set; }
        public string Category { get; set; }
        public BlogType BlogType { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; }
        public List<Comment> Comments { get; set; }

    }
    public class Comment
    {
        public UserModel User { get; set; }
        public string Content { get; set; }
        public DateTime PostedTime { get; set; }
    }
    public enum BlogType
    {
        SinglePage
    }
}
