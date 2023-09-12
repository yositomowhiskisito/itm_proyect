CREATE PROCEDURE [dbo].[SP_DELETE_Persons]
	@Id INT,
	@Result int OUTPUT
AS
BEGIN	
	BEGIN TRY		
		BEGIN TRAN tr
	
		DELETE 
		FROM [dbo].[Persons] 
		WHERE [Id] = @Id;     
  
		EXEC [dbo].[SP_ADD_Audits] @Id, 'DELETE', 'Persons', NULL
		SET @Result = 1;

		COMMIT TRAN tr
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN tr
		DECLARE @Error NVARCHAR(200);
		SET  @Error = ERROR_MESSAGE();
		
		EXEC [dbo].[SP_ADD_Audits]  NULL, @Error, 'SP_DELETE_Persons',  NULL;
		SET @Result = 0;
	END CATCH	
END
GO