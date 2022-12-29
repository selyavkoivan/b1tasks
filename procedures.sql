CREATE PROCEDURE CalculateSum
AS
	SELECT SUM(CAST(RandomNumber AS BIGINT)) AS sum FROM RandomObjects;
GO

CREATE PROCEDURE CalculateMedian
AS
	SELECT(
	(SELECT MAX(RandomDouble) FROM (SELECT TOP 50 PERCENT RandomDouble FROM RandomObjects ORDER BY RandomDouble) AS BottomHalf)
	+
	(SELECT MIN(RandomDouble) FROM (SELECT TOP 50 PERCENT RandomDouble FROM RandomObjects ORDER BY RandomDouble DESC) AS TopHalf)
	) / 2 AS Median
GO

exec CalculateSum
exec CalculateMedian