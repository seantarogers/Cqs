
CREATE PROCEDURE [dbo].[usp_Read_Order]
	-- Add the parameters for the stored procedure here
	@CustomerId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Id]
      ,[CustomerId]
      ,[ProductName]
      ,[PlacedOn]
      ,[DispatchedOn]
  FROM [dbo].[Order]
Where CustomerId = @CustomerId

END

