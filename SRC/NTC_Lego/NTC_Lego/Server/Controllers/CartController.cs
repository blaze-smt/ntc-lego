using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NTC_Lego.Server.Services;
using NTC_Lego.Shared;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using NTC_Lego.Server.Util;

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
        [Route("cartitem")] // cart/cartitem?userId=0
        public IEnumerable<CartItemVM> GetCartItem(int userId)
        {
            var cartItems = _cartService.GetCartItemsVM(userId);
            return cartItems;
        }
    }
}
