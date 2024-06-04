CREATE OR ALTER PROCEDURE [SP_GetOrders] AS
BEGIN
	SELECT O.OrderId,
		   O.CustomerId,
		   O.StudyGuideId,
		   O.IsCompleted,
		   O.OrderCompletedDate
	FROM Orders O
END