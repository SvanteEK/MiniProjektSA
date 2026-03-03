namespace MiniProjektSA.Models;

public class PostModel
{
    public PostModel() { }

    public PostModel(string title, string content, DateTime publishDate, int? voteScore, int? motherPostId)
    {
        Title = title;
        Content = content;
        PublishDate = publishDate;
        Votescore = voteScore;
        UserId = 1;
        MotherPostId = motherPostId;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishDate { get; set; }
    public int? Votescore { get; set; }
    public int? MotherPostId { get; set; }
    public int? UserId { get; set; }
}