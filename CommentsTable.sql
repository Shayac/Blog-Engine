USE [BlogDB]
GO

/****** Object:  Table [dbo].[Comments]    Script Date: 1/9/2014 8:05:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[BlogPostId] [int] NOT NULL,
	[CommentBody] [nvarchar](max) NOT NULL,
	[Date] [datetime] NOT NULL,
	[ReplyId] [int] NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_BlogPosts] FOREIGN KEY([BlogPostId])
REFERENCES [dbo].[BlogPosts] ([Id])
GO

ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_BlogPosts]
GO

ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Replies] FOREIGN KEY([ReplyId])
REFERENCES [dbo].[Comments] ([Id])
GO

ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Replies]
GO

ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users]
GO

