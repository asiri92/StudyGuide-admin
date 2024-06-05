CREATE OR ALTER PROCEDURE [SP_AddCustomer]
(
	@CustomerId VARCHAR(50),
	@CustomerName VARCHAR(100),
	@CustomerEmail VARCHAR(100)
)
AS
BEGIN
	INSERT INTO Customers(CustomerId,CustomerName,CustomerEmail)
	VALUES(@CustomerId,@CustomerName,@CustomerEmail)
END