﻿using BricklinkSharp.Client;

using Microsoft.AspNetCore.Mvc;
using NTC_Lego.Server.Services;
using NTC_Lego.Shared;
using static System.Net.Mime.MediaTypeNames;
using Inventory = NTC_Lego.Shared.Inventory;
using ItemType = BricklinkSharp.Client.ItemType;

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
        public IEnumerable<Item> Get(int page)
        {
            int pageSize = 10;
            var items = _dataService.GetItems().Skip((page - 1) * pageSize).Take(pageSize);
            return items;

        }

        [HttpGet]
        [Route("purchases")]
        public IEnumerable<PurchaseOrder> GetPurchases(int page)
        {
            int pageSize = 10;
            var purchases = _dataService.GetPurchaseOrders().Skip((page - 1) * pageSize).Take(pageSize);
            return purchases;
        }

        [HttpGet]
        [Route("sales")]
        public IEnumerable<SaleOrder> GetSales(int page)
        {
            int pageSize = 10;
            var sales = _dataService.GetSaleOrders().Skip((page - 1) * pageSize).Take(pageSize);
            return sales;
        }

        [HttpGet]
        [Route("inventory")]
        public IEnumerable<Inventory> GetInventory(int page)
        {
            int pageSize = 10;
            var inventories = _dataService.GetInventories().Skip((page - 1) * pageSize).Take(pageSize);
            return inventories;
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
