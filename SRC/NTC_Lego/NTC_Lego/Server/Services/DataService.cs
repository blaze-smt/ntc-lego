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
        public IEnumerable<Inventory> GetInventories(int skip, int take)
        {
            return _dataContext.Inventory
                .Skip(skip)
                .Take(take)
                .Include(x => x.Color)
                .Include(x => x.Item)
                .Include(x => x.InventoryLocations)
                .ThenInclude(y => y.Location)
                .ThenInclude(y => y.Warehouse)
                .ToList();
        }
    }
}
