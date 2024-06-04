CREATE OR ALTER PROCEDURE [SP_AddOrder]
(
	@CustomerId VARCHAR(50),
	@StudyGuideId INT,
	@IsCompleted BIT
)
AS
BEGIN
	INSERT INTO Orders(CustomerId,StudyGuideId,IsCompleted)
	VALUES(@CustomerId,@StudyGuideId,@IsCompleted)
END