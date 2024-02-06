namespace imdb.Models;

public class UserFavoriteMovie {
    int Id { get; set; }
    int UserId { get; set; }
    int MovieId { get; set; }
    User User { get; set; }
    Movie Movie { get; set; }
}