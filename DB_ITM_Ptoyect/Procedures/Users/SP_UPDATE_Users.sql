CREATE PROCEDURE [dbo].[SP_UPDATE_Users]
    @Id INT,
	@Person INT,
    @State BIT,
	@Email NVARCHAR(50),
	@Password NVARCHAR(50),
	@Result int OUTPUT
AS
BEGIN	
	BEGIN TRY
		BEGIN TRAN tr
		IF EXISTS (SELECT 1 FROM [dbo].[Users] 
				   WHERE [Person] = @Person AND [Id] <> @Id)
		BEGIN 		
			ROLLBACK TRAN tr
			SET @Result = -1;	
			RETURN;	
		END
		
		IF NOT EXISTS (SELECT 1 FROM [dbo].[Persons] WHERE [Id] = @Person)
		BEGIN 		
			ROLLBACK TRAN tr
			SET @Result = -2;	
			RETURN;	
		END
	
		IF (@Password IS NULL OR @Password = '')
		BEGIN
			UPDATE [dbo].[Users] 
			SET [Person] = @Person,
				[State] = @State,
				[Email] = @Email
			WHERE [Id] = @Id;
		END ELSE BEGIN
			UPDATE [dbo].[Users] 
			SET [Person] = @Person,
				[State] = @State,
				[Email] = @Email,
				[Password] = @Password
			WHERE [Id] = @Id;
		END
  
		EXEC [dbo].[SP_ADD_Audits] @Id, 'UPDATE', 'Users', NULL;
		
		SET @Result = 1;
		COMMIT TRAN tr
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN tr
		DECLARE @Error NVARCHAR(200);
		SET  @Error = ERROR_MESSAGE();
		
		EXEC [dbo].[SP_ADD_Audits] NULL, @Error, '[SP_UPDATE_Users', NULL;
		
		SELECT @Error;
		
		SET @Result = 0;
	END CATCH	
END
GO