CREATE TABLE [dbo].[User]
(
	[Username] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [PasswordHash] NVARCHAR(50) NOT NULL, 
    [Role] BIT NOT NULL
)
