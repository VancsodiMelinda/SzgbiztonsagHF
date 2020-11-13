CREATE TABLE [dbo].[File]
(
	[FileID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Username] NVARCHAR(50) NULL, 
    [Data] NVARCHAR(MAX) NOT NULL, 
    [Preview] NVARCHAR(MAX) NOT NULL, 
    [Metadata] NVARCHAR(MAX) NOT NULL, 
    [FileName] NVARCHAR(MAX) NOT NULL, 
    [UploadTime] DATETIME NOT NULL, 
    [Length] FLOAT NOT NULL, 
    [Counter] INT NOT NULL, 
    CONSTRAINT [FK_File_User] FOREIGN KEY ([Username]) REFERENCES [User]([Username])
)
