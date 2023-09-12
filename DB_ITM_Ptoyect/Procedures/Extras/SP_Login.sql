CREATE PROCEDURE [dbo].[SP_Login]
	   @Email NVARCHAR(50),
       @Password NVARCHAR (50),
	   @Result INT OUT
AS
BEGIN	
	BEGIN TRY
		BEGIN TRAN tr

		IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] 
					   WHERE [Email] = @Email AND [Password] = @Password)
		BEGIN 		
			ROLLBACK TRAN tr
			SET @Result = -1;	

			SELECT TOP 0 U.[Id]
			  ,U.[Person]
			  ,P.[SSN]
			  ,P.[Name]
			  ,U.[State]
			  ,U.[Email] 
			  ,U.[Password] 
			FROM [dbo].[Users] U
				INNER JOIN [dbo].[Persons] P ON P.[Id] = U.[Person]
			RETURN;	
		END

	   SELECT U.[Id]
			  ,U.[Person]
			  ,P.[SSN]
			  ,P.[Name]
			  ,U.[State]
			  ,U.[Email] 
			  ,U.[Password] 
		FROM [dbo].[Users] U
			INNER JOIN [dbo].[Persons] P ON P.[Id] = U.[Person]
	    WHERE U.[Email] = @Email AND U.[Password] = @Password;
  
		EXEC [dbo].[SP_ADD_Audits] NULL, 'LOGIN', 'Users', NULL;
		
		SET @Result = 1;
	
		COMMIT TRAN tr
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN tr

		DECLARE @Error NVARCHAR(200);
		SET  @Error = ERROR_MESSAGE();
		
		EXEC [dbo].[SP_ADD_Audits] NULL, @Error, '[dbo].[SP_Login]', NULL;
		
		SELECT @Error;
		
		SET @Result = 0;
	END CATCH	
END
GO