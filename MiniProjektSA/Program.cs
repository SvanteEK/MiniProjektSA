using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiniProjektSA.Data;
using MiniProjektSA.Models;
using MiniProjektSA.Services;

var builder = WebApplication.CreateBuilder(args);
// CORS
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowSomeStuff, policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Database
builder.Services.AddDbContext<MainContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));

// Services
builder.Services.AddScoped<DataService>();

var app = builder.Build();

// Opret database + seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MainContext>();
    context.Database.EnsureCreated();

    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    dataService.SeedData();
}

app.UseCors(AllowSomeStuff);

app.MapGet("/", () => "BACKEND KØRE!");


// GET alle posts
app.MapGet("/api/posts", (DataService service) =>
{
    var posts = service.GetPosts();

    if (posts == null)
    {
        return Results.NotFound("No posts found");
    }
    else
    {
        return Results.Ok(posts);
    }
});


// GET post by id
app.MapGet("/api/posts/{id}", (int id, DataService service) =>
{
    var post = service.GetPost(id);

    if (post == null)
    {
        return Results.NotFound("Post not found");
    }
    else
    {
        return Results.Ok(post);
    }
});


// UPVOTE post
app.MapPut("/api/posts/{id}/upvote", (int id, DataService service) =>
{
    service.Upvote(id);
    return Results.Ok();
});


// DOWNVOTE post
app.MapPut("/api/posts/{id}/downvote", (int id, DataService service) =>
{
    service.Downvote(id);
    return Results.Ok();
});


// UPVOTE comment
app.MapPut("/api/posts/{postid}/comments/{commentid}/upvote",
(int postid, int commentid, DataService service) =>
{
    service.UpvoteComment(postid, commentid);
    return Results.Ok();
});


// DOWNVOTE comment
app.MapPut("/api/posts/{postid}/comments/{commentid}/downvote",
(int postid, int commentid, DataService service) =>
{
    service.DownvoteComment(postid, commentid);
    return Results.Ok();
});


// CREATE post
app.MapPost("/api/posts", (PostModel post, DataService service) =>
{
    service.CreatePost(post);
    return Results.Ok(post);
});


// CREATE comment
app.MapPost("/api/posts/{id}/comments", (int id, PostModel comment, DataService service) =>
{
    service.CreatePostComment(comment);
    return Results.Ok(comment);
});

app.Run();