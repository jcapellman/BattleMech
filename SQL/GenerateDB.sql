CREATE TABLE [dbo].[Levels](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Modified] [datetimeoffset](7) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Active] [bit] NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[UserAuthorID] [int] NOT NULL,
	[NumDownloads] [int] NOT NULL,
	[Description] [varchar](500) NOT NULL,
	[Rating] [int] NOT NULL,
 CONSTRAINT [PK_Levels] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PlayerCharacterGames]    Script Date: 9/28/2015 9:51:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayerCharacterGames](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Modified] [datetimeoffset](7) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Active] [bit] NOT NULL,
	[PlayerCharacterID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[LevelID] [int] NOT NULL,
	[ShotsFired] [int] NOT NULL,
	[EnemiesDestroyed] [int] NOT NULL,
	[TotalEnemiesFought] [int] NOT NULL,
	[ExperienceGarnered] [int] NOT NULL,
	[TimesDied] [int] NOT NULL,
 CONSTRAINT [PK_PlayerCharacterGames] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PlayerCharacters]    Script Date: 9/28/2015 9:51:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PlayerCharacters](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Modified] [datetimeoffset](7) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Active] [bit] NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[ClassTypeID] [int] NOT NULL,
	[LevelNumber] [int] NOT NULL,
	[Experience] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_PlayerCharacters] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/28/2015 9:51:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Modified] [datetimeoffset](7) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Active] [bit] NOT NULL,
	[FirstName] [varchar](255) NOT NULL,
	[LastName] [varchar](255) NOT NULL,
	[EmailAddress] [varchar](255) NOT NULL,
	[Password] [uniqueidentifier] NOT NULL,
	[Activated] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[PlayerCharacterGames] ADD  CONSTRAINT [DF_PlayerCharacterGames_ShotsFired]  DEFAULT ((0)) FOR [ShotsFired]
GO
ALTER TABLE [dbo].[PlayerCharacterGames] ADD  CONSTRAINT [DF_PlayerCharacterGames_EnemiesDestroyed]  DEFAULT ((0)) FOR [EnemiesDestroyed]
GO
ALTER TABLE [dbo].[PlayerCharacterGames] ADD  CONSTRAINT [DF_PlayerCharacterGames_TotalEnemiesFought]  DEFAULT ((0)) FOR [TotalEnemiesFought]
GO
ALTER TABLE [dbo].[PlayerCharacterGames] ADD  CONSTRAINT [DF_PlayerCharacterGames_ExperienceGarnered]  DEFAULT ((0)) FOR [ExperienceGarnered]
GO
ALTER TABLE [dbo].[PlayerCharacterGames] ADD  CONSTRAINT [DF_PlayerCharacterGames_TimesDied]  DEFAULT ((0)) FOR [TimesDied]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Activated]  DEFAULT ((0)) FOR [Activated]
GO
ALTER TABLE [dbo].[Levels]  WITH CHECK ADD  CONSTRAINT [FK_Levels_Users] FOREIGN KEY([UserAuthorID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Levels] CHECK CONSTRAINT [FK_Levels_Users]
GO
ALTER TABLE [dbo].[PlayerCharacterGames]  WITH CHECK ADD  CONSTRAINT [FK_PlayerCharacterGames_Levels] FOREIGN KEY([LevelID])
REFERENCES [dbo].[Levels] ([ID])
GO
ALTER TABLE [dbo].[PlayerCharacterGames] CHECK CONSTRAINT [FK_PlayerCharacterGames_Levels]
GO
ALTER TABLE [dbo].[PlayerCharacterGames]  WITH CHECK ADD  CONSTRAINT [FK_PlayerCharacterGames_PlayerCharacters] FOREIGN KEY([PlayerCharacterID])
REFERENCES [dbo].[PlayerCharacters] ([ID])
GO
ALTER TABLE [dbo].[PlayerCharacterGames] CHECK CONSTRAINT [FK_PlayerCharacterGames_PlayerCharacters]
GO
ALTER TABLE [dbo].[PlayerCharacterGames]  WITH CHECK ADD  CONSTRAINT [FK_PlayerCharacterGames_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[PlayerCharacterGames] CHECK CONSTRAINT [FK_PlayerCharacterGames_Users]
GO
ALTER TABLE [dbo].[PlayerCharacters]  WITH CHECK ADD  CONSTRAINT [FK_PlayerCharacters_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[PlayerCharacters] CHECK CONSTRAINT [FK_PlayerCharacters_Users]