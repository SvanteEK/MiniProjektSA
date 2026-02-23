namespace MiniProjektSA.Models;

public class PostModel
{
    public PostModel(string title, string content, DateTime publishDate)
    {
        Title = title;
        Content = content;
        PublishDate = publishDate;
    }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishDate { get; set; }
    
    public int? Votescore { get; set; }
    
    public int? MotherPostId { get; set; }
    
    public int UserId { get; set; }
}