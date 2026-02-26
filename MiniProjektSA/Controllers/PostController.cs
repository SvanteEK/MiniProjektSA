using Microsoft.AspNetCore.Mvc;
using MiniProjektSA.Models;
using MiniProjektSA.Services;

namespace MiniProjektSA.Controllers;

[ApiController]
[Route("api/posts")]
public class PostController : ControllerBase
{
    private readonly DataService _service;

    public PostController(DataService service)
    {
        _service = service;
    }

    [HttpGet] // Finder alle posts
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = _service.GetPosts();
        if (posts == null)
        {
            return NotFound("No posts found");
        } return Ok(posts);
    }

    [HttpGet ("{id}")] // Finder post med id
    public async Task<IActionResult> GetPostById(int id)
    {
        var post = _service.GetPost(id);
        if (post == null)
        {
            return NotFound("Post not found");
        } return Ok(post);
    }
    
    [HttpPut ("{id}/upvote")] // +1 på votescore post
    public async Task<IActionResult> Upvote(int id)
    {
        _service.Upvote(id);
        return Ok();
    }
    
    [HttpPut ("{id}/downvote")] // -1 på votescore post
    public async Task<IActionResult> Downvote(int id)
    {
        _service.Downvote(id);
        return Ok();
    }

    [HttpPut ("{postid}/comments/{commentid}/upvote")]
    public async Task<IActionResult> UpvoteComment(int postid, int commentid)
    {
        _service.UpvoteComment(postid, commentid);
        return Ok();
    }

    [HttpPut("{postid}/comments/{commentid}/downvote}")]
    public async Task<IActionResult> DownvoteComment(int postid, int commentid)
    {
        _service.DownvoteComment(postid, commentid);
        return Ok();
    }
        
    
    [HttpPost] // anvendes til at oprette post
    public async Task<IActionResult> CreatePost(PostModel post)
    {
        _service.CreatePost(post);
        return Ok(post);
    }
    
    [HttpPost ("{id}/comments")] // anvendes til at oprette kommentar
    public async Task<IActionResult> CreatePostComment(PostModel comment)
    {
        _service.CreatePostComment(comment);
        return Ok(comment);
    }
}