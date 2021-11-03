Create Database PolyEarth_DB
GO

Use PolyEarth_DB
GO

/***************************************************************/
/***           Delete tables before creating                 ***/
/***************************************************************/

/* Table: dbo.Users */
if exists (select * from sysobjects 
  where id = object_id('dbo.Users') and sysstat & 0xf = 3)
  drop table dbo.Users
GO

if exists (select * from sysobjects 
  where id = object_id('dbo.EventConnect') and sysstat & 0xf = 3)
  drop table dbo.EventConnect
GO

if exists (select * from sysobjects 
  where id = object_id('dbo.EventUsers') and sysstat & 0xf = 3)
  drop table dbo.EventUsers
GO

/***************************************************************/
/***                     Creating tables                     ***/
/***************************************************************/

/* Table: dbo.User */
CREATE TABLE dbo.Users
(
  UserID 			int IDENTITY (1,1),
  UserName		varchar(50) 	NOT NULL,
  Salutation			varchar(5)  	NULL	
                                        CHECK (Salutation IN ('Dr','Mr','Ms','Mrs','Mdm')),
  EmailAddr		    	varchar(50)  	NOT NULL,
  [Password]		    varchar(255)  	NOT NULL DEFAULT ('password123'),
  Score		int 	NOT NULL DEFAULT (0),
  Badges		int 	NOT NULL DEFAULT (0),
  DateCreated		date 	NULL,
  CONSTRAINT PK_Users PRIMARY KEY NONCLUSTERED (UserID)
)
GO

CREATE TABLE dbo.EventConnect
(
  EventID 			int IDENTITY (1,1),
  EventName		varchar(100) 	NOT NULL,
  EventLocation		varchar(100) 	NOT NULL,
  StartDate datetime NULL,
  EndDate datetime NULL,
  CONSTRAINT PK_EventConnect PRIMARY KEY NONCLUSTERED (EventID)
)
GO

DROP TABLE EventUsers
/****** Object:  Table [dbo].[EventUsers]    Script Date: 3/11/2021 1:31:44 pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EventUsers](
	[EventID] [int] NOT NULL,
	[UserID] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EventUsers]  WITH CHECK ADD  CONSTRAINT [FK_EventUsers_EventID] FOREIGN KEY([EventID])
REFERENCES [dbo].[EventConnect] ([EventID])
GO

ALTER TABLE [dbo].[EventUsers] CHECK CONSTRAINT [FK_EventUsers_EventID]
GO

ALTER TABLE [dbo].[EventUsers]  WITH CHECK ADD  CONSTRAINT [FK_EventUsers_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO

ALTER TABLE [dbo].[EventUsers] CHECK CONSTRAINT [FK_EventUsers_UserID]
GO

/***************************************************************/
/***                Populate Sample Data                     ***/
/***************************************************************/

/* Table: dbo.User */
SET IDENTITY_INSERT [dbo].[Users] ON 
INSERT [dbo].[Users] ([UserID], [UserName], [Salutation], [EmailAddr], [Password], [Score], [Badges], [DateCreated]) 
VALUES (1, 'John Doe', 'Mr', 'jd1@hotmail.com', 'password123', 0, 0, '2021-07-06')
INSERT [dbo].[Users] ([UserID], [UserName], [Salutation], [EmailAddr], [Password], [Score], [Badges], [DateCreated])  
VALUES (2, 'Casca Susan', 'Mrs', 'cs1@gmail.com', 'password123', 100, 3, '2020-12-25')
INSERT [dbo].[Users] ([UserID], [UserName], [Salutation], [EmailAddr], [Password], [Score], [Badges], [DateCreated])  
VALUES (3, 'Gon Yeager', 'Dr', 'gy1@hotmail.com', 'password123', 999, 2, '2020-01-02')
SET IDENTITY_INSERT [dbo].[Users] OFF

/* Table: dbo.EventConnect */
SET IDENTITY_INSERT [dbo].[EventConnect] ON 
INSERT [dbo].[EventConnect] ([EventID], [EventName], [EventLocation], [StartDate], [EndDate]) 
VALUES (1, 'Caligraphy', 'Chua Chu Kang Somewhere','2021-11-04 12:15:00.000','2021-11-04 15:15:00.000')
SET IDENTITY_INSERT [dbo].[EventConnect] OFF 
