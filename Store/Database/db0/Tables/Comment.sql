CREATE TABLE [dbo].[Comment]
(
	[CommentID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Username] NVARCHAR(50) NOT NULL, 
    [Comment] NVARCHAR(MAX) NOT NULL, 
    [FileID] INT NOT NULL, 
    [PostTime] DATETIME NOT NULL, 
    CONSTRAINT [FK_Comment_User] FOREIGN KEY ([Username]) REFERENCES [User]([Username]), 
    CONSTRAINT [FK_Comment_File] FOREIGN KEY ([FileID]) REFERENCES [File]([FileID])
)

