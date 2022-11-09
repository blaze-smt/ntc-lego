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
        public IEnumerable<Item> GetItems(int skip, int take)
        {
            return _dataContext.Item
                .Skip(skip)
                .Take(take)
                .Include(x => x.Category)
                .Include(x => x.ItemType)
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
                    ShippingStatus = x.ShippingStatus,
                    PaymentStatus = x.PaymentStatus,
                    SupplierName = x.Supplier.SupplierName,
                    PurchaseOrderDetails = (ICollection<PurchaseOrderDetailVM>)x.PurchaseOrderDetails.Select(y => new PurchaseOrderDetailVM
                    {
                        PurchaseOrderDetailId = y.PurchaseOrderDetailId,
                        PurchaseOrderDetailQuantity = y.PurchaseOrderDetailQuantity,
                        InventoryItemPrice = y.Inventory.InventoryItemPrice,
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
                    ShippingStatus = x.ShippingStatus,
                    PaymentStatus = x.PaymentStatus,
                    UserName=x.User.UserName,
                    SaleOrderDetails = (ICollection<SaleOrderDetailVM>)x.SaleOrderDetails.Select(y => new SaleOrderDetailVM
                    {
                        SaleOrderDetailId = y.SaleOrderDetailId,
                        SaleOrderDetailQuantity = y.SaleOrderDetailQuantity,
                        InventoryItemPrice = y.Inventory.InventoryItemPrice,
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
                        ColorName = x.Color.ColorName,
                        ItemId = x.ItemId,
                        InventoryLocations = (ICollection<InventoryLocationVM>)x.InventoryLocations.Select(y=> new InventoryLocationVM 
                        { 
                            ItemQuantity = y.ItemQuantity,
                            BinName = y.Location.BinName,
                            WarehouseName = y.Location.Warehouse.WarehouseName
                        })
                    })
                    .ToList();
        }
            
    }
}
