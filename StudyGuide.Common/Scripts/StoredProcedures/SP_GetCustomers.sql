CREATE OR ALTER PROCEDURE [SP_GetCustomers] AS
BEGIN
	SELECT C.CustomerId,
		   C.CustomerName,
		   C.CustomerEmail
	FROM Customers C
END