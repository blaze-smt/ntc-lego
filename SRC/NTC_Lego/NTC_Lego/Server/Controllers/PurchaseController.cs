using BricklinkSharp.Client;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NTC_Lego.Server.Services;
using NTC_Lego.Shared;

using Inventory = NTC_Lego.Shared.Inventory;
using OrderStatus = NTC_Lego.Shared.OrderStatus;

namespace NTC_Lego.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly PurchaseService _purchaseService;

        public PurchaseController(DataContext dataContext)
        {
            _purchaseService = new PurchaseService(dataContext);
        }

        [HttpGet]
        [Route("purchases")]
        public IEnumerable<PurchaseOrderVM> GetPurchases(int page)
        {
            int pageSize = 10;
            int skip = (page - 1) * pageSize;
            var purchases = _purchaseService.GetPurchaseOrders(skip, pageSize);
            return purchases;
        }

        [HttpGet]
        [Route("purchase-id")]
        public PurchaseOrderVM GetPurchaseId(int id)
        {
            var model = _purchaseService.GetPurchaseOrderVM(id);
            return model;
        }

        [HttpPost]
        [Route("purchase-cancel")]
        public async Task<IActionResult> PurchaseCancel([FromBody] int id)
        {
            List<string> actions = new List<string>();

            PurchaseOrder existing = _purchaseService.GetPurchaseOrder(id);
            PurchaseOrder updated = existing;

            // Reverse inventory quantity change
            foreach (var detail in updated.PurchaseOrderDetails)
            {
                var quantity = detail.PurchaseOrderDetailQuantity;

                Inventory inventory = _purchaseService.GetInventory(detail.InventoryId);
                if (inventory.QuantityTotal < quantity) 
                {
                    actions.Add("Insufficent quantity in inventory. Cannot return stock which does not exist.");
                    actions.Insert(0, "Failure: ");
                    return BadRequest(actions);
                }

                IEnumerable<InventoryLocation> locations = _purchaseService.GetInventoryLocations(detail.InventoryId);
                foreach (var location in locations)
                {
                    if (location.ItemQuantity <= quantity)
                    {
                        quantity = quantity - location.ItemQuantity;
                        actions.Add($"Deleted Location {location.LocationId} in Inventory {location.InventoryId}.");
                        _purchaseService.DeleteInventoryLocation(location);
                    }
                    else
                    {
                        InventoryLocation updatedIL = location;
                        updatedIL.ItemQuantity = updatedIL.ItemQuantity - quantity;
                        actions.Add($"Updated Location {location.LocationId} in Inventory {location.InventoryId}; Reduced quantity by {quantity}, new total is {location.ItemQuantity}.");
                        _purchaseService.UpdateInventoryLocation(location, updatedIL);
                        break;
                    }
                }
            }

            updated.OrderStatus = OrderStatus.Canceled;
            _purchaseService.UpdatePurchaseOrder(existing, updated);
            actions.Add($"Updated Purchase Order {existing.PurchaseOrderId}, OrderStatus changed to Canceled.");
            actions.Insert(0, "Success: ");

            return Ok(actions);
        }
    }
}
