CREATE TABLE [dbo].[Comment]
(
	[CommentID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserID] INT NOT NULL, 
    [Comment] NVARCHAR(MAX) NOT NULL, 
    [FileID] INT NOT NULL, 
    CONSTRAINT [FK_Comment_User] FOREIGN KEY ([UserID]) REFERENCES [User]([UserID]), 
    CONSTRAINT [FK_Comment_File] FOREIGN KEY ([FileID]) REFERENCES [File]([FileID])
)
