namespace TechFlurry.BusinessSite.Models
{
    public class BlogModel : DocumentModel
    {
        public BlogModel()
        {
            Paragraphs = new List<Paragraph>();
            Comments = new List<Comment>();
            Tags = new List<string>();
        }
        public string Heading { get; set; }
        public string ShortTitle { get; set; }
        public DateTime DatePosted { get; set; }
        public string Category { get; set; }
        public string BlogType { get; set; }
        public List<Paragraph> Paragraphs { get; set; }
        public List<string> Tags { get; set; }
        public Author Author { get; set; }
        public List<Comment> Comments { get; set; }

    }
    public class Paragraph
    {
        public string Content { get; set; }
        public string Type { get; set; }
    }
    public class Author
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Bio { get; set; }
    }
    public class Comment
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime PostedTime { get; set; }
    }
}
