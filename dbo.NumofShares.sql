CREATE TABLE [dbo].[NumofShares]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [numCommonShares] INT NOT NULL DEFAULT 1000000, 
    [numPreferredShares] INT NOT NULL DEFAULT 100000
)
