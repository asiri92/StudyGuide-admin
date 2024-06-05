CREATE OR ALTER PROCEDURE [SP_FullFillOrder]
(
	@CustomerId VARCHAR(50),
	@StudyGuideId INT
	
)
AS
BEGIN
	UPDATE Orders
	SET IsCompleted = 1,
		OrderCompletedDate = GETDATE()
	WHERE CustomerId = @CustomerId
	AND StudyGuideId = @StudyGuideId
END