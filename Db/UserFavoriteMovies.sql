CREATE TABLE [dbo].[UserFavoriteMovies] (
    [UserId]                                              INT     NOT NULL,
    [MovieId]                                             INT     NOT NULL,
    PRIMARY KEY CLUSTERED (UserId, MovieId),
    CONSTRAINT [FK_UserFavoriteMovies_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]),
    CONSTRAINT [FK_UserFavoriteMovies_Movies] FOREIGN KEY ([MovieId]) REFERENCES [dbo].[Movies] ([Id])
)