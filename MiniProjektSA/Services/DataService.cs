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
         var post = db.Posts.Where(p => p.Id == commentid && p.MotherPostId == postid).FirstOrDefault();
         var comment = db.Posts.Where(p => p.Id == commentid).FirstOrDefault();
         if (comment.Votescore == null)
         {
             comment.Votescore = 1;
         }
         else
         {
             comment.Votescore += 1;
         }
     }
     
     public void DownvoteComment(int postid, int commentid)
     {
         var post = db.Posts.Where(p => p.Id == commentid && p.MotherPostId == postid).FirstOrDefault();
         var comment = db.Posts.Where(p => p.Id == commentid).FirstOrDefault();
         if (comment.Votescore == null)
         {
             comment.Votescore = -1;
         }
         else
         {
             comment.Votescore -= 1;
         }
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
