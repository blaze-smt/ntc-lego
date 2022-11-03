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
   ('1AD', 2);
Go

INSERT INTO [Inventory](ColorId, InventoryItemPrice, InventoryQuantity, ItemId, LocationId)
VALUES 
   (0, 155.95, 20, '10297-1', 1),
   (11, 1.20, 89, '60352-6', 2),
   (0, 155.95, 99, '10297-1', 3 );
Go

INSERT INTO [User](UserName,UserEmail)
VALUES 
   ('ZeldaFan2022','ZeldaRulez@gmail.com'),
   ('JackieJason','JJ2022@hotmail.com');
Go

INSERT INTO [SaleOrder](SaleOrderDate, UserId)
VALUES 
   ('2022-9-12', 1);
Go

INSERT INTO [SaleOrderDetail](InventoryId, SaleOrderDetailQuantity, SaleOrderId)
VALUES 
   (1, 10, 1);
Go

INSERT INTO [Supplier](SupplierName, SUpplierEmail)
VALUES 
   ('Super Toy Inc.', 'Offical@SuperToy.com');
Go

INSERT INTO [PurchaseOrder](PurchaseOrderDate, SupplierId)
VALUES 
   ('2022-9-11', 1);
Go

INSERT INTO [PurchaseOrderDetail](InventoryId, PurchaseOrderDetailQuantity, PurchaseOrderId)
VALUES 
   (1, 100, 1);
Go