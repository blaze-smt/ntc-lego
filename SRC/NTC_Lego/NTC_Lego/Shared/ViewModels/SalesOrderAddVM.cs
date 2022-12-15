using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class SalesOrderAddVM
    {
        public DateTime SaleOrderDate { get; set; } = DateTime.Now;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public int UserId { get; set; } = 1;
        public string UserName { get; set; } = string.Empty;
        //using purchaseorderaddvm, so we dont need to create salesorderdetailaddvm.
        public ICollection<PurchaseOrderDetailAddVM>? PurchaseOrderDetails { get; set; }
    }
}
