using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GamerParadise.DataAccess.Models;

namespace GamerParadise.Models
{
    public class ProfileViewModel
    {
        public User User { get; set; }

        public Subscription Subscription { get; set; }

        public List<CreditCard> CreditCards { get; set; }
    }
}