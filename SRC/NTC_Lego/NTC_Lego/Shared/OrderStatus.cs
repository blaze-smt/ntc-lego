using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC_Lego.Shared
{
    // Enumerator class for Purchase and Sale Orders.
    public enum OrderStatus
    {
        Pending,
        Shipped,
        Delivered,
        Canceled
    }
}
