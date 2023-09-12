CREATE PROCEDURE [dbo].[SP_ADD_Audits]
	 @Reference INT = NULL,
     @Description NVARCHAR (500),
     @Table NVARCHAR (200),
     @User INT = NULL
AS
BEGIN	
	BEGIN TRY	
		INSERT INTO [dbo].[Audits] 
			( [Table],[Description],[Reference], [User], [Date])
		VALUES (@Table, @Description, @Reference,  @User, GETDATE());
	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE();
	END CATCH	
END
GO