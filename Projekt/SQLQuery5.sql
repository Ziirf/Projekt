USE MASTER 
IF EXISTS (SELECT * FROM sys.databases WHERE NAME = 'MechanicDB')
BEGIN
	PRINT 'Databasen er der i forvejen, den bliver droppet'
	DROP DATABASE MechanicDB
END

ELSE
BEGIN
	PRINT 'Databasen findes ikke'
END
GO
CREATE DATABASE MechanicDB
	PRINT 'CREATING DB'
GO
USE MechanicDB
GO
SET DATEFORMAT dmy

CREATE TABLE Customer
(
customerID INT IDENTITY PRIMARY KEY,
firstname NVARCHAR(100) NOT NULL,
lastname NVARCHAR(100) NOT NULL,
[address] NVARCHAR(100) NOT NULL,
zipCode INT NOT NULL,
phoneNumber INT NOT NULL,
eMail NVARCHAR(100) NOT NULL,
createdDate DATE NOT NULL
)
GO

CREATE TABLE ZipAndCity
(
zipCode INT PRIMARY KEY,
city NVARCHAR(100) NOT NULL,
)

CREATE TABLE Car
(
customerID INT NOT NULL,
vinNumber NVARCHAR(100) PRIMARY KEY,
numberPlate NVARCHAR(100) NOT NULL,
carBrand NVARCHAR(100) NOT NULL,
carModel NVARCHAR(100) NOT NULL,
productionYear NVARCHAR(100) NOT NULL,
kmCount INT NOT NULL,
fuelType NVARCHAR(100) NOT NULL,
)

CREATE TABLE ShopVisit
(
dateTimeVisit SMALLDATETIME NOT NULL, 
mechanic NVARCHAR(100) NOT NULL,
vinNumber NVARCHAR(100) PRIMARY KEY,
kmCount INT NOT NULL,
issue NVARCHAR(100) NOT NULL,
notes NVARCHAR(200) 
)

GO
PRINT 'DATABASEN ER OPRETTET'
GO

ALTER TABLE Customer
ADD CONSTRAINT zipCode
FOREIGN KEY (zipCode) REFERENCES ZipAndCity(zipCode)

ALTER TABLE Car
ADD CONSTRAINT vinNumber
FOREIGN KEY (vinNumber) REFERENCES ShopVisit(vinNumber)

ALTER TABLE Car
ADD CONSTRAINT CustomerID
FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
GO
PRINT 'CONSTRAINTS TILFØJET'
GO

-- SELECT TOP 1 * FROM Customer ORDER BY CustomerID DESC
-- insert into Customer values ('ole', 'lukøje', 'nattevej 24', 3000, 12345678, 'sov@godt.dk', '12-12-1950')

SELECT TOP 1 Customer.customerID, Customer.firstname, Customer.lastname, Customer.[address], Customer.zipCode, ZipAndCity.city, Customer.phoneNumber, Customer.eMail, Customer.createdDate FROM Customer left join ZipAndCity
on Customer.zipCode = ZipAndCity.zipCode ORDER BY CustomerID DESC 

BULK INSERT ZipAndCity
FROM 'C:\Users\nfrii\Downloads\ZipAndCity.csv'
WITH
(
	CODEPAGE = 'ACP',
    FIELDTERMINATOR = ';',  --CSV field delimiter
    ROWTERMINATOR = '\n'   --Use to shift the control to next row
    --TABLOCK
)

SELECT * FROM ZipAndCity

SELECT Customer.customerID, Customer.firstname, Customer.lastname, Customer.[address], Customer.zipCode, ZipAndCity.city, Customer.phoneNumber, Customer.eMail, Customer.createdDate FROM Customer left join ZipAndCity
on Customer.zipCode = ZipAndCity.zipCode;


ALTER TABLE ShopVisit
ADD visitID int identity primary key;