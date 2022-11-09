using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC_Lego.Shared
{
    public enum ShippingStatus
    {
        Unshipped,
        Shipped,
        Returned
    }

    public enum PaymentStatus
    {
        Unpaid,
        Paid,
        Refunded
    }
}
