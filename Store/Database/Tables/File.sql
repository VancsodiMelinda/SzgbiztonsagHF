﻿CREATE TABLE [dbo].[File]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Username] NVARCHAR(50) NULL, 
    [Data] NVARCHAR(MAX) NOT NULL, 
    [Preview] NVARCHAR(MAX) NOT NULL
)
