﻿CREATE TABLE [dbo].[OrderLines]
(
	[OrderLineID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [OrderID] INT FOREIGN KEY REFERENCES Orders(OrderID) NOT NULL, 
    [ProductID] INT FOREIGN KEY REFERENCES Product(ProductID) NOT NULL, 
    [QTY] INT NOT NULL
)
