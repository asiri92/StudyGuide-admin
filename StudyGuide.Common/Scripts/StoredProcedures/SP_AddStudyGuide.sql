CREATE OR ALTER PROCEDURE [SP_AddStudyGuide]
(
	@StudyGuideId INT,
	@StudyGuideName VARCHAR(255),
	@Price DECIMAL(10,2)
)
AS
BEGIN
	INSERT INTO StudyGuides(StudyGuideId,StudyGuideName,Price)
	VALUES(@StudyGuideId,@StudyGuideName,@Price)
END