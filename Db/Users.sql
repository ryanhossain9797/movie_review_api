CREATE TABLE [dbo].[Users] (
    [Id]                                               INT IDENTITY (1, 1)     NOT NULL,
    [Name]                                             NVARCHAR (400)          NOT NULL,
    [Email]                                            NVARCHAR (400)          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_User_Email]
    ON [dbo].[Users]([Email] ASC);
GO