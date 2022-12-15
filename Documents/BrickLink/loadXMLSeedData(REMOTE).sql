-- =======================================================
-- ONE-TIME || Add Seed data to NTC Lego Database (REMOTE-Azure)
-- Instructions: 
-- 1. Get the BrickLink XMLs and place them in an Azure storage container. 
-- 2. Get SAS to connect to external data source (set container access to public).
-- 3. Delete exisiting database
-- 4. Run "dotnet ef database update" in a terminal in the Server project to build a clean database
-- 5. Open a query in that new database and paste the scripts below.
-- 6. Run the query
-- 7. If no errors occur, you are finished and all seed data is now in the database.
-- =======================================================

CREATE EXTERNAL DATA SOURCE MyAzureBlobStorage
WITH ( TYPE = BLOB_STORAGE,LOCATION = 'https://cs21003bffdac63df35.blob.core.windows.net/bricklinkxml'); --Update url to SAS as needed.
go

-- =======================================================

DECLARE @InputXML XML
SELECT @InputXML = CAST(x AS XML)
FROM OPENROWSET(BULK 'itemtypes.xml', DATA_SOURCE = 'MyAzureBlobStorage', SINGLE_BLOB) AS T(x)

INSERT INTO [ItemType](ItemTypeId,ItemTypeName)
SELECT
   item.value('(ITEMTYPE)[1]', 'NVARCHAR(1)'),
   item.value('(ITEMTYPENAME)[1]', 'NVARCHAR(20)')
FROM @InputXML.nodes('CATALOG/ITEM') AS X(item);
go

-- =======================================================

DECLARE @InputXML XML
SELECT @InputXML = CAST(x AS XML)
FROM OPENROWSET(BULK 'categories.xml', DATA_SOURCE = 'MyAzureBlobStorage', SINGLE_BLOB) AS T(x)

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
FROM OPENROWSET(BULK 'colors.xml', DATA_SOURCE = 'MyAzureBlobStorage', SINGLE_BLOB) AS T(x)

INSERT INTO [Color](ColorId, ColorName, ColorValue, ColorType)
SELECT
   item.value('(COLOR)[1]', 'INT'),
   item.value('(COLORNAME)[1]','NVARCHAR(50)'),
   item.value('(COLORRGB)[1]','NVARCHAR(10)'),
   item.value('(COLORTYPE)[1]','NVARCHAR(50)')
FROM @InputXML.nodes('CATALOG/ITEM') AS X(item);
go

-- =======================================================

DECLARE @InputXML XML
SELECT @InputXML = CAST(x AS XML)
FROM OPENROWSET(BULK 'Sets.xml', DATA_SOURCE = 'MyAzureBlobStorage', SINGLE_BLOB) AS T(x)

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
FROM OPENROWSET(BULK 'Parts.xml', DATA_SOURCE = 'MyAzureBlobStorage', SINGLE_BLOB) AS T(x)

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

DROP EXTERNAL DATA SOURCE MyAzureBlobStorage;
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
   (0.10, 59,'41748'),
   (0.02, 7,'3003'),
   (0.01, 102,'3005'),
   (0.08, 89,'2456'),
   (0.90, 5,'4161'),
   (0.15, 156,'30363'),
   (0.15, 7,'30144'),
   (0.14, 11,'3031'),
   (6.21, 11,'23949'),
   (0.26, 104,'3007')
Go

INSERT INTO [InventoryLocation](InventoryId,LocationId,ItemQuantity)
VALUES 
   (1,1,50),
   (1,2,25),
   (2,2,500),
   (3,3,200),
   (4,4,700),
   (5,5,800),
   (6,6,600),
   (7,1,200),
   (8,2,500),
   (9,3,525),
   (10,4,600),
   (11,5,335),
   (12,6,250)
Go

-- admin password: admin123
-- ZeldaFan2022 password: test123
-- JackieJason password: test123
INSERT INTO [User](UserName,UserEmail,PasswordHash,IsAdmin)
VALUES 
   ('admin2022','admin@admin.com','AQAAAAEAACcQAAAAELmyrirjM+Ux1myabZvMSlNne9wEmko/d47LsVSaLb43DUeGV069INxCzbvETfUNbw==',1),
   ('ZeldaFan2022','ZeldaRulez@gmail.com','AQAAAAEAACcQAAAAECzJZ1wFa7UyRjz0noX1YfNDd9fc9LN+nfXB+zJjsQ2FWGVeAEPdK2jkzDJ1ZU/wAQ==',0),
   ('JackieJason','JJ2022@hotmail.com','AQAAAAEAACcQAAAAECzJZ1wFa7UyRjz0noX1YfNDd9fc9LN+nfXB+zJjsQ2FWGVeAEPdK2jkzDJ1ZU/wAQ==',0);
Go

INSERT INTO [SaleOrder](SaleOrderDate, UserId, OrderStatus)
VALUES 
   ('2022-9-12', 2, 0),
   ('2022-11-27', 3, 1);
Go

INSERT INTO [SaleOrderDetail](InventoryId, SaleOrderDetailQuantity, SaleOrderId)
VALUES 
   (2, 10, 1),
   (3, 2, 1),
   (4, 25, 2);
Go

INSERT INTO [Supplier](SupplierName, SupplierEmail)
VALUES 
   ('Super Toy Inc.', 'Offical@SuperToy.com'),
   ('ToyHouse LLC.', 'Admin@ToyHouse.com'),
   ('Kids Life Corp.', 'KidsLife@gmail.com');
Go

INSERT INTO [PurchaseOrder](PurchaseOrderDate, SupplierId, OrderStatus)
VALUES 
   ('2022-2-4', 1, 0),
   ('2022-11-26', 1, 1),
   ('2022-8-2', 2, 0),
   ('2022-9-11', 3, 0),
   ('2022-11-7', 1, 1),
   ('2022-11-27', 2, 0);
Go

INSERT INTO [PurchaseOrderDetail](InventoryId, PurchaseOrderDetailQuantity, PurchaseOrderId)
VALUES 
   (8, 25, 1),
   (9, 5, 1),
   (1, 80, 2),
   (4, 25, 3),
   (5, 2, 4),
   (6, 40, 5),
   (7, 15, 6);
Go