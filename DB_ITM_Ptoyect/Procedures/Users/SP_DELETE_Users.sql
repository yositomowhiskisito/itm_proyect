CREATE PROCEDURE [dbo].[SP_DELETE_Users]
       @Id INT,
	   @Result int OUTPUT
AS
BEGIN	
	BEGIN TRY
		BEGIN TRAN tr
		IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE [Id] = @Id)
		BEGIN 		
			ROLLBACK TRAN tr
			SET @Result = -1;	
			RETURN;	
		END
	
		DELETE 
		FROM [dbo].[Users] 
		WHERE [Id] = @Id;     
  
		EXEC [dbo].[SP_ADD_Audits] @Id, 'DELETE', 'Users', NULL;
		
		SET @Result = 1;
		COMMIT TRAN tr
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN tr
		DECLARE @Error NVARCHAR(200);
		SET  @Error = ERROR_MESSAGE();
		
		EXEC [dbo].[SP_ADD_Audits] NULL, @Error, 'SP_DELETE_Users', NULL;
		
		SELECT @Error;
		
		SET @Result = 0;
	END CATCH	
END
GO
