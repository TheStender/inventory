# Inventory Management System

## Installation Instructions
- Download the Git Repo/SQL files
- Created in Visual Studio 2017, use for best results
- Run the solution
- **Need to publish the SQL database?**

## How to Use
- Create products and bins first
- Add inventory using the products and bins
- Create an order for a customer
- Add order lines to the order

## Product Overview
- Inventory Management system uses a MVC Layer as the main UI layer, which connects to a Database layer to store the tables, connecting the two is a Data Layer
- Dapper is used for SQL mapping
- **What to expect when you look at the code**

## Open Questions
- Removing the inventory when it hits zero would not allow for adding inventory back in if the orders are deleted or edited. Removing the inventory could also create issues when adding inventory back in to the bins where it should go
- For the inventory table, an idea would be to add real inventory vs. reservered inventory. That way when an order for a customer is created the inventory is reserved, but since it has not been removed from the bin yet, the real inventory would still track that

## Next Steps
- SQL transactions could be added so that the inventory could be checked and updated in the same transaction which would allow multiple users to create orders at the same time 
- Unit Testing could be added
- Adding a maximum size for bins, this would allow bins that store smaller products to hold more, and bins that store larger products to hold less
- Users are not currently allowed to add product into a bin where it already exists. A feature could be added to add product and it would just add it to the existing bin, or with a bin size implemented it could add it to the bin until it's full, then prompt the user to create a new bin location for the product
- Currently when inventory is added back into a bin, it checks for the bin with the lowest amount of product. This could be updated to put the inventory back in the bin where it came from
- For testing purposes the Order ID auto generates based on the date and time it was submitted, but based on client needs this can be updated to allow manual input, or auto creation based on different parameters
