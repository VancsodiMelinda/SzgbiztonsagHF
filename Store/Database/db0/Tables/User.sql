CREATE TABLE [dbo].[User]
(
	[Username] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [PasswordHash] NVARCHAR(MAX) NOT NULL, 
    [Role] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [CK_User_Role] CHECK (Role IN ('Admin', 'User'))
)
