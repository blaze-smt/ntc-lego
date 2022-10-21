using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NTC_Lego.Server.Services;
using NTC_Lego.Server.ViewModels;
using NTC_Lego.Shared;
using System.Security.Claims;

namespace NTC_Lego.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ItemController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("{itemId}")]
        public async Task<IActionResult> Get(string itemId)
        {
            var item = await _dataContext.Item.FirstOrDefaultAsync(x => x.ItemId == itemId);
            if (item == null)
                return NotFound("Item was not found.");
            return Ok(item);
        }
    }
}
