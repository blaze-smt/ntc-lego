﻿using Microsoft.EntityFrameworkCore;

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

        //.Include(x => x.ItemType).Include(x => x.Category) // Foreign key relation
        // Top 100 for loading, pagination in future
        public IEnumerable<Item> GetItems()
        {
            return _dataContext.Item
                .Include(x => x.Category)
                .Include(x => x.ItemType)
                .ToList();
        }

        public Item GetItem(string ItemId)
        {
            return _dataContext.Item.ToList().Find(x => x.ItemId == ItemId);
        }

        public IEnumerable<PurchaseOrder> GetPurchaseOrders()
        {
            return _dataContext.PurchaseOrder
                .Include(x => x.Supplier)
                .Include(x => x.PurchaseOrderDetails)
                .ThenInclude(y => y.Inventory)
                .ToList();
        }
        public IEnumerable<SaleOrder> GetSaleOrders()
        {
            return _dataContext.SaleOrder
                .Include(x => x.User)
                .Include(x => x.SaleOrderDetails)
                .ThenInclude(y => y.Inventory)
                .ToList();
        }
        public IEnumerable<Inventory> GetInventories()
        {
            return _dataContext.Inventory
                .Include(x => x.Color)
                .Include(x => x.Item)
                .Include(x => x.Location)
                .ToList();
        }
    }
}
