CREATE PROCEDURE [dbo].[SP_ADD_Persons]
	@SSN INT,
	@Name NVARCHAR(200),
	@Born DATETIME,
	@State BIT,
	@File VARBINARY(MAX),
	@Result int OUTPUT
AS
	DECLARE @Id INT, @Current BIT;

	BEGIN TRANSACTION tr
	BEGIN TRY

		IF EXISTS (SELECT 1 FROM [dbo].[Persons] WHERE [SSN] = @SSN)
		BEGIN
			ROLLBACK TRAN tr 		
			SET @Result = -1;	
			RETURN;	
		END
		INSERT INTO [dbo].[Persons]
			([SSN],
			 [Name],
			 [Born],
			 [State],
			 [File],
			 [CreateDate])
		VALUES(@SSN,
			 @Name,
			 @Born,
			 @State,
			 @File,
			 GETDATE());

		SET @Id = SCOPE_IDENTITY();
		
		EXEC [dbo].[SP_ADD_Audits] @Id, 'NEW', 'Persons', NULL
		SET @Result = @Id

		COMMIT TRANSACTION tr
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION tr
		DECLARE @Error NVARCHAR(MAX)
		SET @Error = ERROR_MESSAGE();
		
		EXEC [dbo].[SP_ADD_Audits]  NULL, @Error, 'SP_ADD_Persons',  NULL;
		SET @Result = 0
	END CATCH
RETURN 0