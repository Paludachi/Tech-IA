USE [C:\USERS\MKIRKPATRICK\SOURCE\REPOS\PROJECT\PROJECT\DATABASE1.MDF]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[AllItems]

SELECT	@return_value as 'Return Value'

GO
