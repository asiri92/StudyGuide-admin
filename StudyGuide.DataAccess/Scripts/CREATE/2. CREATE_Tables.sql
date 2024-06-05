USE [StudyGuideOrder]
GO

IF NOT EXISTS(SELECT 'X' FROM sys.objects sys where sys.name = 'Customers')
BEGIN
	CREATE TABLE Customers(
		CustomerId VARCHAR(50) NOT NULL,
		CustomerName VARCHAR(100),
		CustomerEmail VARCHAR(100),
		PRIMARY KEY(CustomerId)
	)
END

IF NOT EXISTS(SELECT 'X' FROM sys.objects sys where sys.name = 'StudyGuides')
BEGIN
	CREATE TABLE StudyGuides(
		StudyGuideId INT NOT NULL,
		StudyGuideName VARCHAR(255),
		Price DECIMAL(10, 2),
		PRIMARY KEY(StudyGuideId)
	)
END

IF NOT EXISTS(SELECT 'X' FROM sys.objects sys where sys.name = 'Orders')
BEGIN
	CREATE TABLE Orders(
		OrderId INT Identity(1,1) NOT NULL,
		CustomerId VARCHAR(50),
		StudyGuideId INT,
		IsCompleted BIT DEFAULT 0,
		OrderCompletedDate DateTime NULL,
		PRIMARY KEY(OrderId),
		CONSTRAINT FK_Customers FOREIGN KEY (CustomerId)
		REFERENCES Customers(CustomerId),
		CONSTRAINT FK_StudyGuides FOREIGN KEY (StudyGuideId)
		REFERENCES StudyGuides(StudyGuideId)
	)
END

--DROP Table Customers
--DROP Table Orders
--DROP Table StudyGuides