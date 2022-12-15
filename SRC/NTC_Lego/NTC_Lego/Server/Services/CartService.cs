using NTC_Lego.Shared;

namespace NTC_Lego.Server.Services
{
    public class CartService
    {
        private readonly DataContext _dataContext;

        public CartService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Get all cart items for a specific user
        public IEnumerable<CartItem> GetCartItems(int userId)
        {
            return _dataContext.CartItem.Where(x => x.UserId == userId).ToList();
        }

        // Get all cart items for a specific user, mapped to view model
        public IEnumerable<CartItemVM> GetCartItemsVM(int userId)
        {
            return _dataContext.CartItem
                .Where(x => x.UserId == userId)
                .Select(x => new CartItemVM
                {
                    CartItemId = x.CartItemId,
                    CartItemQuantity = x.CartItemQuantity,
                    InventoryId = x.InventoryId,
                    Inventory = new InventoryVM
                    {
                        InventoryId = x.Inventory.InventoryId,
                        InventoryItemPrice = x.Inventory.InventoryItemPrice,
                        ItemId = x.Inventory.ItemId,
                        Color = new ColorVM()
                        {
                            ColorId = x.Inventory.Color.ColorId,
                            ColorName = x.Inventory.Color.ColorName,
                        },
                        InventoryLocations = (ICollection<InventoryLocationVM>)x.Inventory.InventoryLocations.Select(y => new InventoryLocationVM
                        {
                            InventoryId = y.InventoryId,
                            ItemQuantity = y.ItemQuantity,
                            LocationId = y.LocationId,
                        })
                    },
                    UserId = x.UserId,
                    User = new UserVM
                    {
                        UserId = x.User.UserId,
                        UserEmail = x.User.UserEmail,
                        UserName = x.User.UserName,
                    },
                })
                .ToList();
        }

        // Cart item add 
        // Cart item remove
        // Cart build sale order = Checkout
    }
}
