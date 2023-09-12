﻿CREATE TABLE [Users]
(
	[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Person] INT REFERENCES [dbo].[Persons]([Id]) NOT NULL,  
	[State] BIT NOT NULL,
	[Email] NVARCHAR(50) NULL,
	[Password] NVARCHAR(50) NULL,
);
