create database [TTT]
go
use [TTT]
go
create table [Role]
(
	RoleID int primary key identity,
	RoleName nvarchar(100) not null
)
go
create table [User]
(
	UserID int primary key identity,
	UserSurname nvarchar(100) not null,
	UserName nvarchar(100) not null,
	UserPatronymic nvarchar(100) not null,
	UserLogin nvarchar(200) not null,
	UserPassword nvarchar(200) not null,
	UserRole int foreign key references [Role](RoleID) not null
)

go

create table UnitProduct
(
  [Name] varchar (5) primary key
)

go

create table ManufacturerProduct
(
  [Name] nvarchar (200) primary key
)

go

create table [Provider]
(
  [Name] nvarchar (200) primary key
)

go
create table CategoryProduct
(
  [Name] nvarchar (200) primary key
)

go
create table Product
(
	ProductArticleNumber nvarchar(100) primary key,
	ProductName nvarchar(max) not null,
	Unit varchar (5) foreign key references UnitProduct ([Name]),
	ProductCost decimal(19,4) not null,
	ProductDiscountAmount int null,
	Manufacturer nvarchar (200) foreign key references ManufacturerProduct ([Name]),
	[Provider] nvarchar (200) foreign key references [Provider] ([Name]),
	Category nvarchar (200) foreign key references CategoryProduct([Name]),
	[CurrentDiscount] int,
	ProductQuantityInStock int not null,
	ProductDescription nvarchar(max) not null,
	ProductPhoto varchar(100) not null
)


go

create table PickupPoint
(
  IDPoint int primary key,
  Adress varchar (150)
  
)

go

create table [Order]
(
	OrderID int primary key identity,
	DateOrder date,
	OrderDeliveryDate datetime not null,
	OrderPickupPoint int foreign key references PickupPoint (IDPoint) not null,
	Client int foreign key references [User] (UserID),
	Code int,
	OrderStatus nvarchar(30) not null
)

create table OrderProduct
(
	OrderID int foreign key references [Order](OrderID) not null,
	ProductArticleNumber nvarchar(100) foreign key references Product(ProductArticleNumber) not null,
	Primary key (OrderID,ProductArticleNumber)
)