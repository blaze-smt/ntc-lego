using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NTC_Lego.Server.Services;
using NTC_Lego.Server.ViewModels;
using NTC_Lego.Shared;
using System.Security.Claims;

namespace NTC_Lego.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        //private readonly AppConfig _appConfig;
        private readonly DataService _dataService;

        public ItemController(ILogger<ItemController> logger, DataContext dataContext)
        {
            _logger = logger;
            //_appConfig = appConfig.Value;
            _dataService = new DataService(dataContext);
        }

        [Route("Item/{id:int}")]
        public IActionResult Item(int ItemId)
        {
            return null;
/*            Item item = _dataService.GetItem(ItemId);
            ItemViewModel ivm = new ItemViewModel();
            ivm.ItemName = item.ItemName;
            ivm.ItemId = item.ItemId;
            ivm.ItemWeight = item.ItemWeight;
            return Ok(ivm);
*/        }

        [AllowAnonymous]
        [Route("Items")]
        public IActionResult Items(string sort, string filter)
        {
            List<Item> items = _dataService.GetItems();
            if (!String.IsNullOrEmpty(filter))
            {
                items = items
                    .Where(i => i.ItemName.ToLower().Contains(filter.ToLower())
                        || i.ItemTypeId.ToLower().Contains(filter.ToLower())
                    )
                    .ToList();
            }
/*            switch (sort)
            {
                case "new":
                    items = items.Where(x => x.IsNew == true).ToList();
                    break;
                case "trending":
                    items = items.Where(x => x.IsTrending == true).ToList();
                    break;
                case "popular":
                    items = items.Where(x => x.IsPopular == true).ToList();
                    break;
                case "name_desc":
                    items = items.OrderByDescending(x => x.Name).ToList();
                    break;
                default:
                    items = items.OrderBy(x => x.Name).ToList();
                    break;
            }
*/
            return Ok(items);
        }
    }
}
