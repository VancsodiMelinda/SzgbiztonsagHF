﻿CREATE TABLE [dbo].[User]
(
	[UserID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [PasswordHash] NVARCHAR(50) NOT NULL, 
    [Role] BIT NOT NULL, 
    [Username] NVARCHAR(50) NOT NULL
)
