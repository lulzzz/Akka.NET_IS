USE master;
GO
IF EXISTS(SELECT * FROM SYS.DATABASES WHERE name='tradedb')
BEGIN
	DROP DATABASE tradedb;
END
CREATE DATABASE tradedb;
USE tradedb;
GO
CREATE TABLE [dbo].[Positions](
	[AccountId] [int] NOT NULL,

	[Instrument] [nchar](16) NULL,
	[Lot] [float] NULL,
	[LotNumber] [float] NULL,
 CONSTRAINT [PK_Positions] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE PROCEDURE [dbo].[sp_InsertPosition]
@AccountId int,
@Instrument nchar(16),
@Lot float,
@LotNumber float
AS
INSERT INTO Positions (AccountId, Instrument, Lot, LotNumber)
VALUES (@AccountId, @Instrument, @Lot, @LotNumber)
GO