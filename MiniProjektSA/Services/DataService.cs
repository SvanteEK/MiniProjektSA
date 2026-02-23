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
    

    //public List<Book> GetBooks() {
    //    return db.Books.Include(b => b.Author).ToList();
    //}
//
    //public Book GetBook(int id) {
    //    return db.Books.Include(b => b.Author).FirstOrDefault(b => b.BookId == id);
    //}
//
    //public List<Author> GetAuthors() {
    //    return db.Authors.ToList();
    //}
//
    //public Author GetAuthor(int id) {
    //    return db.Authors.Include(a => a.Books).FirstOrDefault(a => a.AuthorId == id);
    //}
//
    //public string CreateBook(string title, int authorId) {
    //    Author author = db.Authors.FirstOrDefault(a => a.AuthorId == authorId);
    //    db.Books.Add(new Book { Title = title, Author = author });
    //    db.SaveChanges();
    //    return "Book created";
    //}

}