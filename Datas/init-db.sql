
/*******************************************************************************
   Drop database if it exists
********************************************************************************/
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'FootStatDb')
BEGIN
	ALTER DATABASE [FootStatDb] SET OFFLINE WITH ROLLBACK IMMEDIATE;
	ALTER DATABASE [FootStatDb] SET ONLINE;
	DROP DATABASE [FootStatDb];
END

GO

/*******************************************************************************
   Create database
********************************************************************************/
CREATE DATABASE [FootStatDb];
GO

USE [FootStatDb];
GO

/*******************************************************************************
   Create Tables
********************************************************************************/
CREATE TABLE Competition (
    id INT IDENTITY(1,1) NOT NULL, 
    name varchar(100), 
	year int,
	country varchar(50),
	CONSTRAINT [PK_Competition] PRIMARY KEY CLUSTERED ([id])
);
GO

CREATE TABLE Team (
    id INT IDENTITY(1,1) NOT NULL, 
	name varchar(100),
	CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED ([id])
);
GO

CREATE TABLE CompetitionTeam (
	idCompetition INT NOT NULL,
	idTeam INT NOT NULL
);
GO

CREATE TABLE Fixture (
	id INT IDENTITY(1,1) NOT NULL,
	idCompetition INT NOT NULL,
	idTeamHome INT NOT NULL,
	idTeamAway INT NOT NULL,
	matchDay INT,
	matchDate DATE,
	goalsHome INT,
	goalsAway INT
);
GO

/*******************************************************************************
   Create Foreign Keys
********************************************************************************/
ALTER TABLE CompetitionTeam ADD CONSTRAINT [FK_CompetitionId]
    FOREIGN KEY ([idCompetition]) REFERENCES Competition ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE CompetitionTeam ADD CONSTRAINT [FK_TeamId]
    FOREIGN KEY ([idTeam]) REFERENCES Team ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE Fixture ADD CONSTRAINT [FK_FixtureCompetitionId]
    FOREIGN KEY ([idCompetition]) REFERENCES Competition ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE Fixture ADD CONSTRAINT [FK_TeamHomeId]
    FOREIGN KEY ([idTeamHome]) REFERENCES Team ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE Fixture ADD CONSTRAINT [FK_TeamAwayId]
    FOREIGN KEY ([idTeamAway]) REFERENCES Team ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

/*******************************************************************************
   Populate Tables
********************************************************************************/
INSERT INTO Competition (name, year, country) VALUES ('Ligue 1 1993/1994', 1994, 'France')
INSERT INTO Competition (name, year, country) VALUES ('Ligue 1 1994/1995', 1995, 'France')
