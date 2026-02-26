using System.Net.Http;
using System.Net.Http.Json;
using MiniProjektSA.Models;

namespace MiniProjektSABlazor.Services;

public class PostService
{
   protected readonly HttpClient _httpClient;

   public PostService(HttpClient httpClient)
   {
      _httpClient = httpClient;
   }

   public async Task <List<PostModel>> GetAll()
   {
      var response = await _httpClient.GetFromJsonAsync<List<PostModel>>("api/posts");
      return response;
   }

   public async Task<PostModel?> Get(int id)
   {
      var response = await _httpClient.GetFromJsonAsync<PostModel>($"api/posts/{id}");
      return response;
   }

   public async Task Upvote(int id)
   {
      await _httpClient.PutAsync("api/posts/{id}/upvote", null);
   }

   public async Task Downvote(int id)
   {
      await _httpClient.PutAsync("api/posts/{id}/downvote", null);
   }

   public async Task UpvoteComment(int PostId, int CommentId)
   {
      await _httpClient.PutAsync($"api/posts/{PostId}/comments/{CommentId}/upvote", null);
   }
   
   public async Task DownvoteComment(int PostId, int CommentId)
   {
      await _httpClient.PutAsync($"api/posts/{PostId}/comments/{CommentId}/downvote", null);
   }

   public async Task CreatePost(PostModel post)
   {
      await _httpClient.PostAsJsonAsync("api/posts", post);
   }

   public async Task CreateComment(PostModel post)
   {
      await _httpClient.PostAsJsonAsync("api/posts/{PostId}/comments", post);
   }
}
