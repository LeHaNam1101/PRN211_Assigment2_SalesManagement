	CREATE DATABASE FStore
GO

USE FStore
GO

CREATE TABLE Member(
MemberId INT PRIMARY KEY NOT NULL,
Email VARCHAR(100) NOT NULL,
CompanyName VARCHAR(40) NOT NULL,
City VARCHAR(15) NOT NULL,
Country VARCHAR(15) NOT NULL,
Password VARCHAR(15) NOT NULL
)
GO

CREATE TABLE [Order](
OrderID INT PRIMARY KEY NOT NULL,
MemberId INT NOT NULL FOREIGN KEY REFERENCES dbo.Member(MemberId),
OrderDate DATETIME NOT NULL,
RequiredDate DATETIME,
ShippedDate DATETIME,
Freight MONEY
)
GO

CREATE TABLE Product(
ProductId INT PRIMARY KEY NOT NULL,
CategoryId INT NOT NULL,
ProductName VARCHAR(40) NOT NULL,
[Weight] VARCHAR(20) NOT NULL,
UnitPrice MONEY NOT NULL,
UnitsInStock INT NOT NULL
)
GO

CREATE TABLE OrderDetail(
OrderId INT NOT NULL FOREIGN KEY REFERENCES dbo.[Order] (OrderID),
ProductId INT NOT NULL FOREIGN KEY REFERENCES dbo.Product (ProductId) ,
UnitPrice MONEY NOT NULL,
Quantity INT NOT NULL,
Discount FLOAT NOT NULL,
PRIMARY KEY (OrderId,ProductId)
)
GO