CREATE TABLE [dbo].[File]
(
	[FileID] INT NOT NULL PRIMARY KEY, 
    [Username] NVARCHAR(50) NULL, 
    [Data] NVARCHAR(MAX) NOT NULL, 
    [Preview] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_File_User] FOREIGN KEY ([Username]) REFERENCES [User]([Username])
)
