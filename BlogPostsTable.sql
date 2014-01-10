USE [BlogDB]
GO

/****** Object:  Table [dbo].[BlogPosts]    Script Date: 1/9/2014 8:05:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BlogPosts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AuthorId] [int] NOT NULL,
	[PostBody] [nvarchar](max) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_BlogPosts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[BlogPosts]  WITH CHECK ADD  CONSTRAINT [FK_BlogPosts_Authors] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Authors] ([Id])
GO

ALTER TABLE [dbo].[BlogPosts] CHECK CONSTRAINT [FK_BlogPosts_Authors]
GO

