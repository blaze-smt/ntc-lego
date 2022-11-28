using BricklinkSharp.Client;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NTC_Lego.Server.Services;
using NTC_Lego.Shared;

using Inventory = NTC_Lego.Shared.Inventory;

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
        [Route("purchases")]
        public IEnumerable<PurchaseOrderVM> GetPurchases(int page)
        {
            int pageSize = 10;
            int skip = (page - 1) * pageSize;
            var purchases = _dataService.GetPurchaseOrders(skip, pageSize);
            return purchases;
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
