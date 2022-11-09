using BricklinkSharp.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NTC_Lego.Server.Services;
using NTC_Lego.Shared;
using System.Security.Claims;
using Inventory = NTC_Lego.Shared.Inventory;
using ItemType = BricklinkSharp.Client.ItemType;


namespace NTC_Lego.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly DataService _dataService;

        public ItemController(DataContext dataContext)
        {
            _dataService = new DataService(dataContext);
        }

        [HttpGet("{itemId}")]
        public async Task<IActionResult> Get(string itemId)
        {
            //var item = await _dataContext.Item.FirstOrDefaultAsync(x => x.ItemId == itemId);
            var item = _dataService.GetItem(itemId);
            if (item == null)
                return NotFound("Item was not found.");
            return Ok(item);
        }

        [HttpGet]
        [Route("colors")]
        public async Task<ActionResult<IEnumerable<int>>> GetColors(string id)
        {
            List<int> colors = new List<int>();
            try
            {
                using var client = BricklinkClientFactory.Build();
                var knownColors = await client.GetKnownColorsAsync(ItemType.Part, id);
                client.Dispose();

                foreach (var c in knownColors)
                {
                    colors.Add(c.ColorId);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return colors;
        }

    }
}
