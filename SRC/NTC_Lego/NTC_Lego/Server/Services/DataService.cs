using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NTC_Lego.Shared;

namespace NTC_Lego.Server.Services
{
    /*
     * Data Service classes are used to perform CRUD operations on the database (aka DataContext).
     * This class should NOT contain business logic. All validation and checks should be performed before entering this class, preferably in the controller. 
     * Performing the database model to viewmodel mapping is currently done in this class, however it could also be done in a controller
     */
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
            return _dataContext.User.AsNoTracking().FirstOrDefault(u => u.UserId == id)!;
        }

        public User GetUser(string email)

        {
            return _dataContext.User.FirstOrDefault(e => e.UserEmail.ToLower() == email.ToLower())!;
        }

        public Item GetItem(string ItemId)
        {
            return _dataContext.Item.ToList().Find(x => x.ItemId == ItemId)!;
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

        // Populate master tables
        // Model => ViewModel mapping is done during the database call using the select statement. 
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

        public IEnumerable<SaleOrderVM> GetSaleOrdersRecent()
        {
            return _dataContext.SaleOrder
                .Take(3)
                .OrderByDescending(x => x.SaleOrderId)
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

        // Calculated database calls to get sum totals of a field within each row of a table. 
        // This logic is preferably done within the data service class for reusablity purposes and speed. 
        public decimal GetAllSaleOrders()
        {
            return _dataContext.SaleOrder
                .Where(x => x.OrderStatus != OrderStatus.Canceled)
                .Include(x => x.SaleOrderDetails)
                .ThenInclude(x => x.Inventory)
                .ToList()
                .Sum(x => x.SaleOrderTotalPrice);
        }

        public decimal GetAllPurchaseOrders()
        {
            return _dataContext.PurchaseOrder
                .Where(x => x.OrderStatus != OrderStatus.Canceled)
                .Include(x => x.PurchaseOrderDetails)
                .ThenInclude(x => x.Inventory)
                .ToList()
                .Sum(x => x.PurchaseOrderTotalPrice);
        }

        public async Task<List<Item>> SearchItems(string searchText)
        {
            return await _dataContext.Item
                .Where(x => x.ItemName.Contains(searchText)
                || x.ItemId.Contains(searchText)
                || x.ItemTypeId.Contains(searchText))
                .Take(10).ToListAsync();
        }
    }
}
