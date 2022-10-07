using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GamerParadise.DataAccess.Models;

namespace GamerParadise.Models
{
    public class SubscriptionViewModel
    {
        public Subscription Subscription { get; set; }

        public List<Plan> AllPlans { get; set; }
    }
}