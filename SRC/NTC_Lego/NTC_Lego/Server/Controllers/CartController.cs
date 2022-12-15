using Microsoft.AspNetCore.Mvc;

using NTC_Lego.Server.Services;
using NTC_Lego.Shared;

namespace NTC_Lego.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly ILogger<AccountController> _log;
        private readonly IConfiguration _configuration;

        public CartController(DataContext dataContext, ILogger<AccountController> log, IConfiguration configuration)
        {
            _cartService = new CartService(dataContext);
            _log = log;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("cartitem")] 
        public IEnumerable<CartItemVM> GetCartItem(int userId)
        {
            var cartItems = _cartService.GetCartItemsVM(userId);
            return cartItems;
        }
    }
}
