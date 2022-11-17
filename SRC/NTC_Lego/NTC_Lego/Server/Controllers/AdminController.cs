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
        [Route("location")]
        public IEnumerable<LocationVM> GetLocation()
        {
            var locations = _dataService.GetLocations();
            return locations;
        }

        [HttpGet]
        [Route("item")]
        public ActionResult<ItemVM> GetItem(string itemId)
        {
            var item = _dataService.GetItemVM(itemId);
            if (item == null) return BadRequest();
            return Ok(item);
        }

        [HttpGet]
        [Route("inventory")]
        public IEnumerable<InventoryVM> GetInventory(int page)
        {
            int pageSize = 10;
            int skip = (page - 1) * pageSize;
            var inventories = _dataService.GetInventories(skip, pageSize);
            return inventories;
        }

        [HttpPost]
        [Route("addinventory")]
        public async Task<ActionResult<Inventory>> AddInventory([FromBody] InventoryAddVM inventory)
        {
            // If inventory with same itemId and colorId does not exist, create new
            Inventory existingInventory = _dataService.GetInventory(inventory.ItemId,inventory.ColorId);
            if (existingInventory != null) 
            {
                // If inventorylocation exists, add to quantity
                InventoryLocation existingInventoryLocation = _dataService.GetInventoryLocation(existingInventory.InventoryId, inventory.LocationId);
                if (existingInventoryLocation != null)
                {
                    InventoryLocation updateInventoryLocation = existingInventoryLocation;
                    updateInventoryLocation.ItemQuantity += inventory.ItemQuantity;
                    _dataService.UpdateInventoryLocation(existingInventoryLocation, updateInventoryLocation);
                    return Ok();
                }
                else
                {
                    InventoryLocation newInventoryLocation = new InventoryLocation()
                    {
                        InventoryId = existingInventory.InventoryId,
                        LocationId = inventory.LocationId,
                        ItemQuantity = inventory.ItemQuantity,
                    };
                    _dataService.AddInventoryLocation(newInventoryLocation);
                    return Ok();
                }
            } 
            else
            {
                // Create new inventory
                Inventory newInventory = new Inventory()
                {
                    InventoryItemPrice = inventory.InventoryItemPrice,
                    ItemId = inventory.ItemId,
                    ColorId = inventory.ColorId,
                };
                _dataService.AddInventory(newInventory);

                // Add initial quantity
                existingInventory = _dataService.GetInventory(inventory.ItemId, inventory.ColorId);
                InventoryLocation newInventoryLocation = new InventoryLocation()
                {
                    InventoryId = existingInventory.InventoryId,
                    LocationId = inventory.LocationId,
                    ItemQuantity = inventory.ItemQuantity,
                };
                _dataService.AddInventoryLocation(newInventoryLocation);
                return Ok();
            }
        }
    }
}
