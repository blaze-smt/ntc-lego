using System;

using Microsoft.AspNetCore.Mvc;
using NTC_Lego.Server.Services;
using NTC_Lego.Shared;
using SaleOrder = NTC_Lego.Shared.SaleOrder;

namespace NTC_Lego.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleOrderController : ControllerBase
    {
        private DataContext _dataContext;

        public SaleOrderController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("cancelsaleorder")]
        public async Task<ActionResult<SaleOrder>> CancelOrder(SaleOrderVM saleOrderVM)
        {

            var salesOrder = _dataContext.SaleOrder.Where(s => s.SaleOrderId == saleOrderVM.SaleOrderId).FirstOrDefault();
            if (salesOrder != null)
            {
                salesOrder.SaleOrderDate = saleOrderVM.SaleOrderDate;
                salesOrder.UserId = (int)saleOrderVM.UserId;
                salesOrder.OrderStatus = saleOrderVM.OrderStatus;
            }
            else
            {
                return NotFound();
            }

            _dataContext.SaveChanges();


            return Ok();
        }
    }

}

