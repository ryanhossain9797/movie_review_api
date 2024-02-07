CREATE TABLE [dbo].[Movies] (
    [Id]                                               INT IDENTITY (1, 1)     NOT NULL,
    [Name]                                             NVARCHAR (400)          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)