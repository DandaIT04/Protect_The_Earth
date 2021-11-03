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
  Score		varchar(50) 	NOT NULL DEFAULT ('0'),
  Badges		varchar(50) 	NOT NULL DEFAULT ('0'),
  CONSTRAINT PK_Users PRIMARY KEY NONCLUSTERED (UserID)
)
GO

/***************************************************************/
/***                Populate Sample Data                     ***/
/***************************************************************/

/* Table: dbo.User */
SET IDENTITY_INSERT [dbo].[Users] ON 
INSERT [dbo].[Users] ([UserID], [UserName], [Salutation], [EmailAddr], [Password], [Score], [Badges]) 
VALUES (1, 'John Doe', 'Mr', 'jd1@hotmail.com', 'password123', '0', '0')
INSERT [dbo].[Users] ([UserID], [UserName], [Salutation], [EmailAddr], [Password], [Score], [Badges])  
VALUES (2, 'Casca Susan', 'Mrs', 'cs1@gmail.com', 'password123', '100', '3')
INSERT [dbo].[Users] ([UserID], [UserName], [Salutation], [EmailAddr], [Password], [Score], [Badges]) 
VALUES (3, 'Gon Yeager', 'Dr', 'gy1@hotmail.com', 'password123', '999', '2')
SET IDENTITY_INSERT [dbo].[Users] OFF
