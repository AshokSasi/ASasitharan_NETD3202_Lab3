CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [name] NVARCHAR(MAX) NOT NULL, 
    [shares] INT NOT NULL, 
    [datePurchased] DATE NOT NULL, 
    [shareType] NVARCHAR(MAX) NOT NULL
)
