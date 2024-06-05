CREATE OR ALTER PROCEDURE [SP_FullFillOrder]
(
	@OrderId INT,
	@IsCompleted BIT
)
AS
BEGIN
	UPDATE Orders
	SET IsCompleted = @IsCompleted,
		OrderCompletedDate = GETDATE()
	WHERE OrderId = @OrderId
END