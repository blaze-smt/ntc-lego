using Microsoft.AspNetCore.Mvc;

using NTC_Lego.Server.Services;
using NTC_Lego.Shared;

using Inventory = NTC_Lego.Shared.Inventory;

namespace NTC_Lego.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _inventoryService;

        public InventoryController(DataContext dataContext)
        {
            _inventoryService = new InventoryService(dataContext);
        }

        [HttpGet]
        [Route("location")]
        public IEnumerable<LocationVM> GetLocation()
        {
            var locations = _inventoryService.GetLocations();
            return locations;
        }

        [HttpGet]
        [Route("item")]
        public ActionResult<ItemVM> GetItem(string itemId)
        {
            var item = _inventoryService.GetItem(itemId);
            if (item == null) return BadRequest();
            return Ok(item);
        }

        [HttpGet]
        [Route("inventory-recent")]
        public IEnumerable<InventoryVM> GetInventoryRecent()
        {
            var inventories = _inventoryService.GetInventoriesRecent();
            return inventories;
        }

        [HttpGet]
        [Route("inventory2")]
        public IEnumerable<InventoryVM> GetInventory2(int page)
        {
            int pageSize = 9;
            int skip = (page - 1) * pageSize;
            var inventories = _inventoryService.GetInventories(skip, pageSize);
            return inventories;
        }

        [HttpGet]
        [Route("inventory")]
        public IEnumerable<InventoryVM> GetInventory(int page)
        {
            int pageSize = 10;
            int skip = (page - 1) * pageSize;
            var inventories = _inventoryService.GetInventories(skip, pageSize);
            return inventories;
        }

        [HttpGet]
        [Route("inventory-id")]
        public InventoryVM GetInventoryId(int inventoryId)
        {
            var inventory = _inventoryService.GetInventoryVM(inventoryId);
            return inventory;
        }

        [HttpPost]
        [Route("add-inventory")]
        public async Task<ActionResult<Inventory>> AddInventory([FromBody] InventoryAddVM inventory)
        {
            // If inventory with same itemId and colorId does not exist, create new
            Inventory existingInventory = _inventoryService.GetInventory(inventory.ItemId, inventory.ColorId);
            if (existingInventory != null)
            {
                // If inventorylocation exists, add to quantity
                InventoryLocation existingInventoryLocation = _inventoryService.GetInventoryLocation(existingInventory.InventoryId, inventory.LocationId);
                if (existingInventoryLocation != null)
                {
                    InventoryLocation updateInventoryLocation = existingInventoryLocation;
                    updateInventoryLocation.ItemQuantity += inventory.ItemQuantity;
                    _inventoryService.UpdateInventoryLocation(existingInventoryLocation, updateInventoryLocation);
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
                    _inventoryService.AddInventoryLocation(newInventoryLocation);
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
                    InventoryLocations = new List<InventoryLocation>(),
                };
                // Add initial quantity
                InventoryLocation newInventoryLocation = new InventoryLocation()
                {
                    LocationId = inventory.LocationId,
                    ItemQuantity = inventory.ItemQuantity,
                };
                newInventory.InventoryLocations.Add(newInventoryLocation);
                _inventoryService.AddInventory(newInventory);

                return Ok();
            }
        }

        [HttpPost]
        [Route("edit-inventory")]
        public async Task<ActionResult<Inventory>> EditInventory([FromBody] InventoryVM inventory)
        {
            Inventory existingInventory = _inventoryService.GetInventory(inventory.InventoryId);
            Inventory updateInventory = existingInventory;
            updateInventory.InventoryItemPrice = inventory.InventoryItemPrice;
            _inventoryService.UpdateInventory(existingInventory, updateInventory);
            return Ok();
        }
    }
}
