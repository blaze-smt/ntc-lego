-- =======================================================
-- ONE-TIME || Add Seed data to NTC Lego Database
-- Instructions: 
-- 0. Get the BrickLink XMLs and place them on your machine at "C:\temp\"
-- 1. Delete exisiting database
-- 2. Run "dotnet ef database update" in a terminal in the Server project to build a clean database
-- 3. Open a query in that new database and paste the scripts below.
-- 4. Run the query
-- 5. If no errors occur, you are finished and all seed data is now in the database.
-- =======================================================

DECLARE @InputXML XML
SELECT @InputXML = CAST(x AS XML)
FROM OPENROWSET(BULK 'C:\temp\itemtypes.xml', SINGLE_BLOB) AS T(x)

INSERT INTO [ItemType](ItemTypeId,ItemTypeName)
SELECT
   item.value('(ITEMTYPE)[1]', 'NVARCHAR(1)'),
   item.value('(ITEMTYPENAME)[1]', 'NVARCHAR(20)')
FROM @InputXML.nodes('CATALOG/ITEM') AS X(item);
go

-- =======================================================

DECLARE @InputXML XML
SELECT @InputXML = CAST(x AS XML)
FROM OPENROWSET(BULK 'C:\temp\categories.xml', SINGLE_BLOB) AS T(x)

SET IDENTITY_INSERT [Category] ON

INSERT INTO [Category](CategoryId, CategoryName)
SELECT
   item.value('(CATEGORY)[1]', 'INT'),
   item.value('(CATEGORYNAME)[1]', 'NVARCHAR(50)')
FROM @InputXML.nodes('CATALOG/ITEM') AS X(item);
go

SET IDENTITY_INSERT [Category] OFF

-- =======================================================

DECLARE @InputXML XML
SELECT @InputXML = CAST(x AS XML)
FROM OPENROWSET(BULK 'C:\temp\colors.xml', SINGLE_BLOB) AS T(x)

INSERT INTO [Color](ColorId, ColorName, ColorValue, ColorType)
SELECT
   item.value('(COLOR)[1]', 'INT'),
   item.value('(COLORNAME)[1]','NVARCHAR(50)'),
   item.value('(COLORRGB)[1]','NVARCHAR(10)'),
   item.value('(COLORTYPE)[1]','NVARCHAR(50)')
FROM @InputXML.nodes('CATALOG/ITEM') AS X(item);
go


DECLARE @InputXML XML
SELECT @InputXML = CAST(x AS XML)
FROM OPENROWSET(BULK 'C:\temp\Sets.xml', SINGLE_BLOB) AS T(x)

INSERT INTO [Item](ItemId, ItemName, ItemWeight, ItemTypeId, CategoryId)
SELECT
   item.value('(ITEMID)[1]', 'NVARCHAR(450)'),
   item.value('(ITEMNAME)[1]','NVARCHAR(100)'),
   item.value('(ITEMWEIGHT)[1]','FLOAT'),
   item.value('(ITEMTYPE)[1]','NVARCHAR(1)'),
   item.value('(CATEGORY)[1]','INT')
FROM @InputXML.nodes('CATALOG/ITEM') AS X(item);
go

-- =======================================================

DECLARE @InputXML XML
SELECT @InputXML = CAST(x AS XML)
FROM OPENROWSET(BULK 'C:\temp\Parts.xml', SINGLE_BLOB) AS T(x)

INSERT INTO [Item](ItemId, ItemName, ItemWeight, ItemTypeId, CategoryId)
SELECT
   item.value('(ITEMID)[1]', 'NVARCHAR(450)'),
   item.value('(ITEMNAME)[1]','NVARCHAR(100)'),
   item.value('(ITEMWEIGHT)[1]','FLOAT'),
   item.value('(ITEMTYPE)[1]','NVARCHAR(1)'),
   item.value('(CATEGORY)[1]','INT')
FROM @InputXML.nodes('CATALOG/ITEM') AS X(item);
go

-- =======================================================
-- Other (non-XML) Seed Data Below
-- =======================================================

INSERT INTO [Warehouse](WarehouseName)
VALUES 
   ('Wausau Storage'),
   ('Merrill Supply');
Go

INSERT INTO [Location](BinName,WarehouseId)
VALUES 
   ('1A', 1),
   ('2B', 1),
   ('3C', 1),
   ('1AD', 2),
   ('2BC', 2),
   ('3FE', 2);
Go

INSERT INTO [Inventory](InventoryItemPrice, ColorId, ItemId)
VALUES 
   (155.95, 0, '10297-1'),
   (1.20, 11,'60352-6'),
   (0.10, 59,'41748')
Go

INSERT INTO [InventoryLocation](InventoryId,LocationId,ItemQuantity)
VALUES 
   (1,1,20),
   (1,2,40),
   (2,3,700),
   (2,4,15),
   (3,5,150),
   (3,6,25)
Go

-- admin password: admin123
INSERT INTO [User](UserName,UserEmail,PasswordHash,IsAdmin)
VALUES 
   ('admin2022','admin@admin.com','AQAAAAEAACcQAAAAELmyrirjM+Ux1myabZvMSlNne9wEmko/d47LsVSaLb43DUeGV069INxCzbvETfUNbw==',1),
   ('ZeldaFan2022','ZeldaRulez@gmail.com',null,0),
   ('JackieJason','JJ2022@hotmail.com',null,0);
Go

INSERT INTO [SaleOrder](SaleOrderDate, UserId, ShippingStatus, PaymentStatus)
VALUES 
   ('2022-9-12', 1, 0, 0),
   ('2022-11-6', 2, 1, 1);
Go

INSERT INTO [SaleOrderDetail](InventoryId, SaleOrderDetailQuantity, SaleOrderId)
VALUES 
   (1, 10, 1),
   (2, 2, 1),
   (3, 800, 2);
Go

INSERT INTO [Supplier](SupplierName, SupplierEmail)
VALUES 
   ('Super Toy Inc.', 'Offical@SuperToy.com');
Go

INSERT INTO [PurchaseOrder](PurchaseOrderDate, SupplierId, ShippingStatus, PaymentStatus)
VALUES 
   ('2022-9-11', 1, 0, 0),
   ('2022-11-7', 1, 1, 1);
Go

INSERT INTO [PurchaseOrderDetail](InventoryId, PurchaseOrderDetailQuantity, PurchaseOrderId)
VALUES 
   (1, 25, 1),
   (2, 100, 1),
   (3, 350, 2);
Go