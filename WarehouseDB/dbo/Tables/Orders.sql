CREATE TABLE [dbo].[Orders]
(
	[OrderID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [OrderNumber] NVARCHAR(50) NOT NULL, 
    [DateOrdered] DATETIME NOT NULL, 
    [CustomerName] NVARCHAR(50) NOT NULL, 
    [CustomerAddress] NVARCHAR(MAX) NOT NULL
)