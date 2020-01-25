CREATE TABLE [dbo].[Product]
(
	[ProductID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [SKU] NVARCHAR(50) NOT NULL, 
    [ProductDescription] NVARCHAR(50) NOT NULL
)
