CREATE PROCEDURE [dbo].[SP_GET_Users]
       @User INT = NULL,
	   @Result INT OUT
AS
BEGIN	
	BEGIN TRY
		SELECT U.[Id]
			  ,U.[Person]
			  ,P.[SSN]
			  ,P.[Name]
			  ,U.[State]
			  ,U.[Email]
			  ,'' [Password]
		FROM [dbo].[Users] U
			INNER JOIN [dbo].[Persons] P ON P.[Id] = U.[Person];

		SET @Result = 1;
	END TRY
	BEGIN CATCH
		DECLARE @Error NVARCHAR(200);
		SET  @Error = ERROR_MESSAGE();
		
		EXEC [dbo].[SP_ADD_Audits] NULL, @Error, 'SP_GET_Users', @User, 0;
		
		SELECT @Error;
		
		SET @Result = 0;
	END CATCH	
END
GO