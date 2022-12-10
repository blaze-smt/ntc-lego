using BricklinkSharp.Client;

using Microsoft.AspNetCore.Mvc;

using NTC_Lego.Server.Services;
using NTC_Lego.Shared;

namespace NTC_Lego.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly DataService _dataService;

        public AdminController(DataContext dataContext)
        {
            _dataService = new DataService(dataContext);
        }

        [HttpGet]
        public IEnumerable<ItemVM> Get(int page)
        {
            int pageSize = 10;
            int skip = (page - 1) * pageSize;
            var items = _dataService.GetItems(skip, pageSize);
            return items;
        }

        [HttpGet]
        [Route("search/{searchText}")]
        public async Task<ActionResult<List<Item>>> SearchItems(string searchText)
        {
            return await _dataService.SearchItems(searchText);
        }

        [HttpGet]
        [Route("getItem/{itemId}")]
        public Item GetItem(string itemId)
        {
            return _dataService.GetItem(itemId);
        }

        [HttpGet]
        [Route("colors")]
        public async Task<ActionResult<IEnumerable<int>>> GetColors(string id)
        {
            List<int> colors = new List<int>();
            try
            {
                using var client = BricklinkClientFactory.Build();
                var knownColors = await client.GetKnownColorsAsync(BricklinkSharp.Client.ItemType.Part, id);
                client.Dispose();

                foreach (var c in knownColors)
                {
                    colors.Add(c.ColorId);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return colors;
        }

        [HttpGet]
        [Route("itemcolors")]
        public async Task<ActionResult<IEnumerable<ColorVM>>> GetItemColors(string itemId, string itemType)
        {
            List<int> colors = new List<int>();
            if (itemType == "P")
            {
                try
                {
                    using var client = BricklinkClientFactory.Build();
                    var knownColors = await client.GetKnownColorsAsync(BricklinkSharp.Client.ItemType.Part, itemId);
                    client.Dispose();

                    foreach (var c in knownColors)
                    {
                        colors.Add(c.ColorId);
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            else
            {
                colors.Add(0);
            }

            List<ColorVM> itemColors = new List<ColorVM>();
            foreach (int colorId in colors)
            {
                itemColors.Add(_dataService.GetItemColor(colorId));
            }

            return itemColors;
        }

        [HttpGet]
        [Route("sales-recent")]
        public IEnumerable<SaleOrderVM> GetPurchasesRecent()
        {
            var sales = _dataService.GetSaleOrdersRecent();
            return sales;
        }

        [HttpGet]
        [Route("sales")]
        public IEnumerable<SaleOrderVM> GetSales(int page)
        {
            int pageSize = 10;
            int skip = (page - 1) * pageSize;
            var sales = _dataService.GetSaleOrders(skip, pageSize);
            return sales;
        }

        [HttpGet]
        [Route("allsales")]
        public decimal GetAllSales()
        {
            var sales = _dataService.GetAllSaleOrders();
            return sales;
        }

        [HttpGet]
        [Route("allpurchases")]
        public decimal GetAllPurchases()
        {
            var purchases = _dataService.GetAllPurchaseOrders();
            return purchases;
        }
    }
}
