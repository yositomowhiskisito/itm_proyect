CREATE PROCEDURE [dbo].[SP_UPDATE_Persons]
	@Id INT,
	@SSN INT,
	@Name NVARCHAR(200),
	@Born DATETIME,
	@State BIT,
	@File VARBINARY(MAX),
	@Result int OUTPUT
AS
BEGIN	
	BEGIN TRY		
		BEGIN TRAN tr

		IF NOT EXISTS (SELECT 1 FROM [dbo].[Persons] WHERE [Id] = @Id)
		BEGIN
			ROLLBACK TRAN tr 		
			SET @Result = -1;	
			RETURN;	
		END

		UPDATE [dbo].Persons 
		SET [SSN] = @SSN,
			[Name] = @Name,
			[Born] = @Born,
			[State] = @State,
			[File] = @File,
			[UpdateDate] = GETDATE()
		WHERE [Id] = @Id;
      
		EXEC [dbo].[SP_ADD_Audits] @Id, 'UPDATE', 'Persons', NULL
		SET @Result = 1;

		COMMIT TRAN tr
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN tr
		DECLARE @Error NVARCHAR(200);
		SET  @Error = ERROR_MESSAGE();
		
		EXEC [dbo].[SP_ADD_Audits]  NULL, @Error, 'SP_UPDATE_Persons',  NULL;
		SET @Result = 0;
	END CATCH	
END
GO
