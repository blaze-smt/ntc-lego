using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NTC_Lego.Shared;

namespace NTC_Lego.Server.Services
{
    public class DataService
    {
        private readonly DataContext _dataContext;

        public DataService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public User AddUser(User user)
        {
            _dataContext.User.Add(user);
            _dataContext.SaveChanges();
            return user;
        }

        public User GetUser(int id)
        {
            return _dataContext.User.AsNoTracking().FirstOrDefault(u => u.UserId == id);
        }

        public User GetUser(string email)

        {
            return _dataContext.User.FirstOrDefault(e => e.UserEmail.ToLower() == email.ToLower());
        }

        public Item GetItem(string ItemId)
        {
            return _dataContext.Item.ToList().Find(x => x.ItemId == ItemId);
        }

        // Populate master tables
        // Model => ViewModel mapping is done during the database call in the select statment. 
        public IEnumerable<ItemVM> GetItems(int skip, int take)
        {
            return _dataContext.Item
                .Skip(skip)
                .Take(take)
                .Select(x => new ItemVM
                {
                    ItemId = x.ItemId,
                    ItemName = x.ItemName,
                    ItemWeight = x.ItemWeight,
                    ItemTypeId = x.ItemTypeId,
                    ItemType = new ItemTypeVM
                    {
                        ItemTypeId = x.ItemType.ItemTypeId,
                        ItemTypeName = x.ItemType.ItemTypeName,
                    },
                    CategoryId = x.CategoryId,
                    Category = new CategoryVM
                    {
                        CategoryId = x.Category.CategoryId,
                        CategoryName = x.Category.CategoryName,
                    },
                })
                .ToList();
        }

        public IEnumerable<PurchaseOrderVM> GetPurchaseOrders(int skip, int take)
        {
            return _dataContext.PurchaseOrder
                .Skip(skip)
                .Take(take)
                .Select(x => new PurchaseOrderVM
                {
                    PurchaseOrderId = x.PurchaseOrderId,
                    PurchaseOrderDate = x.PurchaseOrderDate,
                    OrderStatus = x.OrderStatus,
                    SupplierId = x.SupplierId,
                    Supplier = new SupplierVM
                    {
                        SupplierId = x.Supplier.SupplierId,
                        SupplierName = x.Supplier.SupplierName,
                        SupplierEmail = x.Supplier.SupplierEmail,
                    },
                    PurchaseOrderDetails = (ICollection<PurchaseOrderDetailVM>)x.PurchaseOrderDetails.Select(y => new PurchaseOrderDetailVM
                    {
                        PurchaseOrderDetailId = y.PurchaseOrderDetailId,
                        PurchaseOrderDetailQuantity = y.PurchaseOrderDetailQuantity,
                        PurchaseOrderId = y.PurchaseOrderId,
                        InventoryId = y.InventoryId,
                        Inventory = new InventoryVM
                        {
                            InventoryId = y.Inventory.InventoryId,
                            InventoryItemPrice = y.Inventory.InventoryItemPrice,
                            InventoryLocations = (ICollection<InventoryLocationVM>)y.Inventory.InventoryLocations.Select(y => new InventoryLocationVM
                            {
                                InventoryId = y.InventoryId,
                                ItemQuantity = y.ItemQuantity,
                                LocationId = y.LocationId,
                            })
                        }
                    })
                })
                .ToList();
        }

        public IEnumerable<SaleOrderVM> GetSaleOrders(int skip, int take)
        {
            return _dataContext.SaleOrder
                .Skip(skip)
                .Take(take)
                .Select(x => new SaleOrderVM
                {
                    SaleOrderId = x.SaleOrderId,
                    SaleOrderDate = x.SaleOrderDate,
                    OrderStatus = x.OrderStatus,
                    UserId = x.UserId,
                    User = new UserVM
                    {
                        UserId = x.User.UserId,
                        UserEmail = x.User.UserEmail,
                        UserName = x.User.UserName,
                    },
                    SaleOrderDetails = (ICollection<SaleOrderDetailVM>)x.SaleOrderDetails.Select(y => new SaleOrderDetailVM
                    {
                        SaleOrderDetailId = y.SaleOrderDetailId,
                        SaleOrderDetailQuantity = y.SaleOrderDetailQuantity,
                        SaleOrderId = y.SaleOrderId,
                        InventoryId = y.InventoryId,
                        Inventory = new InventoryVM 
                        { 
                            InventoryId = y.Inventory.InventoryId,
                            InventoryItemPrice = y.Inventory.InventoryItemPrice,
                            InventoryLocations = (ICollection<InventoryLocationVM>)y.Inventory.InventoryLocations.Select(y => new InventoryLocationVM
                            {
                                InventoryId = y.InventoryId,
                                ItemQuantity = y.ItemQuantity,
                                LocationId = y.LocationId,
                            })
                        }
                    })
                })
                .ToList();
        }

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

        // Transactional actions below (add, cancel)
        // Validation or business logic in controller and/or viewmodel
        // WIP
        public Inventory AddInventory(Inventory inventory)
        {
            _dataContext.Inventory.Add(inventory);
            _dataContext.SaveChanges();
            return inventory;
        }

        public InventoryLocation AddInventoryLocation(InventoryLocation inventoryLocation)
        {
            _dataContext.InventoryLocation.Add(inventoryLocation);
            _dataContext.SaveChanges();
            return inventoryLocation;
        }
    }
}
