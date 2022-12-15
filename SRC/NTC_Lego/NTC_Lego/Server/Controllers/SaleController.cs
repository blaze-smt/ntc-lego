using BricklinkSharp.Client;

using Microsoft.AspNetCore.Mvc;

using NTC_Lego.Server.Services;
using NTC_Lego.Shared;

using Inventory = NTC_Lego.Shared.Inventory;
using OrderStatus = NTC_Lego.Shared.OrderStatus;

namespace NTC_Lego.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleController : Controller
    {
        private readonly SaleService _saleService;

        public SaleController(DataContext dataContext)
        {
            _saleService = new SaleService(dataContext);
        }

        [HttpGet]
        [Route("user")]
        public IEnumerable<UserVM> GetUser()
        {
            var user = _saleService.GetUser();
            return user;
        }

        [HttpGet]
        [Route("sale-recent")]
        public IEnumerable<SaleOrderVM> GetSaleRecent()
        {
            var sale = _saleService.GetSaleOrderRecent();
            return sale;
        }

        [HttpGet]
        [Route("sales")]
        public IEnumerable<SaleOrderVM> GetSales(int page)
        {
            int pageSize = 10;
            int skip = (page - 1) * pageSize;
            var sales = _saleService.GetSaleOrders(skip, pageSize);
            return sales;
        }

        [HttpGet]
        [Route("sale-id")]
        public SaleOrderVM GetSaleId(int id)
        {
            var model = _saleService.GetSaleOrderVM(id);
            return model;
        }

        [HttpPost]
        [Route("add-sale")]
        public async Task<IActionResult> AddSale([FromBody] SalesOrderAddVM order)
        {
            List<string> actions = new List<string>();
            if(order.PurchaseOrderDetails == null || order.PurchaseOrderDetails.Count == 0)
            {
                actions.Add("Purchase Order must contain at least 1 Purchase Order Detail.");
                actions.Insert(0, "Failure: ");
                return BadRequest(actions);
            }
            // Create new sale order
            SaleOrder saleOrder = new SaleOrder()
            {
                SaleOrderDate = order.SaleOrderDate,
                OrderStatus = order.OrderStatus,
                UserId = order.UserId
            };

            //Create new sales order details
            List<SaleOrderDetail> orderDetails = new List<SaleOrderDetail>();
            foreach (var detail in order.PurchaseOrderDetails)
            {

                var newDetail = new SaleOrderDetail()
                {
                    SaleOrderDetailQuantity = detail.Quantity,
                };

                // Create or find inventory
                Inventory existingInventory = _saleService.GetInventory(detail.ItemId, detail.ColorId);
                if (existingInventory != null)
                {
                    actions.Add($"Inventory {existingInventory.InventoryId} found for item {existingInventory.ItemId} of color {existingInventory.ColorId}.");
                    // If inventorylocation exists, add to quantity
                    InventoryLocation existingInventoryLocation = _saleService.GetInventoryLocation(existingInventory.InventoryId, detail.LocationId);
                    if (existingInventoryLocation != null)
                    {
                        if (existingInventoryLocation.ItemQuantity >= detail.Quantity)
                        {
                            InventoryLocation updateInventoryLocation = existingInventoryLocation;
                            updateInventoryLocation.ItemQuantity -= detail.Quantity;
                            _saleService.UpdateInventoryLocation(existingInventoryLocation, updateInventoryLocation);
                            actions.Add($"Updated Location {existingInventoryLocation.LocationId} in Inventory {existingInventoryLocation.InventoryId}; Decrease quantity by {detail.Quantity}, new total is {updateInventoryLocation.ItemQuantity}.");
                        }
                        else
                        {
                            actions.Add($"Invalid quantity amount of item.{existingInventoryLocation.LocationId} Has only {existingInventory.QuantityTotal}");
                            actions.Insert(0, "Fail");
                            return Ok(actions);
                        }
                    }
                    else
                    {

                        actions.Add($"Not enough amount of item in stock.");
                        actions.Insert(0, "Fail ");
                        return Ok(actions);
                    }
                    newDetail.InventoryId = existingInventory.InventoryId;
                }
                else
                {
                    actions.Add($"No inventory found.");
                    actions.Insert(0, "Fail ");
                    return Ok(actions);
                }
                orderDetails.Add(newDetail);
            }

            saleOrder.SaleOrderDetails = orderDetails;
            _saleService.AddSaleOrder(saleOrder);

            actions.Insert(0, "Success: ");
            return Ok(actions);
        }

        [HttpPost]
        [Route("sale-cancel")]
        public async Task<IActionResult> SaleCancel([FromBody] int id)
        {
            List<string> actions = new List<string>();

            SaleOrder salesOrder = _saleService.GetSaleOrder(id);
                if (salesOrder != null)
            {
                SaleOrder updateOrder = salesOrder;
                updateOrder.OrderStatus = OrderStatus.Canceled;
                _saleService.UpdateSaleOrder(salesOrder, updateOrder);
                actions.Add($"Updated Purchase Order {updateOrder.SaleOrderId}, OrderStatus changed to Canceled.");
            }
            else
            {
                actions.Add("sale order can't be found.");
                actions.Insert(0, "Failure");
                return BadRequest(actions);
            }

            actions.Insert(0, "Success: ");
            return Ok(actions);

        }
    }
}
