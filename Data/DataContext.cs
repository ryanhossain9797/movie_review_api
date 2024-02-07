using imdb.Models;
using Microsoft.EntityFrameworkCore;

namespace imdb.Data;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<UserFavoriteMovie> UserFavoriteMovies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserFavoriteMovie>()
        .HasKey(ufm => new { ufm.MovieId, ufm.UserId });

        modelBuilder.Entity<UserFavoriteMovie>()
        .HasOne(ufm => ufm.User)
        .WithMany(u => u.FavoritedMovies)
        .HasForeignKey(ufm => ufm.UserId);

        modelBuilder.Entity<UserFavoriteMovie>()
        .HasOne(ufm => ufm.Movie)
        .WithMany(m => m.FavoritedByUsers)
        .HasForeignKey(ufm => ufm.MovieId);
    }
}
