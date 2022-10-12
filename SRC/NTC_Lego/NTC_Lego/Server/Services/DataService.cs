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

        //.Include(x => x.ItemType).Include(x => x.Category) // Foreign key relation
        // Top 100 for loading, pagination in future
        public List<Item> GetItems()
        {
            return _dataContext.Item.Take(100).ToList();
        }

        public Item GetItem(string ItemId)
        {
            return _dataContext.Item.Include(x => x.ItemType).Include(x => x.Category).ToList().Find(x => x.ItemId == ItemId);
        }

        public List<Color> GetItemColors(string ItemId)
        {
            // API call to BrickLink
            return null;
        }
        public string GetItemImage(string ItemId, string ColorId)
        {
            // API call to BrickLink, returns image path
            return null;
        }
    }
}
