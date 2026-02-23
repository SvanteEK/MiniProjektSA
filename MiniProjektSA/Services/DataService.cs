using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using MiniProjektSA.Data;
using MiniProjektSA.Models;


namespace MiniProjektSA.Services;

public class DataService
{
    private MainContext db { get; }

    public DataService(MainContext db) {
        this.db = db;
    }
    
    public void SeedData()
    {
        
        db.Database.Migrate();

        // USERS
        if (db.Users.FirstOrDefault() == null)
        {
            var user1 = new UserModel("John");
            var user2 = new UserModel("Jane");

            db.Users.AddRange(user1, user2);
            db.SaveChanges();
        } else { return; }

        // POSTS
        if  (db.Posts.FirstOrDefault() == null)
        {
            var user = db.Users.First();

            db.Posts.Add(new PostModel(
                "Post1",
                "Content1",
                DateTime.Now)
            {
                UserId = user.Id
            });

            db.SaveChanges();
        } else { return; }
    }
}


//Routes til blazor-app:
//GET:
//    /api/posts
//        /api/posts/{id}
//PUT:
//    /api/posts/{id}/upvote
//    /api/posts/{id}/downvote
//    /api/posts/{postid}/comments/{commentid}/upvote
//    /api/posts/{postid}/comments/{commentid}/downvote
//POST:
//    /api/posts
//        /api/posts/{id}/comments
