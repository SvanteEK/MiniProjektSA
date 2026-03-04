using System.Runtime.InteropServices.ComTypes;
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
        // Indsæt nogle mock users
        if (db.Users.FirstOrDefault() == null)
        {
            var user1 = new UserModel("John");
            var user2 = new UserModel("Jane");
            var user3 = new UserModel("Bob");
            var user4 = new UserModel("Alice");
            var user5 = new UserModel("Eve");

            db.Users.AddRange(user1, user2, user3, user4, user5);
            db.SaveChanges();
        } else { return; }

        // POSTS
        if  (db.Posts.FirstOrDefault() == null) // hvis ingen posts i db, insæt posts
        {
            var user = db.Users.First();

            for (int i = 0; i < 5; i++)
            {
                var post = new PostModel(
                    $"Testpost {i}",
                    $"Test content {i}",
                    DateTime.Now,
                    null,
                    null)
                {
                    UserId = user.Id
                };

                db.Posts.Add(post);
                db.SaveChanges(); // <-- vigtigt! Nu får post.Id en værdi

                for (int p = 0; p < 5; p++)
                {
                    var comment = new PostModel(
                            $"Kommentar {p}",
                            $"Kommentar content {p}",
                            DateTime.Now,
                            null,
                            post.Id) // <-- brug RIGTIGT ID
                        {
                            UserId = user.Id
                        };

                    db.Posts.Add(comment);
                }

                db.SaveChanges();
            }
        }
    }
    // get all
     public List<PostModel> GetPosts()
     {
       return db.Posts.ToList();
     }

     public PostModel GetPost(int id)
     {
         return db.Posts.Where(p => p.Id == id).FirstOrDefault();
     }

     public void Upvote(int id)
     {
         var post = db.Posts.Where(p => p.Id == id).FirstOrDefault();
         if (post.Votescore == null)
         {
             post.Votescore = 1;
         }
         else
         {
             post.Votescore += 1;
         }
         db.SaveChanges();
     }
     public void Downvote(int id)
     {
         var post = db.Posts.Where(p => p.Id == id).FirstOrDefault();
         if (post.Votescore == null)
         {
             post.Votescore = -1;
         }
         else
         {
             post.Votescore -= 1;
         }
         db.SaveChanges();
     }
     
     public void UpvoteComment(int postid, int commentid)
     {
         var comment = db.Posts.Where(p => p.Id == commentid && p.MotherPostId == postid).FirstOrDefault();
         if (comment.Votescore == null)
         {
             comment.Votescore = 1;
         }
         else
         {
             comment.Votescore += 1;
         }
         db.SaveChanges();
     }
     
     public void DownvoteComment(int postid, int commentid)
     {
         var comment = db.Posts.Where(p => p.Id == commentid && p.MotherPostId == postid).FirstOrDefault();
         if (comment.Votescore == null)
         {
             comment.Votescore = -1;
         }
         else
         {
             comment.Votescore -= 1;
         }
         db.SaveChanges();
     }
     public void CreatePost(PostModel post)
     {
         db.Posts.Add(post);
         db.SaveChanges();
     }

     public void CreatePostComment(PostModel comment)
     {
         db.Posts.Add(comment);
         db.SaveChanges();
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
