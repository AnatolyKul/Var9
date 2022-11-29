create database [Trade]
go
use [Trade]
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
	UserLogin nvarchar(max) not null,
	UserPassword nvarchar(max) not null,
	UserRole int foreign key references [Role](RoleID) not null
)
go

create table Manufacturer 
(
  [Name] nvarchar (100) primary key not null
)

go

create table [Provider] 
(
  [Name] nvarchar (100) primary key not null
)

go

create table Category 
(
  [Name] nvarchar (100) primary key not null
)

go

create table Unit 
(
  [Name] nvarchar (4) primary key not null
)

go
create table Product
(
	ProductArticleNumber nvarchar(100) primary key,
	ProductName nvarchar(max) not null,
	UnitProduct nvarchar(4) foreign key references Unit ([Name]),
	ProductCost decimal(19,4) not null,
	ProductDiscountAmount int,
	ProductManufacturer nvarchar(100) foreign key references Manufacturer ([Name]),
	ProductProvider nvarchar(100) foreign key references [Provider] ([Name]),
	ProductCategory nvarchar(100) foreign key references [Category] ([Name]),
	ProductDiscount int,
	ProductQuantityInStock int not null,
	ProductDescription nvarchar(max) not null,
	ProductPhoto varchar (50) not null
)
go

create table Order_Status 
(
  [Name] nvarchar (30) primary key not null
)

go

create table PickupPoint
(
 ID int primary key identity  not null,
 [Index] varchar (10),
 Adress varchar (200)
)

go

create table [Order]
(
	OrderID int primary key,
	OrderDate date,
	OrderDeliveryDate datetime not null,
	OrderPickupPoint nvarchar(max) not null,
	Client int foreign key references [User] ([UserID]),
	Code varchar (4),
	OrderStatus nvarchar(30) foreign key references Order_Status ([Name])
)

go

create table OrderProduct
(
	OrderID int foreign key references [Order](OrderID) not null,
	ProductArticleNumber nvarchar(100) foreign key references Product(ProductArticleNumber) not null,
	CountProduct int,
	Primary key (OrderID,ProductArticleNumber)
)
