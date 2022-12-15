using Microsoft.EntityFrameworkCore;

using NTC_Lego.Shared;

using Inventory = NTC_Lego.Shared.Inventory;

namespace NTC_Lego.Server.Services
{
    public class PurchaseService
    {
        private readonly DataContext _dataContext;

        public PurchaseService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Get the three most recent purhcase orders, map to view model
        public IEnumerable<PurchaseOrderVM> GetPurchaseOrdersRecent()
        {
            return _dataContext.PurchaseOrder
                .Take(3)
                .OrderByDescending(x => x.PurchaseOrderId)
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

        // Get all purhcase orders, map to view model
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

        // Get a specific purhcase order, map to view model
        public PurchaseOrderVM GetPurchaseOrderVM(int id)
        {
            return _dataContext.PurchaseOrder
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
                .FirstOrDefault(x => x.PurchaseOrderId == id)!;
        }

        public IEnumerable<SupplierVM> GetSuppliers()
        {
            return _dataContext.Supplier
                .Select(x => new SupplierVM
                {
                    SupplierId = x.SupplierId,
                    SupplierName = x.SupplierName,
                    SupplierEmail = x.SupplierEmail
                })
                .ToList();
        }

        public PurchaseOrder GetPurchaseOrder(int id)
        {
            return _dataContext.PurchaseOrder.Include(x => x.PurchaseOrderDetails).ThenInclude(y => y.Inventory).FirstOrDefault(x => x.PurchaseOrderId == id)!;
        }

        public Inventory GetInventory(int id)
        {
            return _dataContext.Inventory.Include(x => x.InventoryLocations).FirstOrDefault(x => x.InventoryId == id)!;
        }

        public IEnumerable<InventoryLocation> GetInventoryLocations(int id)
        {
            return _dataContext.InventoryLocation.Where(x => x.InventoryId == id).OrderByDescending(x => x.ItemQuantity).ToList();
        }

        public Inventory GetInventory(string itemId, int colorId)
        {
            return _dataContext.Inventory.FirstOrDefault(x => x.ItemId == itemId && x.ColorId == colorId)!;
        }

        public InventoryLocation GetInventoryLocation(int inventoryId, int locationId)
        {
            return _dataContext.InventoryLocation.FirstOrDefault(x => x.InventoryId == inventoryId && x.LocationId == locationId)!;
        }

        // Transaction methods below
        public PurchaseOrder AddPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            _dataContext.PurchaseOrder.Add(purchaseOrder);
            _dataContext.SaveChanges();
            return purchaseOrder;
        }

        public void UpdatePurchaseOrder(PurchaseOrder old, PurchaseOrder update)
        {
            _dataContext.Entry(old).CurrentValues.SetValues(update);
            _dataContext.SaveChanges();
        }

        public PurchaseOrderDetail AddPurchaseOrderDetail(PurchaseOrderDetail purchaseOrderDetail)
        {
            _dataContext.PurchaseOrderDetail.Add(purchaseOrderDetail);
            _dataContext.SaveChanges();
            return purchaseOrderDetail;
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

        public InventoryLocation DeleteInventoryLocation(InventoryLocation inventoryLocation)
        {
            _dataContext.InventoryLocation.Remove(inventoryLocation);
            _dataContext.SaveChanges();
            return inventoryLocation;
        }

        public Inventory DeleteInventory(Inventory inventory)
        {
            _dataContext.Inventory.Remove(inventory);
            _dataContext.SaveChanges();
            return inventory;
        }
    }
}
