IF NOT EXISTS (SELECT 1 FROM [dbo].[Persons] WHERE [Name] = 'PersonTest')
BEGIN
	INSERT INTO [dbo].[Persons]([SSN],[Name],[Born],[State],[File],[CreateDate],[UpdateDate])
	VALUES(2879564,'PersonTest',GETDATE(), 1,NULL,GETDATE(), NULL)
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE [Email] = 'testUser@gmail.com')
BEGIN
	INSERT INTO [dbo].[Users]([Person],[State],[Email],[Password])
	VALUES(1,1, 'testUser@gmail.com', '9876543210')
END
GO