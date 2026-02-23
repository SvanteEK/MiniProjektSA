namespace MiniProjektSA.Models;

public class PostModel
{
    
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishDate { get; set; }
    
    public int? Votescore { get; set; }
    
    public UserModel User { get; set; }
}