using NTC_Lego.Shared;

using Inventory = NTC_Lego.Shared.Inventory;

namespace NTC_Lego.Server.Services
{
    public class InventoryService
    {
        private readonly DataContext _dataContext;

        public InventoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Get a specific inventory, map to view model
        public InventoryVM GetInventoryVM(int inventoryId)
        {
            return _dataContext.Inventory
                .Select(x => new InventoryVM
                {
                    InventoryId = x.InventoryId,
                    InventoryItemPrice = x.InventoryItemPrice,
                    ColorId = x.ColorId,
                    Color = new ColorVM()
                    {
                        ColorId = x.Color.ColorId,
                        ColorName = x.Color.ColorName,
                    },
                    ItemId = x.ItemId,
                    InventoryLocations = (ICollection<InventoryLocationVM>)x.InventoryLocations.Select(y => new InventoryLocationVM
                    {
                        InventoryId = y.InventoryId,
                        ItemQuantity = y.ItemQuantity,
                        LocationId = y.LocationId,
                        Location = new LocationVM()
                        {
                            LocationId = y.LocationId,
                            BinName = y.Location.BinName,
                            Warehouse = new WarehouseVM()
                            {
                                WarehouseId = y.Location.Warehouse.WarehouseId,
                                WarehouseName = y.Location.Warehouse.WarehouseName,
                            },
                        },
                    })
                })
                .FirstOrDefault(x => x.InventoryId == inventoryId)!;
        }

        // Get all inventories, map to view model
        public IEnumerable<InventoryVM> GetInventories(int skip, int take)
        {
            return _dataContext.Inventory
                .Skip(skip)
                .Take(take)
                .Select(x => new InventoryVM
                {
                    InventoryId = x.InventoryId,
                    InventoryItemPrice = x.InventoryItemPrice,
                    ColorId = x.ColorId,
                    Color = new ColorVM()
                    {
                        ColorId = x.Color.ColorId,
                        ColorName = x.Color.ColorName,
                    },
                    ItemId = x.ItemId,
                    Item = new ItemVM()
                    {
                        ItemId = x.ItemId,
                        ItemTypeId = x.Item.ItemTypeId,
                        ItemName = x.Item.ItemName
                    },
                    InventoryLocations = (ICollection<InventoryLocationVM>)x.InventoryLocations.Select(y => new InventoryLocationVM
                    {
                        InventoryId = y.InventoryId,
                        ItemQuantity = y.ItemQuantity,
                        LocationId = y.LocationId,
                        Location = new LocationVM()
                        {
                            LocationId = y.LocationId,
                            BinName = y.Location.BinName,
                            Warehouse = new WarehouseVM()
                            {
                                WarehouseId = y.Location.Warehouse.WarehouseId,
                                WarehouseName = y.Location.Warehouse.WarehouseName,
                            },
                        },
                    })
                })
                .ToList();
        }

        // Get the three most recent inventories, map to view model
        public IEnumerable<InventoryVM> GetInventoriesRecent()
        {
            return _dataContext.Inventory
                .Take(3)
                .OrderByDescending(x => x.InventoryId)
                .Select(x => new InventoryVM
                {
                    InventoryId = x.InventoryId,
                    InventoryItemPrice = x.InventoryItemPrice,
                    ColorId = x.ColorId,
                    Color = new ColorVM()
                    {
                        ColorId = x.Color.ColorId,
                        ColorName = x.Color.ColorName,
                    },
                    ItemId = x.ItemId,
                    InventoryLocations = (ICollection<InventoryLocationVM>)x.InventoryLocations.Select(y => new InventoryLocationVM
                    {
                        InventoryId = y.InventoryId,
                        ItemQuantity = y.ItemQuantity,
                        LocationId = y.LocationId,
                        Location = new LocationVM()
                        {
                            LocationId = y.LocationId,
                            BinName = y.Location.BinName,
                            Warehouse = new WarehouseVM()
                            {
                                WarehouseId = y.Location.Warehouse.WarehouseId,
                                WarehouseName = y.Location.Warehouse.WarehouseName,
                            },
                        },
                    })
                })
                .ToList();
        }

        public IEnumerable<LocationVM> GetLocations()
        {
            return _dataContext.Location
                .Select(x => new LocationVM
                {
                    LocationId = x.LocationId,
                    BinName = x.BinName,
                    WarehouseId = x.WarehouseId,
                    Warehouse = new WarehouseVM
                    {
                        WarehouseId = x.Warehouse.WarehouseId,
                        WarehouseName = x.Warehouse.WarehouseName,
                    }
                })
                .ToList();
        }

        public ItemVM GetItem(string ItemId)
        {
            return _dataContext.Item
                .Select(x => new ItemVM
                {
                    ItemId = x.ItemId,
                    ItemName = x.ItemName,
                    ItemTypeId = x.ItemTypeId,
                    CategoryId = x.CategoryId
                })
                .FirstOrDefault(x => x.ItemId == ItemId)!;
        }

        public ColorVM GetItemColor(int colorId)
        {
            return _dataContext.Color
                .Select(x => new ColorVM
                {
                    ColorId = x.ColorId,
                    ColorName = x.ColorName,
                    ColorValue = x.ColorValue,
                    ColorType = x.ColorType
                })
                .FirstOrDefault(x => x.ColorId == colorId)!;
        }

        public Location GetLocation(int locationId)

        {
            return _dataContext.Location.FirstOrDefault(x => x.LocationId == locationId)!;
        }

        public InventoryLocation GetInventoryLocation(int inventoryId, int locationId)
        {
            return _dataContext.InventoryLocation.FirstOrDefault(x => x.InventoryId == inventoryId && x.LocationId == locationId)!;
        }

        public Inventory GetInventory(string itemId, int colorId)
        {
            return _dataContext.Inventory.FirstOrDefault(x => x.ItemId == itemId && x.ColorId == colorId)!;
        }
        public Inventory GetInventory(int inventoryId)
        {
            return _dataContext.Inventory.FirstOrDefault(x => x.InventoryId == inventoryId)!;
        }

        // Transaction methods below
        public Inventory AddInventory(Inventory inventory)
        {
            _dataContext.Inventory.Add(inventory);
            _dataContext.SaveChanges();
            return inventory;
        }

        public void UpdateInventory(Inventory old, Inventory update)
        {
            _dataContext.Entry(old).CurrentValues.SetValues(update);
            _dataContext.SaveChanges();
        }

        public Location AddLocation(Location location)
        {
            _dataContext.Location.Add(location);
            _dataContext.SaveChanges();
            return location;
        }

        public InventoryLocation AddInventoryLocation(InventoryLocation inventoryLocation)
        {
            _dataContext.InventoryLocation.Add(inventoryLocation);
            _dataContext.SaveChanges();
            return inventoryLocation;
        }

        public void UpdateInventoryLocation(InventoryLocation old, InventoryLocation update)
        {
            _dataContext.Entry(old).CurrentValues.SetValues(update);
            _dataContext.SaveChanges();
        }
    }
}
