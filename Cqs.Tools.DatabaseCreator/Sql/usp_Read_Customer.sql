
CREATE PROCEDURE [dbo].[usp_Read_Customer]
	@LastName NVARCHAR(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	  SELECT [Id]
	  ,[FirstName]
	  ,[LastName]
	  ,[IsActive]
	  ,[EmailAddress]
  FROM [dbo].[Customer]
  WHERE LastName LIKE @LastName  + '%' 

END
