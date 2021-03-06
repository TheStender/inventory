﻿CREATE TABLE [dbo].[Inventory]
(
	[InventoryID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [ProductID] INT FOREIGN KEY REFERENCES Product(ProductID) NOT NULL, 
    [BinID] INT FOREIGN KEY REFERENCES Bins(BinID) NOT NULL, 
    [QTY] INT NOT NULL,
	CONSTRAINT productAndBinUniqueConstraint UNIQUE (ProductId, BinId)
)
