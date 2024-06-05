CREATE OR ALTER PROCEDURE [SP_GetStudyGuides] AS
BEGIN
	SELECT SG.StudyGuideId,
		   SG.StudyGuideName,
		   SG.Price
	FROM StudyGuides SG
END