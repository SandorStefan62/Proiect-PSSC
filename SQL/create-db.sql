
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](10) NOT NULL UNIQUE,
	[Stock] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OrderHeader](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[Adress] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[TelephoneNumber] [varchar](50) NOT NULL,
	[CheckoutDate] [datetime],
	[Total] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_OrderHeader] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OrderLine](
	[OrderLineId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NULL,
	[Price] [decimal](18, 0) NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_OrderLine] PRIMARY KEY CLUSTERED 
(
	[OrderLineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderLine]  WITH CHECK ADD  CONSTRAINT [FK_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[OrderHeader] ([OrderId])
GO

ALTER TABLE [dbo].[OrderLine] CHECK CONSTRAINT [FK_OrderId]
GO

ALTER TABLE [dbo].[OrderLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO

ALTER TABLE [dbo].[OrderLine] CHECK CONSTRAINT [FK_ProductId]
GO

INSERT INTO [dbo].[Product]
     VALUES
	 ('Televizor',130,1500),
	 ('Bormasina',125,225),
	 ('Cauciucuri',165,900),
	 ('Casti',174,200),
	 ('Ceas',112,350),
	 ('Pix',1223,3),
	 ('Creion',1450,4),
	 ('Tastatura',154,175),
	 ('Mouse',132,95),
     ('Calculator',174,4500),
	 ('Telefon',188,2000)
GO
