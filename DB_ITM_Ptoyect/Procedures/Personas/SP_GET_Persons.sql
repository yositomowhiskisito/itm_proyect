CREATE PROCEDURE [dbo].[SP_GET_Persons]
	@Result INT OUT
AS
BEGIN	
	BEGIN TRY
		SELECT TOP 100 P.[Id],
					P.[SSN],
					P.[Name],
					P.[Born],
					P.[State],
					P.[File],
					P.[CreateDate]
		FROM [dbo].[Persons] P
		ORDER BY P.[Id] DESC;

		SET @Result = 1;
	END TRY
	BEGIN CATCH
		DECLARE @Error NVARCHAR(200);
		SET  @Error = ERROR_MESSAGE();
		
		EXEC [dbo].[SP_ADD_Audits]  NULL, @Error, 'SP_GET_Persons',  NULL;

		SELECT TOP 0 [Id],
					[SSN],
					[Name],
					[Born],
					[State],
					[File],
					[CreateDate]
		FROM [dbo].[Persons];
		
		SET @Result = 0;
	END CATCH	
END
GO


