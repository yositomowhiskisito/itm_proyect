CREATE PROCEDURE [dbo].[SP_ADD_Users]
	@Person INT,
	@State BIT,
	@Email NVARCHAR(50)  = NULL,
	@Password NVARCHAR(50) =  NULL,
    @User INT = NULL,
	@Result int OUTPUT
AS
BEGIN	
	DECLARE @Id INT;
	
	BEGIN TRY
		BEGIN TRAN tr
		IF EXISTS (SELECT 1 FROM [dbo].[Users] 
				   WHERE [Person] = @Person)
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
	
		INSERT INTO [dbo].[Users] 
			([Person], [State],[Email],[Password])
		VALUES (@Person, @State, @Email, @Password);
      
		SET @Id = SCOPE_IDENTITY();
		
  
		EXEC [dbo].[SP_ADD_Audits] @Id, 'INSERT', 'Users', NULL
		
		SET @Result = 1;
		COMMIT TRAN tr
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN tr
		DECLARE @Error NVARCHAR(200);
		SET  @Error = ERROR_MESSAGE();
		
		EXEC [dbo].[SP_ADD_Audits] NULL, @Error, 'SP_ADD_Users',  NULL;
		
		SELECT @Error;
		
		SET @Result = 0;
	END CATCH	
END
GO