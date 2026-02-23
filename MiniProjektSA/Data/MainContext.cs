using Microsoft.EntityFrameworkCore;
using MiniProjektSA.Models;

namespace MiniProjektSA.Data;

public class MainContext : DbContext
{
    public DbSet<PostModel> Posts { get; set; }
    public DbSet<UserModel> Users { get; set; }

    public MainContext(DbContextOptions<MainContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostModel>().ToTable("Posts");
        modelBuilder.Entity<UserModel>().ToTable("Users");
    }
}