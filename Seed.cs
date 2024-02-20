using MovieReviewApi.Data;
using MovieReviewApi.Models;

namespace MovieReviewApi
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            if (!dataContext.UserFavoriteMovies.Any())
            {
                var favorites = new List<UserFavoriteMovie>()
                {
                    new UserFavoriteMovie()
                    {
                        User = new User()
                        {
                            Name = "Raiyan",
                            Email = "raiyan.hossain@test.com"
                        },
                        Movie = new Movie()
                        {
                            Name = "End of Evangelion"
                        }
                    },
                    new UserFavoriteMovie()
                    {
                        User = new User()
                        {
                            Name = "Shinji",
                            Email = "baka@test.com"
                        },
                        Movie = new Movie()
                        {
                            Name = "Do androids dream of elecric sheep"
                        }
                    }
                };
                dataContext.UserFavoriteMovies.AddRange(favorites);
                dataContext.SaveChanges();
            }
        }
    }
}