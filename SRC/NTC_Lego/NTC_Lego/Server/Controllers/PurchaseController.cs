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
        [Route("suppliers")]
        public IEnumerable<SupplierVM> GetSuppliers()
        {
            var suppliers = _purchaseService.GetSuppliers();
            return suppliers;
        }


        [HttpGet]
        [Route("purchases-recent")]
        public IEnumerable<PurchaseOrderVM> GetPurchasesRecent()
        {
            var purchases = _purchaseService.GetPurchaseOrdersRecent();
            return purchases;
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
                IEnumerable<InventoryLocation> locations = _purchaseService.GetInventoryLocations(detail.InventoryId);
                if (inventory.QuantityTotal < quantity)
                {
                    actions.Add("Insufficent quantity in inventory. Cannot return stock which does not exist.");
                    actions.Insert(0, "Failure: ");
                    return BadRequest(actions);
                }

                if (inventory.QuantityTotal == quantity)
                {
                    actions.Add($"Sufficent quantity. Deleted Inventory {detail.InventoryId}.");
                    foreach (var location in locations)
                    {
                        _purchaseService.DeleteInventoryLocation(location);
                    }
                    _purchaseService.DeleteInventory(inventory);
                }
                else
                {
                    foreach (var location in locations)
                    {
                        if (location.ItemQuantity < quantity)
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

            }

            updated.OrderStatus = OrderStatus.Canceled;
            _purchaseService.UpdatePurchaseOrder(existing, updated);
            actions.Add($"Updated Purchase Order {existing.PurchaseOrderId}, OrderStatus changed to Canceled.");
            actions.Insert(0, "Success: ");

            return Ok(actions);
        }

        [HttpPost]
        [Route("add-purchase")]
        public async Task<IActionResult> AddPurchase([FromBody] PurchaseOrderAddVM order)
        {
            List<string> actions = new List<string>();

            if (order.PurchaseOrderDetails == null || order.PurchaseOrderDetails.Count <= 0)
            {
                actions.Add("Purchase Order must contain at least 1 Purchase Order Detail.");
                actions.Insert(0, "Failure: ");
                return BadRequest(actions);
            }
            // Create new purchase order
            PurchaseOrder purchaseOrder = new PurchaseOrder()
            {
                PurchaseOrderDate = order.PurchaseOrderDate,
                OrderStatus = order.OrderStatus,
                SupplierId = order.SupplierId
            };

            // Create new purchase order details
            List<PurchaseOrderDetail> orderDetails = new List<PurchaseOrderDetail>();
            foreach (var detail in order.PurchaseOrderDetails)
            {
                var newDetail = new PurchaseOrderDetail()
                {
                    PurchaseOrderDetailQuantity = detail.Quantity,
                };

                // Create or find inventory
                Inventory existingInventory = _purchaseService.GetInventory(detail.ItemId, detail.ColorId);
                if (existingInventory != null)
                {
                    actions.Add($"Inventory {existingInventory.InventoryId} found for item {existingInventory.ItemId} of color {existingInventory.ColorId}.");
                    // If inventorylocation exists, add to quantity
                    InventoryLocation existingInventoryLocation = _purchaseService.GetInventoryLocation(existingInventory.InventoryId, detail.LocationId);
                    if (existingInventoryLocation != null)
                    {
                        InventoryLocation updateInventoryLocation = existingInventoryLocation;
                        updateInventoryLocation.ItemQuantity += detail.Quantity;
                        _purchaseService.UpdateInventoryLocation(existingInventoryLocation, updateInventoryLocation);
                        actions.Add($"Updated Location {existingInventoryLocation.LocationId} in Inventory {existingInventoryLocation.InventoryId}; Increased quantity by {detail.Quantity}, new total is {updateInventoryLocation.ItemQuantity}.");
                    }
                    else
                    {
                        InventoryLocation newInventoryLocation = new InventoryLocation()
                        {
                            InventoryId = existingInventory.InventoryId,
                            LocationId = detail.LocationId,
                            ItemQuantity = detail.Quantity,
                        };
                        _purchaseService.AddInventoryLocation(newInventoryLocation);
                        actions.Add($"Created Location {newInventoryLocation.LocationId} in Inventory {existingInventory.InventoryId}; Initial quantity is {newInventoryLocation.ItemQuantity}.");
                    }
                    newDetail.InventoryId = existingInventory.InventoryId;
                }
                else
                {
                    // Create new inventory
                    Inventory newInventory = new Inventory()
                    {
                        InventoryItemPrice = detail.Price+(detail.Price * 0.05m), // set inventory list price to 5% more than buying price
                        ItemId = detail.ItemId,
                        ColorId = detail.ColorId,
                        InventoryLocations = new List<InventoryLocation>(),
                    };
                    // Add initial quantity
                    InventoryLocation newInventoryLocation = new InventoryLocation()
                    {
                        LocationId = detail.LocationId,
                        ItemQuantity = detail.Quantity,
                    };
                    newInventory.InventoryLocations.Add(newInventoryLocation);
                    newDetail.Inventory = newInventory;
                    actions.Add($"Created new inventory with initial quantity set to {newInventoryLocation.ItemQuantity} at location {newInventoryLocation.LocationId}.");
                }
                orderDetails.Add(newDetail);
            }

            purchaseOrder.PurchaseOrderDetails = orderDetails;
            _purchaseService.AddPurchaseOrder(purchaseOrder);

            actions.Insert(0, "Success: ");
            return Ok(actions);
        }
    }
}
