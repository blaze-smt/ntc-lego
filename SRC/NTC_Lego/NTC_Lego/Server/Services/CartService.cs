using NTC_Lego.Client.Pages.AdminPortal;
using System.Collections.Immutable;
using NTC_Lego.Shared;
using Microsoft.EntityFrameworkCore;

namespace NTC_Lego.Server.Services
{
    public class CartService
    {
        private readonly DataContext _dataContext;

        public CartService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Cart item get
        public IEnumerable<CartItem> GetCartItems(int userId)
        {
            return _dataContext.CartItem.Where(x=>x.UserId==userId).ToList();
        }

        // Cart item add 
        // Cart item remove
        // Cart build sale order = Checkout
    }
}
